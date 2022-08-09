using Loans.Data.BaseEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loans.Data.Initializers
{
    public partial class DBInitializer : IDBInitializer
    {
        private readonly IServiceScopeFactory _scopeFactory;

        public DBInitializer(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }


        public async Task Initialize()
        {
            using var serviceScope = _scopeFactory.CreateScope();

            using var context = serviceScope.ServiceProvider.GetService<DatabaseContext>();
            await context!.Database.MigrateAsync();
        }

        private async Task InsertWithId<T>(DatabaseContext context, List<T> list, string tableName) where T : BaseEntity
        {
            await context.Database.ExecuteSqlRawAsync($"SET IDENTITY_INSERT {tableName} ON;");
            context.AddRange(list);
            await context.SaveChangesAsync();

            await context.Database.ExecuteSqlRawAsync($"SET IDENTITY_INSERT {tableName} OFF;");
            await context.SaveChangesAsync();
        }
    }
}
