using AutoMapper;
using Loans.BL.BaseServices;
using Loans.BL.Configuration.Dtos;
using Loans.BL.Configuration.Interfaces;
using Loans.Data;
using Loans.Data.Entities;

namespace Loans.BL.Configuration.Services
{
    public class TypeCatalogService : BaseService, ITypeCatalogService
    {
        public TypeCatalogService(DatabaseContext databaseContext, IMapper mapper) : base(databaseContext, mapper) { }

        public async Task<IList<TypeCatalogDto>> GetAllTypeCatalogsAsync()
        {
            var result = context.TypeCatalogs.ToList();
            return mapper.Map<List<TypeCatalogDto>>(result);
        }

        public async Task<TypeCatalogDto> GetTypeCatalogByIdAsync(int id)
        {
            var result = context.TypeCatalogs.FirstOrDefault(t => t.Id == id);
            if (result != null)
                return mapper.Map<TypeCatalogDto>(result);
            else
                return null;
        }

        public async Task<IList<TypeCatalogDto>> GetTypeCatalogsByNameAsync(string name)
        {
            var result = context.TypeCatalogs.Where(t => t.Name.ToUpper().Contains(name.ToUpper())).ToList();
            return mapper.Map<List<TypeCatalogDto>>(result);
        }
    }
}
