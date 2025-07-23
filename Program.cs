using Hexecuter.Data;
using Hexecuter.Repositories.AbstractRepositories;
using Hexecuter.Repositories.ConcreteRepositories;
using Hexecuter.Services.AbstractServices;
using Hexecuter.Services.ConcreteServices;
using Hexecuter.Win32;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Hexecuter
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {

            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var services = new ServiceCollection();

            // Ayný path hesaplamasý burada da
            var dbPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Db", "Hexecuter.db");

            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlite($"Data Source={dbPath}"));

            services.AddScoped<IDeviceRepository, DeviceRepository>();
            services.AddScoped<IFirmwareRepository, FirmwareRepository>();
            services.AddScoped<IProgrammingLogRepository, ProgrammingLogRepository>();

            services.AddScoped<IDeviceService, DeviceService>();
            services.AddScoped<IFirmwareService, FirmwareService>();
            services.AddScoped<IProgrammingLogService, ProgrammingLogService>();
            services.AddScoped<IDiskAccess, Win32DiskAccess>();
            services.AddScoped<Form1>();


            using var provider = services.BuildServiceProvider();

            // Db dosyasý yoksa oluþtur
            using (var context = new AppDbContextFactory().CreateDbContext(Array.Empty<string>()))
            {
                context.Database.Migrate(); // Tablolar otomatik oluþturulur
            }


            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            var mainForm = provider.GetRequiredService<Form1>();
            Application.Run(mainForm);
            //ApplicationConfiguration.Initialize();
            //Application.Run(new Form1());
        }
    }
}