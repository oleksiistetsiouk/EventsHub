using EventsHub.DAL.SQLServer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using EventsHub.DAL.MySQL;

namespace EventsHub.BLL.Infrastructure
{
    public static class BllServiceCollectionExtensions
    {
        public static IServiceCollection AddSqlServerDbContext(this IServiceCollection services, string connectionString)
        {
            return services.AddDbContext<SqlServerDbContext>(options => options.UseSqlServer(connectionString));
        }

        public static IServiceCollection AddMySqlDbContext(this IServiceCollection services, string connectionString)
        {
            return services.AddDbContext<MySqlDbContext>(options => options.UseSqlServer(connectionString));
        }
    }
}
