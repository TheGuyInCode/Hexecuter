using Hexecuter.Entities;
using Hexecuter.Enums;
using Hexecuter.Repositories.AbstractRepositories;
using Hexecuter.Services.AbstractServices;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Management;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Hexecuter.Services.ConcreteServices
{
    public class DeviceService : IDeviceService
    {
        private readonly IDeviceRepository _deviceRepository;
    //    private readonly (DeviceCategory Category, string Name, ushort Vid, ushort Pid)[] _cardDefs =
    //    {
    //    (DeviceCategory.SensorCard, "SensorCard",  0x04D8, 0x0033),  // Microchip PICkit3
    //    (DeviceCategory.MasterCard, "MasterCard",  0x04D8, 0x0033),  // Eğer farklısa kendi PID’ini ekleyin
    //    (DeviceCategory.DryerCard,  "DryerCard",   0x04D8, 0x0051),  // Örnek başka Microchip kart
    //    (DeviceCategory.StmCard,    "STMCard",     0x0483, 0x3748)   // ST-Link
    //};
        public DeviceService(IDeviceRepository deviceRepository)
        {
            _deviceRepository = deviceRepository;
        }

        public async Task AddDeviceAsync(Entities.Device device)
        {
             await _deviceRepository.AddDeviceAsync(device);            
        }

        public async Task<IEnumerable<Hexecuter.Entities.Device>> GetAllDevicesAsync()
        {
            return await _deviceRepository.GetAllAsync();
        }

        public async Task<IEnumerable<Hexecuter.Entities.Device>> GetConnectedDevicesAsync()
        {
            // Microchip Vendor ID ve gerçek PICkit3 PID (debug çıktınızdan aldığınız)
            const string MICROCHIP_VID = "04D8";
            const string PICKIT3_PID = "900A";

            var devices = new List<Hexecuter.Entities.Device>();

            await Task.Run(() =>
            {
                // WMI’den sadece Microchip VID’li aygıtları çekiyoruz
                const string wql = @"
                SELECT PNPDeviceID, Description, Name
                  FROM Win32_PnPEntity
                 WHERE PNPDeviceID LIKE '%VID_04D8%'
            ";

                using var searcher = new ManagementObjectSearcher(wql);
                var results = searcher.Get().Cast<ManagementObject>().ToList();

                Debug.WriteLine($"[INFO] Toplam {results.Count} Microchip cihazı bulundu.");

                foreach (var mo in results)
                {
                    var pnpId = mo["PNPDeviceID"] as string;
                    if (string.IsNullOrWhiteSpace(pnpId))
                        continue;

                    var description = mo["Description"]?.ToString()
                                      ?? mo["Name"]?.ToString()
                                      ?? "Bilinmeyen Aygıt";

                    Debug.WriteLine($"[PNP] {description} → {pnpId}");

                    // VID ve PID’i regex ile ayıkla
                    var vidMatch = Regex.Match(pnpId, @"VID_([0-9A-F]{4})", RegexOptions.IgnoreCase);
                    var pidMatch = Regex.Match(pnpId, @"PID_([0-9A-F]{4})", RegexOptions.IgnoreCase);

                    if (!vidMatch.Success || !pidMatch.Success)
                        continue;

                    var vid = vidMatch.Groups[1].Value;
                    var pid = pidMatch.Groups[1].Value;

                    Debug.WriteLine($"   Parsed → VID={vid}, PID={pid}");

                    // Sadece VID 04D8 & PID 900A eşleşirse listeye ekle
                    if (vid.Equals(MICROCHIP_VID, StringComparison.OrdinalIgnoreCase) &&
                        pid.Equals(PICKIT3_PID, StringComparison.OrdinalIgnoreCase))
                    {
                        Debug.WriteLine($"[MATCH] PICkit3 cihazı eklendi: {description}");

                        devices.Add(new Hexecuter.Entities.Device
                        {
                            Id = Guid.NewGuid(),
                            DeviceName = description,
                            Category = DeviceCategory.SensorCard,
                            UsbIdentifier = pnpId,
                            RootPath = null
                        });
                    }
                }
            });

            return devices;

        }


        public async Task<Hexecuter.Entities.Device?> GetDeviceByIdAsync(Guid deviceId)
        {
            return await _deviceRepository.GetByIdAsync(deviceId);
        }

        public async Task<IEnumerable<Hexecuter.Entities.Device>> GetRemovableDrivesAsync()
        {

            var drives = DriveInfo
                .GetDrives()
                .Where(d => d.DriveType == DriveType.Removable && d.IsReady);

            var devices = drives.Select(drive => new Hexecuter.Entities.Device
            {                
                DeviceName = $"SD Card ({drive.RootDirectory.FullName.TrimEnd('\\')})",
                Category = DeviceCategory.SdCard,
                UsbIdentifier = drive.VolumeLabel,                   
                RootPath = drive.RootDirectory.FullName,
                SerialPortName = null,                
            }).ToList();

            return await Task.FromResult(devices);
            
        }
    }
}
