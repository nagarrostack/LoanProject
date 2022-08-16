using System.ComponentModel.DataAnnotations;

namespace Loans.WebApp.Models
{
    public class ClientBusiness
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        
        [Required,
            StringLength(200, MinimumLength = 10)]
        public string Address { get; set; }
        
        [Required,
            StringLength(50)]
        public string Name { get; set; }
        
        [Required,
            DataType(DataType.PhoneNumber),
            StringLength(20),
            Display(Name = "Phone number")]
        public string PhoneNumber { get; set; }
        
        [Required,
            Display(Name = "Tax")]
        public int TaxId { get; set; }
        public Client Client { get; set; }
    }
}
