using Loans.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Loans.Testing
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddDbContext<DatabaseContext>(opts =>
            {
                opts.UseInMemoryDatabase(Guid.NewGuid().ToString());
            });
            return services;
        }
    }
}
