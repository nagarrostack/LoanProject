using Loans.BL.Configuration.Dtos;

namespace Loans.BL.Configuration.Interfaces
{
    public interface ICatalogService
    {
        Task<IList<CatalogDto>> GetAllCatalogsAsync();
        Task<IList<CatalogDto>> GetCatalogsByNameAsync(string name);
        Task<IList<CatalogDto>> GetCatalogsByTypeCatalogIdAsync(int typeCatalogId);
        Task<CatalogDto> GetCatalogByIdAsync(int id);
    }
}
