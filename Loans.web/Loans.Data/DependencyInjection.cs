using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Loans.Data
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DatabaseContext>(options => {
                if (Environment.GetEnvironmentVariable("DATABASE_CONNECTION") == null)
                    Environment.SetEnvironmentVariable("DATABASE_CONNECTION", @"data source=(LocalDB)\MSSQLLocalDB;initial catalog=LoansDB;integrated security=True;");
                options.UseSqlServer(Environment.GetEnvironmentVariable("DATABASE_CONNECTION")!);
            });

            return services;
        }
    }
}
