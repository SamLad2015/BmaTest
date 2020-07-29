using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using BmaTestApi.Entities;

namespace BmaTestApi.Repositories
{
    public class TestDbContext : DbContext
    {
        public TestDbContext(DbContextOptions<TestDbContext> options)
           : base(options)
        {

        }
        public DbSet<CuisineEntity> CuisineEntities { get; set; }
        public DbSet<RestaurantEntity> RestaurantEntities { get; set; }
        public DbSet<RestaurantCuisineEntity> RestaurantCuisineEntities { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CuisineEntity>()
                .Property(c => c.Id)
                .IsRequired();
            modelBuilder.Entity<RestaurantEntity>()
                .Property(r => r.Id)
                .IsRequired();
            modelBuilder.Entity<RestaurantCuisineEntity>()
                .HasKey(rc => new {rc.CuisineId, rc.RestaurantId});

            modelBuilder.Entity<RestaurantCuisineEntity>()
                .HasOne<RestaurantEntity>(rc => rc.RestaurantEntity)
                .WithMany(r => r.Cuisine)
                .HasForeignKey(rc => rc.RestaurantId);


            modelBuilder.Entity<RestaurantCuisineEntity>()
                .HasOne<CuisineEntity>(rc => rc.CuisineEntity)
                .WithMany(c => c.Cuisine)
                .HasForeignKey(rc => rc.CuisineId);
            
            modelBuilder.Entity<CuisineEntity>()
                .HasData(
                    new CuisineEntity
                    {
                        Id = 1,
                        Name = "Asian"
                    },
                    new CuisineEntity
                    {
                        Id = 2,
                        Name = "Friendly"
                    },
                    new CuisineEntity
                    {
                        Id = 3,
                        Name = "Healthy"
                    },
                    new CuisineEntity
                    {
                        Id = 4,
                        Name = "Japanese"
                    },
                    new CuisineEntity
                    {
                        Id = 5,
                        Name = "British"
                    },
                    new CuisineEntity
                    {
                        Id = 6,
                        Name = "Fast food"
                    },
                    new CuisineEntity
                    {
                        Id = 7,
                        Name = "Indian"
                    },
                    new CuisineEntity
                    {
                        Id = 8,
                        Name = "Bar"
                    },
                    new CuisineEntity
                    {
                        Id = 9,
                        Name = "Steakhouse"
                    },
                    new CuisineEntity
                    {
                        Id = 10,
                        Name = "Argentinian"
                    },
                    new CuisineEntity
                    {
                        Id = 11,
                        Name = "Latin"
                    },
                    new CuisineEntity
                    {
                        Id = 12,
                        Name = "Spanish"
                    },
                    new CuisineEntity
                    {
                        Id = 13,
                        Name = "Cuban"
                    },
                    new CuisineEntity
                    {
                        Id = 14,
                        Name = "Pub"
                    }
                );
            modelBuilder.Entity<RestaurantEntity>()
                .HasData(
                    new RestaurantEntity
                    {
                        Id =  1,
                        Name = "Yokohama",
                        Address =  "331 Roundhay Road, Harehills, Leeds LS8 4HT",
                        FamilyFriendly = false,
                        VeganOptions = true,
                        Rating = 5
                    },
                    new RestaurantEntity
                    {
                        Id =  2,
                        Name = "Falafel Guys",
                        Address =  "50 Briggate, Leeds, LS1 6HD",
                        FamilyFriendly = false,
                        VeganOptions = true,
                        Rating = 5
                    },
                    new RestaurantEntity
                    {
                        Id =  3,
                        Name = "Bengal Brasserie",
                        Address =  "65 Haddon Road, Burley, Leeds, LS4 2JE",
                        FamilyFriendly = false,
                        VeganOptions = true,
                        Rating = 4
                    }
                );
            modelBuilder.Entity<RestaurantCuisineEntity>().HasData(
            new RestaurantCuisineEntity
            {
                RestaurantId = 1, CuisineId = 1
            },
            new RestaurantCuisineEntity
            {
                RestaurantId = 1, CuisineId = 3
            },
            new RestaurantCuisineEntity
            {
                RestaurantId = 1, CuisineId = 4
            },
            new RestaurantCuisineEntity
            {
                RestaurantId = 2, CuisineId = 3
            },
            new RestaurantCuisineEntity
            {
                RestaurantId = 2, CuisineId = 5
            },
            new RestaurantCuisineEntity
            {
                RestaurantId = 2, CuisineId = 6
            },
            new RestaurantCuisineEntity
            {
                RestaurantId = 3, CuisineId = 7
            },
            new RestaurantCuisineEntity
            {
                RestaurantId = 3, CuisineId = 8
            },
            new RestaurantCuisineEntity
            {
                RestaurantId = 3, CuisineId = 1
            });
        }

    }
}
