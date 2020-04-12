using EventsHub.Common.Helpers;
using EventsHub.DAL.Entities;
using EventsHub.DAL.Entities.Theatre;
using EventsHub.DAL.SQLServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EventsHub.DataSeeding
{
    internal static class Program
    {
        private static async Task Main(string[] args)
        {
            using var dbContext = CreateDbContext();
            var adminRoleId = new Guid("f4cbff0f-4bc0-42a4-9738-8d9f9bb734ba");
            var userRoleId = new Guid("50245ab3-6770-4269-ac26-30a942116a70");
            var guestRoleId = new Guid("7e9b3674-e720-4d50-939b-93ce8e8b1c44");

            var adminId = new Guid("f5aa0c0e-b669-466c-825a-2c52c58a019b");
            var memberId = new Guid("3bcec608-e6a4-45e5-b3b2-cdf252a639de");
            var guestId = new Guid("29cf2d1c-6328-4861-a718-6a6bcc984337");

            var roles = new List<Role>()
            {
                new Role() { Id = adminRoleId, Name = "Admin" },
                new Role() { Id = userRoleId, Name = "User" },
                new Role() { Id = guestRoleId, Name = "Guest" }
            };

            var users = new List<User>()
            {
                new User()
                {
                    Id = adminId, Email = "admin@gmail.com",
                    PasswordHash = PasswordHasher.HashPassword("password"), RoleId = adminRoleId
                },
                new User
                {
                    Id = memberId, Email = "user@gmail.com",
                    PasswordHash = PasswordHasher.HashPassword("password"), RoleId = userRoleId
                }
            };

            var theatrePlays = new List<TheatrePlay>()
            {
                new TheatrePlay()
                {
                    Name ="Romeo and Julietta",
                    Description ="Very sad story",
                    Place ="Operniy teatre",
                    PriceFrom = 120,
                    PriceTo = 190
                },
                new TheatrePlay
                {
                    Name ="Gamlet",
                    Description ="You mast watch it!",
                    Place ="Operniy teatre",
                    PriceFrom = 110,
                    PriceTo = 210
                }
            };

            await dbContext.ClearAndSeed(roles);
            await dbContext.ClearAndSeed(users);
            await dbContext.ClearAndSeed(theatrePlays);

            Console.WriteLine("Completed");
        }

        private static DbContext CreateDbContext()
        {

            var configurationBuilder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json");

            var configuration = configurationBuilder.Build();

            var connectionString = configuration.GetConnectionString("SmarterAspConnectionString");

            var dbContextOptionsBuilder = new DbContextOptionsBuilder()
                .UseSqlServer(connectionString);
            var dbContextOptions = dbContextOptionsBuilder.Options;

            return new SqlServerDbContext(dbContextOptions);
        }
    };
}
