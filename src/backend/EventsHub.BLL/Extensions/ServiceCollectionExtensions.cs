using EventsHub.DAL.SQLServer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using EventsHub.DAL.MySQL;
using EventsHub.DAL.UnitOfWork;

namespace EventsHub.BLL.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddSqlServerDbContext(this IServiceCollection services, string connectionString)
        {
            return services.AddDbContext<SqlServerDbContext>(options => options.UseSqlServer(connectionString));
        }

        public static IServiceCollection AddMySqlDbContext(this IServiceCollection services, string connectionString)
        {
            return services.AddDbContext<MySqlDbContext>(options => options.UseSqlServer(connectionString));
        }

        public static IServiceCollection AddUnitOfWork(this IServiceCollection services)
        {
            return services.AddTransient<IUnitOfWork, UnitOfWork>(provider =>
                new UnitOfWork(provider.GetRequiredService<SqlServerDbContext>()));
        }
    }
}
