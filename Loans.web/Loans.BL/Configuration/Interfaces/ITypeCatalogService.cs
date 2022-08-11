using Loans.BL.Configuration.Dtos;

namespace Loans.BL.Configuration.Interfaces
{
    public interface ITypeCatalogService
    {
        Task<IList<TypeCatalogDto>> GetAllTypeCatalogsAsync();
        Task<IList<TypeCatalogDto>> GetTypeCatalogsByNameAsync(string name);
        Task<TypeCatalogDto> GetTypeCatalogByIdAsync(int id);
    }
}
