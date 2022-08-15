using Loans.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;

namespace Loans.Data.Initializers
{
    // This partial class contains data initialization for TypeCatalog and Catalog
    public partial class DBInitializer
    {
        public async Task SeedConfiguration()
        {
            using (IServiceScope serviceScope = _scopeFactory.CreateScope())
            {
                using (var context = serviceScope.ServiceProvider.GetService<DatabaseContext>())
                {
                    //using (IDbContextTransaction transaction = context.Database.BeginTransaction())
                    //{
                        List<TypeCatalog> typeCatalogs = new List<TypeCatalog>();
                        List<Catalog> catalogs = new List<Catalog>();

                        if (!await context.TypeCatalogs.AnyAsync())
                        {
                            typeCatalogs.AddRange(new TypeCatalog[] {
                                new TypeCatalog{Id = 1, Name = "Gender"},
                                new TypeCatalog{Id = 2, Name = "Title"},
                                new TypeCatalog{Id = 3, Name = "Country"},
                                new TypeCatalog{Id = 4, Name = "Country code"},
                                new TypeCatalog{Id = 5, Name = "Phone type"}
                            });

                            await InsertWithId(context, typeCatalogs, "TypeCatalog");
                        }

                        if (!await context.Catalogs.AnyAsync())
                        {
                            catalogs.AddRange(new Catalog[] {
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

                            await InsertWithId(context, catalogs, "Catalog");
                        }
                    //}
                }
            }
        }
    }
}
