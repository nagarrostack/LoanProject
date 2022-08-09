using Loans.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;

namespace Loans.Data.Initializers
{
    public partial class DBInitializer
    {
        public async Task SeedLoan()
        {
            using (IServiceScope serviceScope = _scopeFactory.CreateScope())
            {
                using (var context = serviceScope.ServiceProvider.GetService<DatabaseContext>())
                {
                    //using (IDbContextTransaction transaction = context.Database.BeginTransaction())
                    //{
                        List<ClientLoan> loans = new List<ClientLoan>();

                        if (!await context.ClientLoans.AnyAsync())
                        {
                            loans.AddRange(new ClientLoan[]
                            {
                                new ClientLoan{ Id = 1, ClientId = 1, AmountRequest = 1, APR = 4, OutstandingDebt = 25000, LateLoans = 0, QtyMonthsPayment = 12, Rating = 600 }
                            });
                            await InsertWithId(context, loans, "Loan");
                        }
                    //}
                }
            }
        }
    }
}
