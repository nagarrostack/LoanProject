using FluentValidation.AspNetCore;
using Loans.BL.Client.Interfaces;
using Loans.BL.Client.Services;
using Loans.BL.Configuration.Interfaces;
using Loans.BL.Configuration.Services;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Loans.BL
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services
                .AddTransient<ITypeCatalogService, TypeCatalogService>()
                .AddTransient<ICatalogService, CatalogService>()
                .AddTransient<IClientsService, ClientsService>()
                //.AddTransient<IClientBusinessesService, IClientBusinessesService>()
                //.AddTransient<IClientPhonesService, ClientPhonesService>()
                ;

            services.AddFluentValidation(options =>
            {
                options.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            });

            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            return services;
        }
    }
}