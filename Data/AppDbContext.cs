using Hexecuter.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hexecuter.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Hexecuter.Entities.Device> Devices { get; set; }
        public DbSet<Firmware> Firmwares { get; set; }
        public DbSet<ProgrammingLog> ProgrammingLogs { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options): base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // DEVICE
            modelBuilder.Entity<Hexecuter.Entities.Device>(entity =>
            {
                entity.ToTable("Devices");
                entity.HasKey(d => d.Id);

                entity.Property(d => d.DeviceName)
                      .IsRequired()
                      .HasMaxLength(250);


                entity.Property(d => d.UsbIdentifier)
                      .IsRequired()
                      .HasMaxLength(300);

                entity.Property(d => d.RootPath)
                      .HasMaxLength(300);

                entity.Property(d => d.SerialPortName)
                      .HasMaxLength(200);

                entity.Property(d => d.Category)
                      .HasConversion<string>();

                entity.HasMany(d => d.ProgrammingLogs)
                      .WithOne(pl => pl.Device)
                      .HasForeignKey(pl => pl.DeviceId)
                      .OnDelete(DeleteBehavior.Cascade);

            });

            // FIRMWARE
            modelBuilder.Entity<Firmware>(entity =>
            {
                entity.ToTable("Firmwares");
                entity.HasKey(f => f.Id);

                entity.Property(f => f.FilePath)
                      .IsRequired();

                entity.Property(f => f.Version)
                      .IsRequired()
                      .HasMaxLength(50);

                entity.Property(f => f.Checksum)
                      .HasMaxLength(200);

                entity.Property(f => f.McuModel)
                      .HasMaxLength(100);


            });

            // PROGRAMMINGLOG
            modelBuilder.Entity<ProgrammingLog>(entity =>

            {

                entity.ToTable("ProgrammingLogs");
                
                entity.HasKey(pl => pl.Id);


                entity.Property(pl => pl.UserName)
                      .HasMaxLength(100);

                entity.Property(pl => pl.IsSuccess)
                      .IsRequired();

                entity.Property(pl => pl.Message)
                      .HasMaxLength(1000);

                entity.Property(pl => pl.LogOutput)
      .HasMaxLength(4000);

                entity.HasOne(pl => pl.Device)
                      .WithMany(d => d.ProgrammingLogs)
                      .HasForeignKey(pl => pl.DeviceId)
                      .OnDelete(DeleteBehavior.Cascade);


            });
        }
    }
}
