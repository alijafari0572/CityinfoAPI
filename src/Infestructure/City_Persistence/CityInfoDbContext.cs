using City_Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace City_Persistence
{
    public class CityInfoDbContext:DbContext
    {
        public CityInfoDbContext
           (DbContextOptions<CityInfoDbContext> options)
           : base(options)
        {

        }
        public DbSet<City> Cities { get; set; } = null!;
        public DbSet<PointOfInterest> PointOfInterest { get; set; } = null!;

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlite();
        //    base.OnConfiguring(optionsBuilder);
        //}
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<City>()
                .HasData(
                new City("Tehran")
                {
                    Id = 1,
                    Description = "This is Tehran"
                },
                 new City("Shiraz")
                 {
                     Id = 2,
                     Description = "This is Shiraz"
                 },
                  new City("Tabriz")
                  {
                      Id = 3,
                      Description = "This is Tabriz"
                  }
                );
            modelBuilder.Entity<PointOfInterest>()
                .HasData(
                new PointOfInterest("Academy Barnamenevisan")
                {
                    Id = 1,
                    CityId = 1,
                    Description = "Tell 02188454816"
                },
                 new PointOfInterest("Shemiran")
                 {
                     Id = 2,
                     CityId = 1,
                     Description = "This is Shemiran"
                 },
                   new PointOfInterest("Meydan ToopKhoone")
                   {
                       Id = 3,
                       CityId = 1,
                       Description = "This is ToopKhoone"
                   }
                );


            base.OnModelCreating(modelBuilder);
        }
    }
}
