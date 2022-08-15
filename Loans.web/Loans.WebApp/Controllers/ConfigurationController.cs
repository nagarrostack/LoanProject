using Loans.BL.Configuration.Interfaces;
using Loans.WebApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace Loans.WebApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ConfigurationController : ControllerBase
    {
        private readonly ILogger<ConfigurationController> _logger;
        private readonly ICatalogService service;

        public ConfigurationController(ILogger<ConfigurationController> logger, ICatalogService catalogService)
        {
            _logger = logger;
            service = catalogService;
        }

        [HttpGet]
        public async Task<IEnumerable<Catalog>> GetAll()
        {
            var response = await service.GetAllCatalogsAsync();

            return response.Select(c => new Catalog
            {
                Id = c.Id,
                TypeCatalogId = c.TypeCatalogId,
                Name = c.Name
            }).ToList();
        }

        [HttpGet("ByType/{id}")]
        public async Task<IEnumerable<Catalog>> GetByTypeId(int id)
        {
            var response = await service.GetCatalogsByTypeCatalogIdAsync(id);

            return response.Select(c => new Catalog
            {
                Id = c.Id,
                TypeCatalogId = c.TypeCatalogId,
                Name = c.Name
            }).ToList();
        }
    }
}