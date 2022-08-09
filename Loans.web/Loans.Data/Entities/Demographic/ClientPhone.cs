using Loans.Data.BaseEntities;

namespace Loans.Data.Entities
{
    public class ClientPhone : BaseClientEntity
    {
        public int TypePhoneId { get; set; }
        public string Number { get; set; }
        public int CountryCodeId { get; set; }

        public Catalog TypePhoneCatalog { get; set; }
        public Catalog CountryCodeCatalog { get; set; }
    }
}
