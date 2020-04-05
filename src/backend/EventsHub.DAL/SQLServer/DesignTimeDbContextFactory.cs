using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace EventsHub.DAL.SQLServer
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<SqlServerDbContext>
    {
        public SqlServerDbContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();
            var builder = new DbContextOptionsBuilder<SqlServerDbContext>();
            var connectionString = configuration.GetConnectionString("SqlServerConnection");
            builder.UseSqlServer(connectionString);

            return new SqlServerDbContext(builder.Options);
        }
    }
}
