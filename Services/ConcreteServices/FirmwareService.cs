using Hexecuter.Entities;
using Hexecuter.Repositories.AbstractRepositories;
using Hexecuter.Services.AbstractServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Hexecuter.Services.ConcreteServices
{
    
    public class FirmwareService : IFirmwareService
    {
        private readonly IFirmwareRepository _firmwareRepository;

        public FirmwareService(IFirmwareRepository firmwareRepository)
        {
            _firmwareRepository = firmwareRepository;
        }
        private static async Task<string> ComputeChecksumAsync(string filePath)
        {
            using var stream = File.OpenRead(filePath);
            using var sha256 = SHA256.Create();
            var hashBytes = await sha256.ComputeHashAsync(stream);            
            return BitConverter
                .ToString(hashBytes)
                .Replace("-", "")
                .ToLowerInvariant();
        }

        public async Task<Firmware> AddFirmwareAsync(Firmware firmware)
        {
            await _firmwareRepository.AddFirmwareAsync(firmware);

            return firmware;
        }

        public async Task<Firmware> LoadFromFileAsync(string filePath)
        {

            if (string.IsNullOrWhiteSpace(filePath) || !File.Exists(filePath))
                if (string.IsNullOrWhiteSpace(filePath) || !File.Exists(filePath))
                    throw new FileNotFoundException("Firmware file not found.", filePath);

            var fileName = Path.GetFileNameWithoutExtension(filePath);

            // Örnek: "Sensor_PIC16F877A_v1.2.3"
            var parts = fileName.Split('_', StringSplitOptions.RemoveEmptyEntries);

            // Versiyon bilgisi (son parça, "v1.2.3" gibi)
            var version = parts.Length > 1 ? parts[^1] : fileName;

            // MCU Model (2. parça gibi varsayıyoruz)
            string? mcuModel = null;

            if (parts.Length >= 3)
            {
                // 2. indeks genelde MCU adını temsil eder gibi varsayıyoruz
                // Sensor_PIC16F877A_v1.2.3.hex → parts[1] = "PIC16F877A"
                mcuModel = parts[1].ToUpperInvariant();
            }

            // Checksum hesapla
            var checksum = await ComputeChecksumAsync(filePath);

            var newFirmware = new Firmware
            {
                Checksum = checksum,
                FilePath = filePath,
                McuModel = mcuModel,
                Version = version                
            };
            await AddFirmwareAsync(newFirmware);
            

            return new Firmware
            {                
                FilePath = filePath,
                Version = version,
                Checksum = checksum,
                McuModel = mcuModel,                
            };
        }

        public async Task<Firmware> LoadFromFileSdCardAsync(string filePath)
        {

            if (string.IsNullOrWhiteSpace(filePath) || !File.Exists(filePath) ||
                 !filePath.EndsWith(".img", StringComparison.OrdinalIgnoreCase))
            {
                throw new FileNotFoundException("Image firmware file not found or invalid extension.", filePath);
            }

            var fileName = Path.GetFileNameWithoutExtension(filePath);

            var parts = fileName.Split('_', StringSplitOptions.RemoveEmptyEntries);

            // Versiyon bilgisi: son parça örn. "backup" veya tarih
            var version = parts.Length > 1 ? parts[^1] : fileName;

            string? mcuModel = null;

            if (parts.Length >= 3)
            {
                // 2. indeks genelde MCU adını temsil eder gibi varsayıyoruz
                // Sensor_PIC16F877A_v1.2.3.hex → parts[1] = "PIC16F877A"
                mcuModel = parts[1].ToUpperInvariant();
            }
            // SD kart imajlarında MCU modeli yok
            var checksum = await ComputeChecksumAsync(filePath);

            var newFirmware = new Firmware
            {               
                Checksum = checksum,
                FilePath = filePath,
                McuModel = mcuModel,
                Version = version
            };

            await AddFirmwareAsync(newFirmware);

            return new Firmware
            {                
                FilePath = filePath,
                Version = version,
                Checksum = checksum,
                McuModel = mcuModel,                
            };
        }
    }
}
