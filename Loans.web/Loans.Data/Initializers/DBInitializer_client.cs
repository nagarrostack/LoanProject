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
                    //using (IDbContextTransaction transaction = context.Database.BeginTransaction())
                    //{
                    List<Client> clients = new List<Client>();
                    List<ClientPhone> clientPhones = new List<ClientPhone>();

                    string[][] femNames = {
                        new string[] { "Jane", "Mary", "Anna", "Emily", "Danna", "Rin" },
                        new string[] { "Maria", "Luisa", "Veronica", "Ana", "Mayte", "Miraldy" },
                        new string[] { "Lakshmi", "Sarika", "Uma", "Farida", "Sahara", "Divya"},
                        new string[] { "Emilia", "Hannah", "Sophia", "Mila", "Leah", "Klara" } };

                    string[][] maleNames = {
                        new string[] { "John", "Marty", "Alex", "Ernest", "Daniel", "Ron" },
                        new string[] { "Mario", "Luis", "Victor", "Anthony", "Malcom", "Marc" },
                        new string[] { "Aarav", "Aarush", "Bhavin", "Devansh", "Divij", "Faiyaz"},
                        new string[] { "Jurgen", "Karl", "Walter", "Klaus", "Uwe", "Stefan" } };

                    Random r = new Random(100);

                    for (int i = 0; i < 100; i++)
                    {
                        var client = new Client
                        {
                            Id = i + 1,
                            GenderId = (r.Next(100) % 2) + 1,
                            MidName = Convert.ToChar((r.Next(100) % 13) + 65).ToString(),
                            CountryId = (r.Next(100) % 4) + 6
                        };
                        if (client.GenderId == 1)
                        {
                            client.TitleId = (r.Next(100) % 2) + 4;
                            client.Name = femNames[client.CountryId - 6][r.Next(100) % 6];
                        }
                        else
                        {
                            client.TitleId = 3;
                            client.Name = maleNames[client.CountryId - 6][r.Next(100) % 6];
                        }
                        clients.Add(client);
                    }

                    await InsertWithId(context, clients, "Client");

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
                    //}
                }
            }
        }
    }
}
