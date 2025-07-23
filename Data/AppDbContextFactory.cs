using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Hexecuter.Data;

public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
{
    public AppDbContext CreateDbContext(string[] args)
    {
        var dbPath = GetAbsoluteDatabasePath();

        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
        optionsBuilder.UseSqlite($"Data Source={dbPath}");

        return new AppDbContext(optionsBuilder.Options);
    }

    private string GetAbsoluteDatabasePath()
    {
        var baseDir = AppDomain.CurrentDomain.BaseDirectory;
        var dbDir = Path.Combine(baseDir, "Db");

        if (!Directory.Exists(dbDir))
            Directory.CreateDirectory(dbDir);

        return Path.Combine(dbDir, "Hexecuter.db");
    }
}
