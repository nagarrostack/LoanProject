using Loans.Data;
using Loans.Data.Entities;
using Microsoft.Extensions.DependencyInjection;

namespace Loans.Testing
{
    public abstract class BaseTest
    {
        protected ServiceCollection services;
        protected IServiceProvider serviceProvider;
        protected DatabaseContext? context;

        protected string[][] femNames = {
                new string[] { "Jane", "Mary", "Anna", "Emily", "Danna", "Rin" },
                new string[] { "Maria", "Luisa", "Veronica", "Ana", "Mayte", "Miraldy" },
                new string[] { "Lakshmi", "Sarika", "Uma", "Farida", "Sahara", "Divya"},
                new string[] { "Emilia", "Hannah", "Sophia", "Mila", "Leah", "Klara" } };

        protected string[][] maleNames = {
                new string[] { "John", "Marty", "Alex", "Ernest", "Daniel", "Ron" },
                new string[] { "Mario", "Luis", "Victor", "Anthony", "Malcom", "Marc" },
                new string[] { "Aarav", "Aarush", "Bhavin", "Devansh", "Divij", "Faiyaz"},
                new string[] { "Jurgen", "Karl", "Walter", "Klaus", "Uwe", "Stefan" } };

        [TestInitialize]
        public void Init()
        {
            services = new ServiceCollection();
            //services.AddApplication();
            services.AddInfrastructure();
            serviceProvider = services.BuildServiceProvider();
            context = serviceProvider.GetService<DatabaseContext>();

            Seed();
        }

        public void Seed()
        {
            var catalogTypes = new List<TypeCatalog>();
            var catalogs = new List<Catalog>();
            var clients = new List<Client>();
            var clientPhones = new List<ClientPhone>();
            var businessInfos = new List<ClientBusinessInfo>();
            var loans = new List<ClientLoan>();

            catalogTypes.AddRange(new TypeCatalog[]
            {
                new TypeCatalog{Id = 1, Name = "Gender"},
                new TypeCatalog{Id = 2, Name = "Title"},
                new TypeCatalog{Id = 3, Name = "Country"},
                new TypeCatalog{Id = 4, Name = "Country code"},
                new TypeCatalog{Id = 5, Name = "Phone type"}
            });

            catalogs.AddRange(new Catalog[]
            {
                new Catalog{Id =  1, Name = "Female", TypeCatalogId = 1},
                new Catalog{Id =  2, Name = "Male", TypeCatalogId = 1},
                new Catalog{Id =  3, Name = "Mr.", TypeCatalogId = 2},
                new Catalog{Id =  4, Name = "Mrs.", TypeCatalogId = 2},
                new Catalog{Id =  5, Name = "Ms.", TypeCatalogId = 2},
                new Catalog{Id =  6, Name = "US", TypeCatalogId = 3},
                new Catalog{Id =  7, Name = "Mexico", TypeCatalogId = 3},
                new Catalog{Id =  8, Name = "India", TypeCatalogId = 3},
                new Catalog{Id =  9, Name = "Germany", TypeCatalogId = 3},
                new Catalog{Id = 10, Name = "+1", TypeCatalogId = 4},
                new Catalog{Id = 11, Name = "+52", TypeCatalogId = 4},
                new Catalog{Id = 12, Name = "+91", TypeCatalogId = 4},
                new Catalog{Id = 13, Name = "+49", TypeCatalogId = 4},
                new Catalog{Id = 14, Name = "Local phone number", TypeCatalogId = 5},
                new Catalog{Id = 15, Name = "Cell phone number", TypeCatalogId = 5},
                new Catalog{Id = 16, Name = "Office phone number", TypeCatalogId = 5}
            });

            Random r = new Random(100);

            // Clients
            for (int i = 0; i < 100; i++)
            {
                var client = new Client
                {
                    Id = i + 1,
                    GenderId = (r.Next(100) % 2) + 1,
                    MidName = Convert.ToChar((r.Next(100) % 13) + 65).ToString(),
                    LastName = String.Empty,
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

            // Clients' phones
            for (int i = 0; i < 100; i++)
            {
                var clientPhone1 = new ClientPhone { Id = (i * 2) + 1, ClientId = i, CountryCodeId = clients[i].CountryId + 4, TypePhoneId = (r.Next(100) % 3) + 10, Number = $"{r.Next(9)}{r.Next(9)}{r.Next(9)}-{r.Next(9)}{r.Next(9)}{r.Next(9)}-{r.Next(9)}{r.Next(9)}{r.Next(9)}{r.Next(9)}" };
                var clientPhone2 = new ClientPhone { Id = (i * 2) + 2, ClientId = i, CountryCodeId = clients[i].CountryId + 4, TypePhoneId = (r.Next(100) % 3) + 10, Number = $"{r.Next(9)}{r.Next(9)}{r.Next(9)}-{r.Next(9)}{r.Next(9)}{r.Next(9)}-{r.Next(9)}{r.Next(9)}{r.Next(9)}{r.Next(9)}" };
                clientPhones.Add(clientPhone1);
                clientPhones.Add(clientPhone2);
            }

            context.TypeCatalogs.AddRange(catalogTypes);
            context.Catalogs.AddRange(catalogs);
            context.Clients.AddRange(clients);
            context.ClientPhones.AddRange(clientPhones);

            context.SaveChanges();

            // Clients businesses
            for (int i = 1; i <= 45; i++)
            {
                var client = context.Clients.Find(i);
                var businessInfo = new ClientBusinessInfo
                {
                    Id = i,
                    ClientId = r.Next(100),
                    Address = $"{i} Street #{i}",
                    Name = $"{client.Name}'s Busness demo {client.Id}",
                    PhoneNumber = $"555-444-{client.Id.ToString().PadLeft(4, '0')}",
                    TaxId = 1
                };
                businessInfos.Add(businessInfo);
            }

            // Clients' loans
            int c = 0;
            int k = 1;
            for (int i = 1; i <= 100; i++)
            {
                var client = context.Clients.Find(i);
                c = r.Next(6);
                for (int j = 0; j < c; j++)
                {
                    var loan = new ClientLoan
                    {
                        Id = k,
                        ClientId = client.Id,
                        AmountRequest = r.Next(4_000, 75_000),
                        APR = (byte)r.Next(4, 12),
                        LoanDate = new DateTime(r.Next(2015, 2022), r.Next(1, 12), r.Next(1, 28)),
                        Rating = r.Next(600, 750)
                    };
                    if (loan.LoanDate.Year <= 2021)
                    {
                        loan.QtyMonthsPayment = (int)((DateTime.Now - loan.LoanDate).TotalDays / 30);
                        loan.LateLoans = r.Next(loan.QtyMonthsPayment / 4);
                    }
                    else
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

            context.ClientBusinessInfos.AddRange(businessInfos);
            context.ClientLoans.AddRange(loans);

            context.SaveChanges();

        }
    }
}