//using System.Net.NetworkInformation;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OneShop.Application.Common.Interfaces;
using OneShop.Persistence.Persistence;
using OneShop.Persistence.Services;

namespace OneShop.Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DatabaseConnection");
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(connectionString, b => b.MigrationsAssembly("OneShop.Persistence"));
                options.EnableSensitiveDataLogging();
            });
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            return services;
        }
    }
}