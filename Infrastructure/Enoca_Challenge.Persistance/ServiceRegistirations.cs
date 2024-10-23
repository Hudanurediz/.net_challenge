using Enoca_Challenge.Application.Abstractions;
using Enoca_Challenge.Persistance.Context;
using Enoca_Challenge.Persistance.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Enoca_Challenge.Persistance
{
    public static class ServiceRegistirations
    {
        public static void AddPersistenceService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("mssql")));
            services.AddScoped<ICarrierWriteRepository, CarrierWriteRepository>();
            services.AddScoped<ICarrierReadRepository, CarrierReadRepository>();
            services.AddScoped<ICarrierConfigurationReadRepository, CarrierConfigurationReadRepository>();
            services.AddScoped<ICarrierConfigurationWriteRepository, CarrierConfigurationWriteRepository>();
            services.AddScoped<IOrderReadRepository, OrderReadRepository>();
            services.AddScoped<IOrderWriteRepository, OrderWriteRepository>();
           
        }    
    }
}
