namespace Loans.web.Models
{
    public class ClientBusiness
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public string Address { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public int TaxId { get; set; }
        public Client Client { get; set; }
    }
}
