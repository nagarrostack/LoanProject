using Loans.BL.Configuration.Interfaces;

namespace Loans.Testing._2_BusinessLogic
{
    [TestClass]
    public class TypeCatalogsBLTest : BaseTest
    {
        [TestMethod]
        public async Task GetAllTypeCatalogs_TestAsync()
        {
            var service = serviceProvider.GetRequiredService<ITypeCatalogService>();
            var result = await service.GetAllTypeCatalogsAsync();

            Assert.IsNotNull(result);
            Assert.AreEqual(context.TypeCatalogs.Count(), result.Count);
        }

        [TestMethod,
            DataTestMethod,
            DataRow("Gender"),
            DataRow("Country"),
            DataRow("Other")]
        public async Task GetTypeCatalogsByName_TestAsync(string name)
        {
            var service = serviceProvider.GetRequiredService<ITypeCatalogService>();
            var result = await service.GetTypeCatalogsByNameAsync(name);

            Assert.IsNotNull(result);
            Assert.AreEqual(context.TypeCatalogs.Where(t => t.Name.ToUpper().Contains(name.ToUpper())).Count(), result.Count);
        }

        [TestMethod,
            DataTestMethod,
            DataRow(1),
            DataRow(4)]
        public async Task GetTypeCatalogsById_TestAsync(int id)
        {
            var service = serviceProvider.GetRequiredService<ITypeCatalogService>();
            var result = await service.GetTypeCatalogByIdAsync(id);

            Assert.IsNotNull(result);
            Assert.AreEqual(context.TypeCatalogs.First(t => t.Id == id).Name, result.Name);
        }

        [TestMethod,
            DataTestMethod,
            DataRow(10)]
        public async Task GetTypeCatalogsById_OutOfRange_TestAsync(int id)
        {
            var service = serviceProvider.GetRequiredService<ITypeCatalogService>();
            var result = await service.GetTypeCatalogByIdAsync(id);

            Assert.IsNull(result);
        }
    }
}
