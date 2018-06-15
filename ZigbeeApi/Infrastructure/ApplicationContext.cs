using Core;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure
{
    public class ApplicationContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlServer(@"Data Source=WOJTEK;Initial Catalog=Zigbee;Integrated Security=True;
            //                                Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;
            //                                MultiSubnetFailover=False");
            optionsBuilder.UseSqlServer(@"Server=tcp:zigbeewm.database.windows.net,1433;Initial Catalog=ZigbeeDatabase;
                                        Persist Security Info=False;User ID=xardas663;
                                        Password=wojtek123!;MultipleActiveResultSets=False;Encrypt=True;
                                        TrustServerCertificate=False;Connection Timeout=30");
          
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<HumiditySensor>()
                .HasOne(r => r.Room)
                .WithMany(s => s.HumiditySensors)
                .HasForeignKey(s => s.RoomId)
                .OnDelete(DeleteBehavior.SetNull); 

            modelBuilder.Entity<TemperatureSensor>()
                .HasOne(r => r.Room)
                .WithMany(s => s.TemperatureSensors)
                .HasForeignKey(s => s.RoomId)
                .OnDelete(DeleteBehavior.SetNull);


            modelBuilder.Entity<Temperature>()
                .HasOne(s => s.TemperatureSensor)
                .WithMany(t => t.Temperatures)
                .HasForeignKey(t => t.TemperatureSensorId);

            modelBuilder.Entity<Humidity>()
               .HasOne(s => s.HumiditySensor)
               .WithMany(h => h.Humidity)
               .HasForeignKey(h => h.HumiditySensorId);
          
        }

        public DbSet<Temperature> Temperatures { get; set; }
        public DbSet<Humidity> Humidity { get; set; }
        public DbSet<TemperatureSensor> TemperatureSensors { get; set; }
        public DbSet<HumiditySensor> HumiditySensors { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Setting> Settings { get; set; }

    }
}
