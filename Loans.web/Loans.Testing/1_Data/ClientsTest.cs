using Loans.Data.Entities;

namespace Loans.Testing._1_Data
{
    [TestClass]
    public class ClientsTest : BaseTest
    {
        [TestMethod]
        public void GetAllClients_Test()
        {
            var result = context.Clients.Count();
            Assert.AreNotEqual(0, result);
        }

        [TestMethod,
            DataTestMethod,
            DataRow(3, 1),
            DataRow(30, 1),
            DataRow(59, 1),
            DataRow(78, 1)]
        public void GetClientById_Test(int id, int expected)
        {
            var result = context.Clients.Count(c => c.Id == id);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void GetClientsByName_Test()
        {
            int count = 0;
            foreach (string[] names in femNames.Concat(maleNames))
            {
                foreach (string name in names)
                {
                    count += context.Clients.Count(c => c.Name.ToUpper().Contains(name.ToUpper()));
                }
            }
            Assert.AreNotEqual(0, count);
        }

        [TestMethod]
        public void GetClientsByCountryId_Test()
        {
            int count = 0;
            var countries = context.Catalogs.Where(c => c.TypeCatalogId == 3);
            foreach (var item in countries)
            {
                count += context.Clients.Count(c => c.CountryId == item.Id);
            }
            Assert.AreNotEqual(0, count);
        }

        [TestMethod,
            DataTestMethod,
            DataRow(1, 2)]
        public void GetClientPhones_Test(int idClient, int expected)
        {
            var clientphones = context.ClientPhones.Where(c => c.ClientId == idClient);
            Assert.AreEqual(expected, clientphones.Count());
        }

        [TestMethod,
            DataTestMethod,
            DataRow(101, 7, 1, 5, "Maria Jose", "H.", "Gomez", 101)]
        public void AddClient_Test(int id, int countryId, int genderId, int titleId, string name, string midName, string lastName, int expected)
        {
            var client = new Client { Id = id, CountryId = countryId, GenderId = genderId, TitleId = titleId, Name = name, MidName = midName, LastName = lastName};
            context.Clients.Add(client);
            context.SaveChanges();
            Assert.AreEqual(expected, context.Clients.Count());
        }

        [TestMethod,
            DataTestMethod,
            DataRow(201, 14, 14, 11, "222-222-2254", 201)]
        public void AddClientsPhone(int id, int clientId, int typeId, int code, string number, int expected)
        {
            var phone = new ClientPhone { Id = id, ClientId = clientId, TypePhoneId = typeId, CountryCodeId = code, Number = number};
            context.ClientPhones.Add(phone);
            context.SaveChanges();
            Assert.AreEqual(expected, context.ClientPhones.Count());            
        }

        [TestMethod,
            DataTestMethod,
            DataRow(5, 1, 199)]
        public void RemoveFromClientsPhones_Test(int id, int expectedPartialCount, int expected)
        {
            ClientPhone phone = context.ClientPhones.FirstOrDefault(p => p.Id == id);
            int clientId = phone.ClientId;
            context.ClientPhones.Remove(phone);
            context.SaveChanges();

            phone = context.ClientPhones.FirstOrDefault(p => p.Id == id);
            Assert.AreEqual(expected, context.ClientPhones.Count());
            Assert.AreEqual(expectedPartialCount, context.ClientPhones.Count(p => p.ClientId == clientId));
            Assert.IsNull(phone);
        }

        [TestMethod,
            DataTestMethod,
            DataRow(4, 99)]
        public void RemoveFromClient_Test(int id, int expected)
        {
            Client client = context.Clients.FirstOrDefault(c => c.Id == id);
            context.Clients.Remove(client);
            context.SaveChanges();
            client = context.Clients.FirstOrDefault(c => c.Id == id);

            Assert.AreEqual(expected, context.Clients.Count());
            Assert.IsNull(client);
        }
    }
}
