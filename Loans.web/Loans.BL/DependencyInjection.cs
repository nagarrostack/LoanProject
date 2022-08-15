using FluentValidation.AspNetCore;
using Loans.BL.Client.Interfaces;
using Loans.BL.Client.Services;
using Loans.BL.Configuration.Interfaces;
using Loans.BL.Configuration.Services;
using Loans.BL.Loan.Interfaces;
using Loans.BL.Loan.Services;
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
                .AddTransient<IClientBusinessesService, ClientBusinessesService>()
                .AddTransient<IClientPhonesService, ClientPhonesService>()
                .AddTransient<ILoanService, LoanService>()
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