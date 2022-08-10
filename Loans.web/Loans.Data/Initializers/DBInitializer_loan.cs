using Loans.Data.Entities;
using Microsoft.EntityFrameworkCore;
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

                    if (!await context.ClientBusinessInfos.AnyAsync())
                    {
                        Random r = new Random();
                        int c = 0;
                        int k = 1;
                        for (int i = 1; i <= 100; i++)
                        {
                            var client = context.Clients.Find(i);
                            c = r.Next(6);
                            for (int j = 0; j < c; j++)
                            {
                                var loan = new ClientLoan { 
                                    Id = k,
                                    ClientId = client.Id,
                                    AmountRequest = r.Next(4_000,75_000),
                                    APR = (byte)r.Next(4,12),
                                    LoanDate = new DateTime(r.Next(2015, 2022), r.Next(1, 12), r.Next(1, 28)),
                                    Rating = r.Next(600,750)
                                };
                                if (loan.LoanDate.Year <= 2021)
                                {
                                    loan.QtyMonthsPayment = (int)((DateTime.Now - loan.LoanDate).TotalDays / 30);
                                    loan.LateLoans = r.Next(loan.QtyMonthsPayment / 4);
                                } else
                                {
                                    loan.QtyMonthsPayment = r.Next(12, 24);
                                    loan.LateLoans = 0;
                                }
                                decimal payment = (loan.AmountRequest / loan.QtyMonthsPayment);
                                int passedMonths = (int)((DateTime.Now - loan.LoanDate).TotalDays / 30);
                                loan.OutstandingDebt = loan.AmountRequest - (payment * passedMonths);
                                loans.Add(loan);
                                k++;
                            }
                        }


                        await InsertWithId(context, loans, "Loan");
                    }
                    //}
                }
            }
        }
    }
}
