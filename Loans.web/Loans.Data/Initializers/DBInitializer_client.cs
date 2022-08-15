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
                        int countryId = (r.Next(100) % 4) + 6;
                        int genderId = (r.Next(100) % 2) + 1;
                        var client = new Client
                        {
                            Id = i + 1,
                            GenderId = genderId,
                            GenderCatalog = context.Catalogs.Find(genderId),
                            MidName = Convert.ToChar((r.Next(100) % 13) + 65).ToString(),
                            LastName = "",
                            CountryId = countryId,
                            CountryCatalog = context.Catalogs.Find(countryId)
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
                        client.TitleCatalog = context.Catalogs.Find(client.TitleId);

                        clients.Add(client);
                    }

                    await InsertWithId(context, clients, "Client");

                    if (!await context.ClientPhones.AnyAsync())
                    {
                        for (int i = 0; i < 100; i++)
                        {
                            var clientPhone1 = new ClientPhone { Id = (i * 2) + 1, ClientId = i, CountryCodeId = clients[i].CountryId + 4, TypePhoneId = (r.Next(100) % 3) + 10, Number = $"{r.Next(9)}{r.Next(9)}{r.Next(9)}-{r.Next(9)}{r.Next(9)}{r.Next(9)}-{r.Next(9)}{r.Next(9)}{r.Next(9)}{r.Next(9)}" };
                            var clientPhone2 = new ClientPhone { Id = (i * 2) + 2, ClientId = i, CountryCodeId = clients[i].CountryId + 4, TypePhoneId = (r.Next(100) % 3) + 10, Number = $"{r.Next(9)}{r.Next(9)}{r.Next(9)}-{r.Next(9)}{r.Next(9)}{r.Next(9)}-{r.Next(9)}{r.Next(9)}{r.Next(9)}{r.Next(9)}" };
                            clientPhones.Add(clientPhone1);
                            clientPhones.Add(clientPhone2);
                        }

                        context.ClientPhones.AddRange(clientPhones);

                        await InsertWithId(context, clientPhones, "ClientPhone");
                    }
                    //}
                }
            }
        }
    }
}
