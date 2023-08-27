using System.Reflection;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using OneShop.Application.Interfaces;
using OneShop.Application.Services;

namespace OneShop.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddScoped<IVendorService, VendorService>();

            return services;
        }
    }
}
