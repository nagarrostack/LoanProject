using AutoMapper;
using Loans.BL.BaseServices;
using Loans.BL.Configuration.Dtos;
using Loans.BL.Configuration.Interfaces;
using Loans.Data;

namespace Loans.BL.Configuration.Services
{
    internal class CatalogService : BaseService, ICatalogService
    {
        public CatalogService(DatabaseContext databaseContext, IMapper mapper) : base(databaseContext, mapper) { }

        public async Task<IList<CatalogDto>> GetAllCatalogsAsync()
        {
            var result = context.Catalogs.ToList();
            return mapper.Map<List<CatalogDto>>(result);
        }

        public async Task<CatalogDto> GetCatalogByIdAsync(int id)
        {
            var result = context.Catalogs.FirstOrDefault(c => c.Id == id);
            return mapper.Map<CatalogDto>(result);
        }

        public async Task<IList<CatalogDto>> GetCatalogsByNameAsync(string name)
        {
            var result = context.Catalogs.Where(c => c.Name.ToUpper().Contains(name.ToUpper())).ToList();
            return mapper.Map<List<CatalogDto>>(result);
        }

        public async Task<IList<CatalogDto>> GetCatalogsByTypeCatalogIdAsync(int typeCatalogId)
        {
            var result = context.Catalogs.Where(c => c.TypeCatalogId == typeCatalogId).ToList();
            return mapper.Map<List<CatalogDto>>(result);
        }
    }
}
