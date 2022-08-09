using Loans.Data.BaseEntities;

namespace Loans.Data.Entities
{
    public class TypeCatalog : BaseEntity
    {
        public string Name { get; set; }

        public IList<Catalog> Catalogs { get; private set; } = new List<Catalog>();
    }

}
