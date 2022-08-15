using Loans.BL.Client.Interfaces;
using Loans.web.Models;
using Microsoft.AspNetCore.Mvc;

namespace Loans.web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClientBusinessController : ControllerBase
    {
        private readonly ILogger<LoanController> _logger;
        private readonly IClientBusinessesService service;

        public ClientBusinessController(ILogger<LoanController> logger, IClientBusinessesService catalogService)
        {
            _logger = logger;
            service = catalogService;
        }

        [HttpGet]
        public async Task<IEnumerable<ClientBusiness>> GetAll()
        {
            var response = await service.GetAllClientBusinessesAsync();

            return response.Select(c => new ClientBusiness
            {
                Id = c.Id,
                Address = c.Address,
                ClientId = c.ClientId,
                PhoneNumber = c.PhoneNumber,
                TaxId = c.TaxId,
                Name = c.Name,
                Client = new Client
                {
                    Id = c.Client.Id,
                    CountryId = c.Client.CountryId,
                    GenderId = c.Client.GenderId,
                    TitleId = c.Client.TitleId,
                    LastName= c.Client.LastName,
                    MidName = c.Client.MidName,
                    Name = c.Client.Name,
                    Country = c.Client.CountryCatalog.Name,
                    Gender = c.Client.GenderCatalog.Name,
                    Title = c.Client.TitleCatalog.Name
                }
            }).ToList();
        }

        [HttpGet("/ByName/{name}")]
        public async Task<IEnumerable<ClientBusiness>> GetByName(string name)
        {
            var response = await service.GetClientBusinessByNameAsync(name);

            return response.Select(c => new ClientBusiness
            {
                Id = c.Id,
                Address = c.Address,
                ClientId = c.ClientId,
                PhoneNumber = c.PhoneNumber,
                TaxId = c.TaxId,
                Name = c.Name,
                Client = new Client
                {
                    Id = c.Client.Id,
                    CountryId = c.Client.CountryId,
                    GenderId = c.Client.GenderId,
                    TitleId = c.Client.TitleId,
                    LastName = c.Client.LastName,
                    MidName = c.Client.MidName,
                    Name = c.Client.Name,
                    Country = c.Client.CountryCatalog.Name,
                    Gender = c.Client.GenderCatalog.Name,
                    Title = c.Client.TitleCatalog.Name
                }
            }).ToList();
        }

        [HttpGet("/ByClient/{id}")]
        public async Task<IEnumerable<ClientBusiness>> GetByClientId(int id)
        {
            var response = await service.GetClientBusinessesByClientIdAsync(id);

            return response.Select(c => new ClientBusiness
            {
                Id = c.Id,
                Address = c.Address,
                ClientId = c.ClientId,
                PhoneNumber = c.PhoneNumber,
                TaxId = c.TaxId,
                Name = c.Name,
                Client = new Client
                {
                    Id = c.Client.Id,
                    CountryId = c.Client.CountryId,
                    GenderId = c.Client.GenderId,
                    TitleId = c.Client.TitleId,
                    LastName = c.Client.LastName,
                    MidName = c.Client.MidName,
                    Name = c.Client.Name,
                    Country = c.Client.CountryCatalog.Name,
                    Gender = c.Client.GenderCatalog.Name,
                    Title = c.Client.TitleCatalog.Name
                }
            }).ToList();
        }

        [HttpGet("/ById/{id}")]
        public async Task<ClientBusiness> GetById(int id)
        {
            var response = await service.GetClientBusinessByIdAsync(id);

            return new ClientBusiness
            {
                Id = response.Id,
                Address = response.Address,
                ClientId = response.ClientId,
                PhoneNumber = response.PhoneNumber,
                TaxId = response.TaxId,
                Name = response.Name,
                Client = new Client
                {
                    Id = response.Client.Id,
                    CountryId = response.Client.CountryId,
                    GenderId = response.Client.GenderId,
                    TitleId = response.Client.TitleId,
                    LastName = response.Client.LastName,
                    MidName = response.Client.MidName,
                    Name = response.Client.Name,
                    Country = response.Client.CountryCatalog.Name,
                    Gender = response.Client.GenderCatalog.Name,
                    Title = response.Client.TitleCatalog.Name
                }
            };
        }

        [HttpPost]
        public async Task<ClientBusiness> Post(ClientBusiness clientBusiness)
        {
            var response = await service.SaveClientBusinessAsync(new BL.Client.Dtos.ClientBusinessDto
            {
                Address = clientBusiness.Address,
                ClientId = clientBusiness.ClientId,
                PhoneNumber = clientBusiness.PhoneNumber,
                Id = 0,
                TaxId = clientBusiness.TaxId,
                Name = clientBusiness.Name
            });

            return new ClientBusiness
            {
                Id = response.Id,
                Address = response.Address,
                ClientId = response.ClientId,
                PhoneNumber = response.PhoneNumber,
                TaxId = response.TaxId,
                Name = response.Name,
                Client = new Client
                {
                    Id = response.Client.Id,
                    CountryId = response.Client.CountryId,
                    GenderId = response.Client.GenderId,
                    TitleId = response.Client.TitleId,
                    LastName = response.Client.LastName,
                    MidName = response.Client.MidName,
                    Name = response.Client.Name,
                    Country = response.Client.CountryCatalog.Name,
                    Gender = response.Client.GenderCatalog.Name,
                    Title = response.Client.TitleCatalog.Name
                }
            };

        }

        [HttpPut("/{id}")]
        public async Task<ClientBusiness> Put(int id, [FromBody] ClientBusiness clientBusiness)
        {
            if(id == clientBusiness.Id)
            {
                var response = await service.SaveClientBusinessAsync(new BL.Client.Dtos.ClientBusinessDto
                {
                    Address = clientBusiness.Address,
                    ClientId = clientBusiness.ClientId,
                    PhoneNumber = clientBusiness.PhoneNumber,
                    Id = clientBusiness.Id,
                    TaxId = clientBusiness.TaxId,
                    Name = clientBusiness.Name
                });

                return new ClientBusiness
                {
                    Id = response.Id,
                    Address = response.Address,
                    ClientId = response.ClientId,
                    PhoneNumber = response.PhoneNumber,
                    TaxId = response.TaxId,
                    Name = response.Name,
                    Client = new Client
                    {
                        Id = response.Client.Id,
                        CountryId = response.Client.CountryId,
                        GenderId = response.Client.GenderId,
                        TitleId = response.Client.TitleId,
                        LastName = response.Client.LastName,
                        MidName = response.Client.MidName,
                        Name = response.Client.Name,
                        Country = response.Client.CountryCatalog.Name,
                        Gender = response.Client.GenderCatalog.Name,
                        Title = response.Client.TitleCatalog.Name
                    }
                };
            }
            throw new ArgumentException("Client business's data doesn't match.");
        }

        [HttpDelete]
        public async Task<bool> Del(int id)
        {
            var response = await service.DeleteClientBusinessAsync(id);

            return response;
        }
    }
}