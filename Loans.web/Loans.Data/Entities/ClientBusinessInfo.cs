using Loans.Data.BaseEntities;

namespace Loans.Data.Entities
{
    public class ClientBusinessInfo : BaseClientEntity
    {
        public string Address { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public int TaxId { get; set; }

        public Catalog TaxCatalog { get; set; }
    }
}
