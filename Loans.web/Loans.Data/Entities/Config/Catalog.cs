using Loans.Data.BaseEntities;

namespace Loans.Data.Entities
{
    public class Catalog : BaseEntity
    {
        public int TypeCatalogId { get; set; }
        public string Name { get; set; }

        public TypeCatalog Type { get; set; }

        //public IList<Client> ClientGenders { get; private set; } = new List<Client>();
        //public IList<Client> ClientTitles { get; private set; } = new List<Client>();
        //public IList<ClientPhone> ClientPhoneTypes { get; private set; } = new List<ClientPhone>();
        //public IList<ClientPhone> CountryCodes { get; private set; } = new List<ClientPhone>();
    }
}
