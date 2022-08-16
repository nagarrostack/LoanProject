using Loans.BL.Client.Interfaces;
using Loans.web.Models;
using Microsoft.AspNetCore.Mvc;

namespace Loans.web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClientController : ControllerBase
    {
        private readonly ILogger<ClientController> _logger;
        private readonly IClientsService service;

        public ClientController(ILogger<ClientController> logger, IClientsService clientService)
        {
            _logger = logger;
            service = clientService;
        }

        [HttpGet]
        public async Task<IEnumerable<Client>> Get()
        {
            var response = await service.GetAllClientsAsync();

            return response.Select(c => new Client
            {
                Id = c.Id,
                CountryId = c.CountryId,
                Country = c.CountryCatalog.Name,
                GenderId = c.GenderId,
                Gender = c.GenderCatalog.Name,
                TitleId = c.TitleId,
                Title = c.TitleCatalog.Name,
                LastName = c.LastName,
                MidName = c.MidName,
                Name = c.Name
            }).ToList();
        }

        [HttpGet("ById/{id}")]
        public async Task<Client> GetById(int id)
        {
            var response = await service.GetClientByIdAsync(id);

            return new Client
            {
                Id = response.Id,
                CountryId = response.CountryId,
                Country = response.CountryCatalog.Name,
                GenderId = response.GenderId,
                Gender = response.GenderCatalog.Name,
                TitleId = response.TitleId,
                Title = response.TitleCatalog.Name,
                LastName = response.LastName,
                MidName = response.MidName,
                Name = response.Name
            };
        }

        [HttpGet("ByName/{name}")]
        public async Task<IEnumerable<Client>> GetByName(string name)
        {
            var response = await service.GetClientByNameAsync(name);

            return response.Select(c => new Client
            {
                Id = c.Id,
                CountryId = c.CountryId,
                Country = c.CountryCatalog.Name,
                GenderId = c.GenderId,
                Gender = c.GenderCatalog.Name,
                TitleId = c.TitleId,
                Title = c.TitleCatalog.Name,
                LastName = c.LastName,
                MidName = c.MidName,
                Name = c.Name
            }).ToList();
        }

        [HttpGet("ByGender/{idGender}")]
        public async Task<IEnumerable<Client>> GetByGender(int idGender)
        {
            var response = await service.GetClientByGenderIdAsync(idGender);

            return response.Select(c => new Client
            {
                Id = c.Id,
                CountryId = c.CountryId,
                Country = c.CountryCatalog.Name,
                GenderId = c.GenderId,
                Gender = c.GenderCatalog.Name,
                TitleId = c.TitleId,
                Title = c.TitleCatalog.Name,
                LastName = c.LastName,
                MidName = c.MidName,
                Name = c.Name
            }).ToList();
        }

        [HttpGet("ByCountry/{idCountry}")]
        public async Task<IEnumerable<Client>> GetByCountry(int idCountry)
        {
            var response = await service.GetClientByCountryIdAsync(idCountry);

            return response.Select(c => new Client
            {
                Id = c.Id,
                CountryId = c.CountryId,
                Country = c.CountryCatalog.Name,
                GenderId = c.GenderId,
                Gender = c.GenderCatalog.Name,
                TitleId = c.TitleId,
                Title = c.TitleCatalog.Name,
                LastName = c.LastName,
                MidName = c.MidName,
                Name = c.Name
            }).ToList();
        }

        [HttpPost]
        public async Task<Client> Post([FromBody] Client client)
        {
            var response = await service.SaveClientAsync(new BL.Client.Dtos.ClientDto
            {
                GenderId = client.GenderId,
                CountryId = client.CountryId,
                TitleId = client.TitleId,
                Id = 0,
                LastName = client.LastName,
                MidName = client.MidName,
                Name = client.Name

            });

            return new Client
            {
                Id = response.Id,
                CountryId = response.CountryId,
                Country = response.CountryCatalog.Name,
                GenderId = response.GenderId,
                Gender = response.GenderCatalog.Name,
                TitleId = response.TitleId,
                Title = response.TitleCatalog.Name,
                LastName = response.LastName,
                MidName = response.MidName,
                Name = response.Name
            };
        }

        [HttpPut("{id}")]
        public async Task<Client> Put(int id, [FromBody] Client client)
        {
            if (id == client.Id)
            {
                var response = await service.SaveClientAsync(new BL.Client.Dtos.ClientDto
                {
                    Id = client.Id,
                    LastName = client.LastName,
                    MidName = client.MidName,
                    Name = client.Name,
                    GenderId = client.GenderId,
                    CountryId = client.CountryId,
                    TitleId = client.TitleId
                });

                return new Client
                {
                    Id = response.Id,
                    CountryId = response.CountryId,
                    Country = response.CountryCatalog.Name,
                    GenderId = response.GenderId,
                    Gender = response.GenderCatalog.Name,
                    TitleId = response.TitleId,
                    Title = response.TitleCatalog.Name,
                    LastName = response.LastName,
                    MidName = response.MidName,
                    Name = response.Name
                };
            }
            throw new ArgumentException("Client's data doesn't match.");
        }

        [HttpDelete("Delete/{id}")]
        public async Task<bool> Del(int id)
        {
            var response = await service.DeleteClientAsync(id);

            return response;
        }
    }
}