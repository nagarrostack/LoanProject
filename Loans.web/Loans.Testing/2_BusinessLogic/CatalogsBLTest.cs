using Loans.BL.Configuration.Interfaces;

namespace Loans.Testing._2_BusinessLogic
{
    [TestClass]
    public class CatalogsBLTest : BaseTest
    {
        [TestMethod]
        public async Task GetAllCatalogs_TestAsync()
        {
            var service = serviceProvider.GetRequiredService<ICatalogService>();
            var result = await service.GetAllCatalogsAsync();

            Assert.IsNotNull(result);
            Assert.AreEqual(context.Catalogs.Count(), result.Count);
        }

        [TestMethod,
            DataTestMethod,
            DataRow("phone"),
            DataRow("Female"),
            DataRow("India"),
            DataRow("Mr"),
            DataRow("Other")]
        public async Task GetCatalogsByName_TestAsync(string name)
        {
            var service = serviceProvider.GetRequiredService<ICatalogService>();
            var result = await service.GetCatalogsByNameAsync(name);

            Assert.IsNotNull(result);
            Assert.AreEqual(context.Catalogs.Where(c => c.Name.ToUpper().Contains(name.ToUpper())).Count(), result.Count);
        }

        [TestMethod,
            DataTestMethod,
            DataRow(1),
            DataRow(5),
            DataRow(9)]
        public async Task GetCatalogById_TestAsync(int id)
        {
            var service = serviceProvider.GetRequiredService<ICatalogService>();
            var result = await service.GetCatalogByIdAsync(id);

            Assert.IsNotNull(result);
            Assert.AreEqual(context.Catalogs.First(c => c.Id == id).Name, result.Name);
        }

        [TestMethod,
            DataTestMethod,
            DataRow(19),
            DataRow(-5)]
        public async Task GetCatalogById_OutOfRange_TestAsync(int id)
        {
            var service = serviceProvider.GetRequiredService<ICatalogService>();
            var result = await service.GetCatalogByIdAsync(id);

            Assert.IsNull(result);
        }

        [TestMethod,
            DataTestMethod,
            DataRow(1),
            DataRow(4)]
        public async Task GetCatalogsByTypeCatalogId_TestAsync(int typeId)
        {
            var service = serviceProvider.GetRequiredService<ICatalogService>();
            var result = await service.GetCatalogsByTypeCatalogIdAsync(typeId);

            Assert.IsNotNull(result);
            Assert.AreEqual(context.Catalogs.Count(c => c.TypeCatalogId == typeId), result.Count);
        }

        [TestMethod,
            DataTestMethod,
            DataRow(8)]
        public async Task GetCatalogsByTypeCatalogId_OutOfRange_TestAsync(int typeId)
        {
            var service = serviceProvider.GetRequiredService<ICatalogService>();
            var result = await service.GetCatalogsByTypeCatalogIdAsync(typeId);

            Assert.IsNotNull(result);
            Assert.AreEqual(context.Catalogs.Count(c => c.TypeCatalogId == typeId), result.Count);
        }
    }
}
