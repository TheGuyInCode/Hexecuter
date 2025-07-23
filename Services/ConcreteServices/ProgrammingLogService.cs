
using DiscUtils.Fat;
using DiscUtils.Raw;
using DiscUtils.Setup;
using DiscUtils.Streams;
using Hexecuter.Entities;
using Hexecuter.Enums;
using Hexecuter.Repositories.AbstractRepositories;
using Hexecuter.Services.AbstractServices;
using Microsoft.VisualBasic.Logging;
using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Management;
using System.Reflection.Metadata;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
namespace Hexecuter.Services.ConcreteServices
{
    public class ProgrammingLogService : IProgrammingLogService
    {
        private readonly IProgrammingLogRepository _programmingLogRepository;
        private readonly IDeviceService _deviceService;
        private readonly IDiskAccess _diskAccess;
        private const string ProgrammerCli = "pickitcli.exe";

        public delegate void TextHandler(object sender, string message);

        public delegate void ProgressHandler(object sender, int progressPercentage);
        public ProgrammingLogService(IProgrammingLogRepository programmingLogRepository, IDeviceService deviceService,IDiskAccess diskAccess)
        {
            _programmingLogRepository = programmingLogRepository;
            _deviceService = deviceService;
            _diskAccess = diskAccess;
        }
        public async Task AddLogAsync(ProgrammingLog log)
        {
            await _programmingLogRepository.AddAsync(log);
        }

        public async Task<ProgrammingLog> CopyFirmwareToSdCardAsync(Entities.Device sdCard, string imgFilePath, IProgress<int>? progress = null)
        {
            var existingDevice = await _deviceService.GetDeviceByIdAsync(sdCard.Id);

            var device = new Hexecuter.Entities.Device
            {
                Category = sdCard.Category,
                DeviceName = sdCard.DeviceName,
                RootPath = sdCard.RootPath,
                UsbIdentifier = sdCard.UsbIdentifier ?? "Unknown Device",
                ProgrammingLogs = null,
                SerialPortName = null,
            };

            await _deviceService.AddDeviceAsync(device);


            var log = new ProgrammingLog
            {
                DeviceId = device.Id,
                DeviceName = device.DeviceName,
                UserName = Environment.UserName ?? "Unknown",                
            };

            try
            {
                // 1. DriveLetter ve fiziksel yol bul
                var driveLetter = sdCard.RootPath?.Substring(0, 2); // "E:"
                if (string.IsNullOrWhiteSpace(driveLetter))
                    throw new Exception("SD kart sürücü harfi alınamadı.");

                var physicalPath = _diskAccess.GetPhysicalPathForLogicalPath(driveLetter);
                if (string.IsNullOrWhiteSpace(physicalPath))
                    throw new Exception("Fiziksel sürücü yolu bulunamadı.");

                // 2. Sürücüyü kilitle
                if (!_diskAccess.LockDrive(driveLetter))
                    throw new Exception("SD kart kilitlenemedi (başka bir program kullanıyor olabilir).");

                // 3. Fiziksel diski aç
                var handle = _diskAccess.Open(physicalPath);
                if (handle == null)
                    throw new Exception("Fiziksel sürücü açılamadı!");

                // 4. Dosyayı aç ve yaz
                var fileLength = new FileInfo(imgFilePath).Length;
                using (var imgStream = new FileStream(imgFilePath, FileMode.Open, FileAccess.Read))
                {
                    long offset = 0;
                    var buffer = new byte[32 * 1024 * 1024]; // 16MB
                    int bytesRead;
                    while ((bytesRead = await imgStream.ReadAsync(buffer, 0, buffer.Length)) > 0)
                    {
                        int wroteBytes;
                        var result = _diskAccess.Write(buffer, bytesRead, out wroteBytes);
                        if (result < 0 || wroteBytes != bytesRead)
                            throw new IOException("Disk yazma hatası!");

                        offset += wroteBytes;
                        int percent = (int)((offset * 100) / fileLength);
                        progress?.Report(percent);
                    }
                }

                // 5. Sürücüyü serbest bırak
                //_diskAccess.UnlockDrive();
                //_diskAccess.Close();
                log.IsSuccess = true;
                log.Message = ".img dosyası SD karta başarıyla yazıldı!";
                
            }
            catch (Exception ex)
            {

                log.IsSuccess = false;
                log.Message = $"SD karta yazma hatası: {ex.Message}";
                //Debug.WriteLine(ex.ToString());
                //_diskAccess.UnlockDrive();

                //var logError = new ProgrammingLog
                //{
                //    DeviceId = device.Id,
                //    DeviceName = device.DeviceName,
                //    UserName = Environment.UserName ?? "Unknown",
                //    Message = $"SD karta yazma hatası: {ex.Message}",
                //    IsSuccess = false
                //};

                //await AddLogAsync(logError);
            }
            finally
            {
                try
                {
                   await _programmingLogRepository.AddAsync(log);
                }
                catch (Exception logEx)
                {
                    Debug.WriteLine("Veritabanı loglama hatası: " + logEx.ToString());                    
                }
            }
            
            return log;

        }

        public async Task<IEnumerable<ProgrammingLog>> GetAllAsync()
        {
            return await _programmingLogRepository.GetAllAsync();
        }

        public async Task<IEnumerable<ProgrammingLog>> GetByDeviceIdAsync(Guid deviceId)
        {
            return await _programmingLogRepository.GetByDeviceIdAsync(deviceId);
        }

        public async Task<ProgrammingLog?> GetByIdAsync(Guid logId)
        {
            return await _programmingLogRepository.GetByIdAsync(logId);
        }

        public async Task<ProgrammingLog> ProgramFirmwareAsync(Entities.Device device, Firmware firmware)
        {

            string stdOut = string.Empty, stdErr = string.Empty;
            int exitCode = -1;
            string cliPath, cliArgs;

            try
            {
                //switch (device.Category)
                //{
                //    case DeviceCategory.PicKit3:
                //        cliPath = "ipe.exe"; // MPLAB IPE CLI'nin yolu (config'den alınabilir)
                //        cliArgs = $"-P:{firmware.McuModel} -TPPK3 -F:\"{firmware.FilePath}\" -M -Y --VERBOSE";
                //        break;

                //    case DeviceCategory.Stm32Bootloader:
                //        cliPath = "STM32_Programmer_CLI.exe"; // STM32 Cube Programmer CLI
                //        cliArgs = $"-c port={device.SerialPortName} -d \"{firmware.FilePath}\" -v -rst";
                //        break;

                //    default:
                //        throw new NotSupportedException($"Desteklenmeyen cihaz tipi: {device.Category}");
                //}

                //if (!File.Exists(cliPath))
                //    throw new FileNotFoundException("Yükleyici CLI bulunamadı", cliPath);

                //var psi = new ProcessStartInfo
                //{
                //    FileName = cliPath,
                //    Arguments = cliArgs,
                //    UseShellExecute = false,
                //    RedirectStandardOutput = true,
                //    RedirectStandardError = true,
                //    CreateNoWindow = true
                //};

                //using var proc = Process.Start(psi)
                //     ?? throw new InvalidOperationException("Yükleyici başlatılamadı.");

                //stdOut = await proc.StandardOutput.ReadToEndAsync();
                //stdErr = await proc.StandardError.ReadToEndAsync();

                //var success = await Task.Run(() =>
                //{
                //    if (!proc.WaitForExit(30000))
                //    {
                //        try { proc.Kill(); } catch { /* optional */ }
                //        return false;
                //    }
                //    exitCode = proc.ExitCode;
                //    return true;
                //});

                //var log = new ProgrammingLog
                //{
                //    DeviceId = device.Id,
                //    UserName = Environment.UserName,
                //    IsSuccess = success && exitCode == 0,
                //    Message = success && exitCode == 0
                //        ? "Programlama başarılı."
                //        : $"Hata (ExitCode {exitCode}): {stdErr}"
                //};

                //await AddLogAsync(log);
                //return log;

            }
            catch (Exception ex)
            {
                var failLog = new ProgrammingLog
                {
                    DeviceId = device.Id,
                    UserName = Environment.UserName,
                    IsSuccess = false,
                    Message = $"İstisna: {ex.Message}\nHata Detayı: {stdErr}"
                };

                await AddLogAsync(failLog);

                return failLog;
            }
        }

        public async Task<ProgrammingLog> ExtractImgToSdCardAsync(Entities.Device sdCard, string imgFilePath, IProgress<int>? progress = null)
        {

            var device = new Hexecuter.Entities.Device
            {
                Category = sdCard.Category,
                DeviceName = sdCard.DeviceName,
                RootPath = sdCard.RootPath,
                UsbIdentifier = sdCard.UsbIdentifier,
                ProgrammingLogs = null,
                SerialPortName = null,
            };

            await _deviceService.AddDeviceAsync(device);

            var log = new ProgrammingLog
            {
                DeviceId = device.Id,
                DeviceName = device.DeviceName,
                UserName = Environment.UserName ?? "Unknown",
            };

            try
            {

                if (!File.Exists(imgFilePath))
                    throw new FileNotFoundException(".img dosyası bulunamadı.");

                if (!Directory.Exists(sdCard.RootPath))
                    throw new DirectoryNotFoundException("SD kart yolu bulunamadı veya erişilemiyor.");

                using var imgStream = File.OpenRead(imgFilePath);
                using var disk = new DiscUtils.Raw.Disk(imgStream, Ownership.None);

                if (disk.Partitions.Count == 0)
                    throw new InvalidOperationException(".img dosyasında partition bulunamadı!");

                var partition = disk.Partitions[0];

                var volume = partition.Open();

                var fatFs = new FatFileSystem(volume);

                await CopyFatFilesRecursiveAsync(fatFs, "", sdCard.RootPath, progress);

                log.IsSuccess = true;
                log.Message = ".img dosyasındaki dosyalar başarıyla SD karta ayıklandı!";

            }
            catch (Exception ex)
            {
                log.IsSuccess = false;
                log.Message = $"Ayıklama/yazma sırasında hata: {ex.Message}";
            }

            await _programmingLogRepository.AddAsync(log);

            return log;

        }

        public async Task CopyFatFilesRecursiveAsync(DiscUtils.Fat.FatFileSystem fs, string fatDir, string targetDir, IProgress<int>? progress = null)
        {
            fatDir = fatDir?.Trim('/') ?? "";

            var files = fs.GetFiles(fatDir);
            var directories = fs.GetDirectories(fatDir);

            int total = files.Length + directories.Length;
            int current = 0;

            var skipFolders = new[] { "SYSTEM~1", "System Volume Information", "RECYCLED" };

            // Dosyaları kopyala
            foreach (var file in files)
            {
                try
                {
                    if (string.IsNullOrWhiteSpace(file)) continue;

                    var relativePath = file.TrimStart('/').Replace('/', Path.DirectorySeparatorChar);
                    var destPath = Path.Combine(targetDir, relativePath);

                    var destDir = Path.GetDirectoryName(destPath);
                    if (!string.IsNullOrWhiteSpace(destDir))
                        Directory.CreateDirectory(destDir);

                    // Uyarı: kısa ad içeriyorsa logla (isteğe bağlı)
                    if (Path.GetFileName(file).Contains("~"))
                        Debug.WriteLine($"Uyarı: Kısa ad kullanılıyor: {file}");

                    using var src = fs.OpenFile(file, FileMode.Open, FileAccess.Read);
                    using var dst = File.Create(destPath);

                    await src.CopyToAsync(dst);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Dosya kopyalanamadı: {file} → {ex.Message}");
                }
                finally
                {
                    progress?.Report(++current * 100 / total);
                }
            }

            // Klasörleri kopyala (recursive)
            foreach (var dir in directories)
            {
                try
                {
                    if (string.IsNullOrWhiteSpace(dir)) continue;

                    var dirName = Path.GetFileName(dir);

                    if (dirName.IndexOfAny(Path.GetInvalidFileNameChars()) >= 0)
                        continue;

                    if (skipFolders.Any(s => dirName.Equals(s, StringComparison.OrdinalIgnoreCase)))
                    {
                        Debug.WriteLine($"Klasör atlandı: {dirName}");
                        continue;
                    }

                    var relativePath = dir.TrimStart('/').Replace('/', Path.DirectorySeparatorChar);
                    var newTargetDir = Path.Combine(targetDir, relativePath);

                    Directory.CreateDirectory(newTargetDir);

                    await CopyFatFilesRecursiveAsync(fs, dir, targetDir, progress); // Not: targetDir sabit kalıyor, relative path ile klasörler korunuyor
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Klasör kopyalanamadı: {dir} → {ex.Message}");
                }
                finally
                {
                    progress?.Report(++current * 100 / total);
                }
            }

        }
    }
}


