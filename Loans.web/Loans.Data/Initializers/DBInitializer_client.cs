using Loans.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;

namespace Loans.Data.Initializers
{
    // This partial class contains data initialization for Client and Client phone type
    public partial class DBInitializer
    {
        public async Task SeedClient()
        {
            using (IServiceScope serviceScope = _scopeFactory.CreateScope())
            {
                using (var context = serviceScope.ServiceProvider.GetService<DatabaseContext>())
                {
                    using (IDbContextTransaction transaction = context.Database.BeginTransaction())
                    {
                        List<Client> clients = new List<Client>();
                        List<ClientPhone> clientPhones = new List<ClientPhone>();

                        if (!await context.Clients.AnyAsync())
                        {
                            clients.AddRange(new Client[] { 
                                new Client{ Id = 1, Name = "John", MidName="A.", LastName="Bell", TitleId = 3, GenderId = 2 },
                                new Client{ Id = 2, Name = "Jack", MidName="B.", LastName="Anderson", TitleId = 3, GenderId = 2 },
                                new Client{ Id = 3, Name = "Jane", MidName="C.", LastName="Gates", TitleId = 4, GenderId = 1 }
                            });

                            await InsertWithId(context, clients, "Client");
                        }

                        if (!await context.ClientPhones.AnyAsync())
                        {
                            clientPhones.AddRange(new ClientPhone[] { 
                                new ClientPhone{ Id = 1, ClientId = 1, CountryCodeId = 6, TypePhoneId = 10, Number ="222222221"},
                                new ClientPhone{ Id = 2, ClientId = 1, CountryCodeId = 6, TypePhoneId = 11, Number ="222222222"},
                                new ClientPhone{ Id = 3, ClientId = 2, CountryCodeId = 6, TypePhoneId = 10, Number ="222222223"},
                                new ClientPhone{ Id = 4, ClientId = 2, CountryCodeId = 6, TypePhoneId = 11, Number ="222222224"},
                                new ClientPhone{ Id = 5, ClientId = 3, CountryCodeId = 6, TypePhoneId = 10, Number ="222222225"},
                                new ClientPhone{ Id = 6, ClientId = 3, CountryCodeId = 6, TypePhoneId = 11, Number ="222222226"}
                            });

                            await InsertWithId(context, clientPhones, "ClientPhone");
                        }
                    }
                }
            }
        }
    }
}
