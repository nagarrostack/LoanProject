namespace Loans.WebApp.Models
{
    public class Client
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string MidName { get; set; }
        public string LastName { get; set; }
        public int TitleId { get; set; }
        public string Title { get;set; }
        public int GenderId { get; set; }
        public string Gender { get; set; }
        public int CountryId { get; set; }
        public string Country { get; set; }
    }
}
