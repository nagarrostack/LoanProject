using Microsoft.Extensions.DependencyInjection;

namespace Loans.BL
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            return services;
        }
    }
}