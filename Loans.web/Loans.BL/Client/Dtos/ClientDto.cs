using Loans.BL.BaseDtos;
using Loans.BL.Configuration.Dtos;

namespace Loans.BL.Client.Dtos
{
    public class ClientDto : BaseDto
    {
        public string Name { get; set; }
        public string MidName { get; set; }
        public string LastName { get; set; }
        //public string FullName
        //{
        //    get
        //    {
        //        return $"{Name} {MidName} {LastName}";
        //    }
        //}
        public int TitleId { get; set; }
        public int GenderId { get; set; }
        public int CountryId { get; set; }

        public CatalogDto CountryCatalog { get; set; }
        public CatalogDto TitleCatalog { get; set; }
        public CatalogDto GenderCatalog { get; set; }
    }
}
