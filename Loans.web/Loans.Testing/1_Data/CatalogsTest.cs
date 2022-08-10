using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loans.Testing._1_Data
{
    [TestClass]
    public class CatalogsTest : BaseTest
    {
        [TestMethod]
        public void GetTypeCatalog_Test()
        {
            var result = context.TypeCatalogs.Count();
            Assert.AreNotEqual(0, result);
        }

        [TestMethod]
        public void GetCatalog_Test()
        {
            var result = context.Catalogs.Count();
            Assert.AreNotEqual(0, result);
        }

        [TestMethod,
            DataTestMethod,
            DataRow(1, 2),
            DataRow(2, 3),
            DataRow(3, 4),
            DataRow(4, 4),
            DataRow(5, 3)]
        public void GetCatalogByType_Test(int id, int expected)
        {
            var result = context.Catalogs.Where(c => c.TypeCatalogId == id).Count();
            Assert.AreEqual(expected, result);
        }

        [TestMethod,
            DataTestMethod,
            DataRow(6, "Demo 1", 6)]
        public void AddCatalogType_Test(int id, string name, int expected)
        {
            context.TypeCatalogs.Add(new Data.Entities.TypeCatalog { Id = id, Name = name });
            context.SaveChanges();

            var result = context.TypeCatalogs.Count();
            Assert.AreEqual(expected, result);
        }

        [TestMethod,
            DataTestMethod,
            DataRow(5, "Demo 1", 1, 5)]
        public void AddCatalogDuplicatedId_Test(int id, string name, int typeId, int expected)
        {
            Assert.ThrowsException<InvalidOperationException>(() =>
            {
                context.Catalogs.Add(new Data.Entities.Catalog { Id = id, Name = name, TypeCatalogId = typeId });
                context.SaveChanges();
            });

            var result = context.TypeCatalogs.Count();
            Assert.AreEqual(expected, result);
        }

        [TestMethod,
            DataTestMethod,
            DataRow(6, "Demo 1", 6)]
        public void AddTwiceCatalogType_Test(int id, string name, int expected)
        {
            context.TypeCatalogs.Add(new Data.Entities.TypeCatalog { Id = id, Name = name });
            context.SaveChanges();

            Assert.ThrowsException<InvalidOperationException>(() =>
            {
                context.TypeCatalogs.Add(new Data.Entities.TypeCatalog { Id = id, Name = name });
                context.SaveChanges();
            });

            var result = context.TypeCatalogs.Count();
            Assert.AreEqual(expected, result);
        }

        [TestMethod,
            DataTestMethod,
            DataRow(17, "demo 1", 17)]
        public void AddCatalog_Test(int id, string name, int expected)
        {
            context.Catalogs.Add(new Data.Entities.Catalog { Id = id, Name = name });
            context.SaveChanges();

            var result = context.Catalogs.Count();
            Assert.AreEqual(expected, result);
        }

        [TestMethod,
            DataTestMethod,
            DataRow(1, 15)]
        public void RemoveFromCatalog_Test(int id, int expected)
        {
            var item = context.Catalogs.Find(id);
            context.Catalogs.Remove(item);
            context.SaveChanges();

            var result = context.Catalogs.Count();
            Assert.AreEqual(expected, result);

            var found = context.Catalogs.FirstOrDefault(c => c.Id == id);
            Assert.IsNull(found);
        }
    }
}
