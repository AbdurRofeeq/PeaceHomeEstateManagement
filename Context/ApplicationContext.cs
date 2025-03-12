using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PeaceHomeEstateManagement.Models;

namespace PeaceHomeEstateManagement.Context
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
        : base(options)
        { }
        public DbSet<Amenities> Amenities => Set<Amenities>();
        public DbSet<Property> Properties => Set<Property>();
        public DbSet<PropertyType> PropertyTypes => Set<PropertyType>();
        public DbSet<AmenitiesProperty> AmenitiesProperties => Set<AmenitiesProperty>();
        public DbSet<User> Users => Set<User>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            var user = new User
            {
                Id = Guid.NewGuid(),
                Email = "peacehome@gmail.com"
            };
            
            user.SetPassword("peace01"); 

            modelBuilder.Entity<User>().HasData(user);

             // Configure the many-to-many relationship
                modelBuilder.Entity<AmenitiesProperty>()
                    .HasKey(ap => new { ap.PropertyId, ap.AmenitiesId });

                modelBuilder.Entity<AmenitiesProperty>()
                    .HasOne(ap => ap.Property)
                    .WithMany(p => p.AmenitiesProperties)
                    .HasForeignKey(ap => ap.PropertyId);

                modelBuilder.Entity<AmenitiesProperty>()
                    .HasOne(ap => ap.Amenities)
                    .WithMany(a => a.AmenitiesProperties)
                    .HasForeignKey(ap => ap.AmenitiesId);
        }
    }
}