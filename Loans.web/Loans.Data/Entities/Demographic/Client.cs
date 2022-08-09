using Loans.Data.BaseEntities;

namespace Loans.Data.Entities
{
    public class Client : BaseEntity
    {
        public string Name { get; set; }
        public string MidName { get; set; }
        public string LastName { get; set; }
        public int TitleId { get; set; }
        public int GenderId { get; set; }

        public Catalog GenderCatalog { get; set; }
        public Catalog TitleCatalog { get; set; }

        //public IList<ClientPhone> Phones { get; private set; } = new List<ClientPhone>();
        //public IList<ClientBusinessInfo> Businesses { get; private set; } = new List<ClientBusinessInfo>();
        //public IList<ClientLoan> Loans { get; private set; } = new List<ClientLoan>();

    }
}
