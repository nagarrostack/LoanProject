namespace Loans.WebApp.Models
{
    public class FullEditLoan
    {
        public List<Catalog> TitleCatalog { get; set; }
        public List<Catalog> CountryCatalog { get; set; }
        public List<Catalog> GenderCatalog { get; set; }
        public Client ClientInfo { get; set; }
        public ClientBusiness BusinessInfo { get; set; }
        public Loan LoanInfo { get; set; }
    }
}
