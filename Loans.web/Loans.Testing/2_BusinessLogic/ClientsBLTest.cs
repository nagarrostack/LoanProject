using Loans.BL.Client.Interfaces;

namespace Loans.Testing._2_BusinessLogic
{
    [TestClass]
    public class ClientsBLTest : BaseTest
    {
        [TestMethod]
        public async Task GetAllClients_TestAsync()
        {
            var service = serviceProvider.GetRequiredService<IClientsService>();
            var result = await service.GetAllClientsAsync();

            Assert.IsNotNull(result);
            Assert.AreEqual(context.Clients.Count(), result.Count);
        }

        [TestMethod,
            DataTestMethod,
            DataRow(14),
            DataRow(5),
            DataRow(75)]
        public async Task GetClientById_TestAsync(int id)
        {
            var service = serviceProvider.GetRequiredService<IClientsService>();
            var result = await service.GetClientByIdAsync(id);

            Assert.IsNotNull(result);
            Assert.AreEqual(context.Clients.First(c => c.Id == id).Name, result.Name);
        }

        [TestMethod,
            DataTestMethod,
            DataRow(14),
            DataRow(5),
            DataRow(75)]
        public async Task GetClientsByName_TestAsync(int id)
        {
            var service = serviceProvider.GetRequiredService<IClientsService>();

            var _client = await service.GetClientByIdAsync(id);
            var result = await service.GetClientByNameAsync(_client.Name);

            Assert.IsNotNull(result);
            Assert.AreEqual(context.Clients.Count(c => c.Name.Equals(_client.Name)), result.Count);
        }

        [TestMethod]
        public async Task GetClientByCountryId_TestAsync()
        {
            var service = serviceProvider.GetRequiredService<IClientsService>();

            var countries = context.TypeCatalogs.First(t => t.Name == "Country").Catalogs.ToList();
            int id = countries.First().Id;
            var result = await service.GetClientByCountryIdAsync(id);

            Assert.IsNotNull(result);
            Assert.AreEqual(context.Clients.Count(c => c.CountryId == id), result.Count);
        }

        [TestMethod]
        public async Task GetClientByGenderId_TestAsync()
        {
            var service = serviceProvider.GetRequiredService<IClientsService>();
            var genders = context.TypeCatalogs.First(t => t.Name == "Gender").Catalogs.ToList();
            int id = genders.First().Id;
            var result = await service.GetClientByGenderIdAsync(id);

            Assert.IsNotNull(result);
            Assert.AreEqual(context.Clients.Count(c => c.GenderId == id), result.Count);
        }

        [TestMethod,
            DataTestMethod,
            DataRow(0, 7, 2, 3, "Efrain", "S.", "Gracia")]
        public async Task SaveNewClient_TestAsync(int id, int countryId, int genderId, int titleId, string name, string midName, string lastName)
        {
            var service = serviceProvider.GetRequiredService<IClientsService>();

            var count = (await service.GetAllClientsAsync()).Count();

            var result = await service.SaveClientAsync(new BL.Client.Dtos.ClientDto
            {
                Id = id,
                CountryId = countryId,
                GenderId = genderId,
                LastName = lastName,
                MidName = midName,
                Name = name,
                TitleId = titleId
            });

            var returns = (await service.GetClientByNameAsync(name)).First();

            var newCount = (await service.GetAllClientsAsync()).Count();

            Assert.IsNotNull(result);
            Assert.AreEqual(result.Name, returns.Name);
            Assert.AreNotEqual(0, returns.Id);
            Assert.AreNotEqual(count, newCount);
        }

        [TestMethod,
            DataTestMethod,
            DataRow(14, 7, 2, 3, "Efrain", "S.", "Gracia")]
        public async Task SaveClientExists_TestAsync(int id, int countryId, int genderId, int titleId, string name, string midName, string lastName)
        {
            var service = serviceProvider.GetRequiredService<IClientsService>();

            var count = (await service.GetAllClientsAsync()).Count();

            var result = await service.SaveClientAsync(new BL.Client.Dtos.ClientDto
            {
                Id = id,
                CountryId = countryId,
                GenderId = genderId,
                LastName = lastName,
                MidName = midName,
                Name = name,
                TitleId = titleId
            });

            var returns = (await service.GetClientByNameAsync(name)).First();

            var newCount = (await service.GetAllClientsAsync()).Count();

            Assert.IsNotNull(result);
            Assert.AreEqual(result.Name, returns.Name);
            Assert.AreEqual(id, returns.Id);
            Assert.AreEqual(count, newCount);
        }

        [TestMethod,
            DataTestMethod,
            DataRow(23)]
        public async Task DeleteClientWithAssociated_TestAsync(int id)
        {
            var service = serviceProvider.GetRequiredService<IClientsService>();
            var count = (await service.GetAllClientsAsync()).Count();

            var result = await service.DeleteClientAsync(id);

            var newCount = (await service.GetAllClientsAsync()).Count();

            var returns = await service.GetClientByIdAsync(id);

            Assert.IsFalse(result);
            Assert.IsNotNull(returns);
            Assert.AreEqual(count, newCount);
        }

        [TestMethod,
            DataTestMethod,
            DataRow(0),
            DataRow(110)]
        public async Task DeleteClientDoesntExist_TestAsync(int id)
        {
            var service = serviceProvider.GetRequiredService<IClientsService>();
            var count = (await service.GetAllClientsAsync()).Count();

            Assert.ThrowsException<AggregateException>(() => {
                var r = service.DeleteClientAsync(id);
                r.Wait();
                });

            var newCount = (await service.GetAllClientsAsync()).Count();
            var returns = await service.GetClientByIdAsync(id);

            Assert.IsNull(returns);
            Assert.AreEqual(count, newCount);
        }
    }
}
