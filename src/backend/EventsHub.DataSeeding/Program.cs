using EventsHub.Common.Helpers;
using EventsHub.DAL.Entities;
using EventsHub.DAL.Entities.Concert;
using EventsHub.DAL.Entities.Film;
using EventsHub.DAL.Entities.Theatre;
using EventsHub.DAL.SQLServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace EventsHub.DataSeeding
{
    internal static class Program
    {
        private static async Task Main(string[] args)
        {
            using var dbContext = CreateDbContext();

            await dbContext.ClearAndSeed(await GetRoles());
            await dbContext.ClearAndSeed(await GetUsersWithRoles());
            await dbContext.ClearAndSeed(await GetTheatrePlays());
            await dbContext.ClearAndSeed(await GetConcerts());

            await dbContext.ClearAndSeed(await GetFilms());
            await dbContext.ClearAndSeed(await GetCinemas());
            await dbContext.ClearAndSeed(await GetSessions());

            Console.WriteLine("Completed");
        }

        private static DbContext CreateDbContext()
        {
            var configurationBuilder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json");

            var configuration = configurationBuilder.Build();

            var connectionString = configuration.GetConnectionString("SmarterAspConnectionString");

            var dbContextOptionsBuilder = new DbContextOptionsBuilder()
                .UseSqlServer(connectionString)
                .EnableSensitiveDataLogging(true)
                .EnableDetailedErrors(true);

            var dbContextOptions = dbContextOptionsBuilder.Options;
            return new SqlServerDbContext(dbContextOptions);
        }

        private static async Task<List<User>> GetUsersWithRoles()
        {
            var users = await Task.Run(() => JsonConvert.DeserializeObject<List<User>>(File.ReadAllText(@"seedings\users.json")));
            users.ForEach(u => u.PasswordHash = PasswordHasher.HashPassword(u.PasswordHash));

            var roles = await GetRoles();
            var adminRoleId = roles.ElementAt(0).RoleId;
            var userRoleId = roles.ElementAt(1).RoleId;

            users.ElementAt(0).RoleId = adminRoleId;
            users.ElementAt(1).RoleId = userRoleId;

            return users;
        }

        private static async Task<List<Role>> GetRoles()
        {
            return await Task.Run(() => JsonConvert.DeserializeObject<List<Role>>(File.ReadAllText(@"seedings\roles.json")));
        }

        private static async Task<List<TheatrePlay>> GetTheatrePlays()
        {
            return await Task.Run(() => JsonConvert.DeserializeObject<List<TheatrePlay>>(File.ReadAllText(@"seedings\theatrePlays.json")));
        }

        private static async Task<List<Concert>> GetConcerts()
        {
            return await Task.Run(() => JsonConvert.DeserializeObject<List<Concert>>(File.ReadAllText(@"seedings\concerts.json")));
        }

        private static async Task<List<Film>> GetFilms()
        {
            return await Task.Run(() => JsonConvert.DeserializeObject<List<Film>>(File.ReadAllText(@"seedings\films.json")));
        }

        private static async Task<List<Cinema>> GetCinemas()
        {
            return await Task.Run(() => JsonConvert.DeserializeObject<List<Cinema>>(File.ReadAllText(@"seedings\cinemas.json")));
        }

        private static async Task<List<Session>> GetSessions()
        {
            return await Task.Run(() => JsonConvert.DeserializeObject<List<Session>>(File.ReadAllText(@"seedings\sessions.json")));
        }

        private static async Task<List<Film>> GetFilmsWithInfo()
        {
            var sessions = await GetSessions();
            var cinemas = await GetCinemas();
            var films = await GetFilms();

            var f1 = films.ElementAt(0);
            var f2 = films.ElementAt(1);

            var c1 = cinemas.ElementAt(0);
            var c2 = cinemas.ElementAt(1);
            var c3 = cinemas.ElementAt(2);
            var c4 = cinemas.ElementAt(3);
            var c5 = cinemas.ElementAt(4);
            var c6 = cinemas.ElementAt(5);


            c1.Sessions = new List<Session>()
            {
                sessions.ElementAt(0),
                sessions.ElementAt(1),
                sessions.ElementAt(2)
            };
            c2.Sessions = new List<Session>()
            {
                sessions.ElementAt(3),
                sessions.ElementAt(4),
                sessions.ElementAt(5)
            };
            c3.Sessions = new List<Session>()
            {
                sessions.ElementAt(6),
                sessions.ElementAt(7),
                sessions.ElementAt(8)
            };
            c4.Sessions = new List<Session>()
            {
                sessions.ElementAt(9),
                sessions.ElementAt(10),
                sessions.ElementAt(11)
            };
            c5.Sessions = new List<Session>()
            {
                sessions.ElementAt(12),
                sessions.ElementAt(13)
            };
            c6.Sessions = new List<Session>()
            {
                sessions.ElementAt(14)
            };

            f1.Cinemas = new List<Cinema> { c1, c2, c3 };
            f2.Cinemas = new List<Cinema> { c4, c5, c6 };

            return films;
        }
    };
}
