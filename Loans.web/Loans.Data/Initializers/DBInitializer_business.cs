using Loans.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Loans.Data.Initializers
{
    public partial class DBInitializer
    {
        public async Task SeedBusiness()
        {
            using (IServiceScope serviceScope = _scopeFactory.CreateScope())
            {
                using (var context = serviceScope.ServiceProvider.GetService<DatabaseContext>())
                {
                    //using (IDbContextTransaction transaction = context.Database.BeginTransaction())
                    //{
                    List<ClientBusinessInfo> businessInfos = new List<ClientBusinessInfo>();

                    if (!await context.ClientBusinessInfos.AnyAsync())
                    {
                        Random r = new Random(100);

                        for (int i = 1; i <= 45; i++)
                        {
                            var client = context.Clients.Find(r.Next(1,100));
                            var businessInfo = new ClientBusinessInfo
                            {
                                Id = i,
                                ClientId = client.Id,
                                Address = $"{i} Street #{i}",
                                Name = $"{client.Name}'s Busness demo {client.Id}",
                                PhoneNumber = $"555-444-{client.Id.ToString().PadLeft(4, '0')}",
                                TaxId = 1,
                                Client = client
                            };
                            businessInfos.Add(businessInfo);
                        }
                        await InsertWithId(context, businessInfos, "Loan");
                    }
                    //}
                }
            }
        }
    }
}
