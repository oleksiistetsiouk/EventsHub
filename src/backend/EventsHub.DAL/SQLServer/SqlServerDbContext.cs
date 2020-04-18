using EventsHub.DAL.Configurations;
using EventsHub.DAL.Entities;
using EventsHub.DAL.Entities.Concert;
using EventsHub.DAL.Entities.Film;
using EventsHub.DAL.Entities.Theatre;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace EventsHub.DAL.SQLServer
{
    public class SqlServerDbContext : DbContext
    {
        public SqlServerDbContext(DbContextOptions options) : base(options)
        {
        }

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Concert> Concerts { get; set; }
        public virtual DbSet<Cinema> Cinemas { get; set; }
        public virtual DbSet<Film> Films { get; set; }
        public virtual DbSet<Session> Sessions { get; set; }
        public virtual DbSet<TheatrePlay> TheatrePlays { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new SessionConfiguration());
            modelBuilder.ApplyConfiguration(new CinemaConfiguration());
            modelBuilder.ApplyConfiguration(new FilmConfiguration());
            modelBuilder.ApplyConfiguration(new RoleConfiguration());
        }
    }
}
