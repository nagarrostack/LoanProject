using Loans.BL.BaseDtos;

namespace Loans.BL.Configuration.Dtos
{
    public class CatalogDto : BaseDto
    {
        public int TypeCatalogId { get; set; }
        public string Name { get; set; }
    }
}
