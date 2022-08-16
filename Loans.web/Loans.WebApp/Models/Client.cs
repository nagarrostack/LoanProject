using System.ComponentModel.DataAnnotations;

namespace Loans.WebApp.Models
{
    public class Client
    {
        public int Id { get; set; }
        
        [Required,
            StringLength(50)]
        public string Name { get; set; }
        
        [Required,
            StringLength(50),
            Display(Name="Mid name")]
        public string MidName { get; set; }
        
        [Required,
            StringLength(50),
            Display(Name = "Last name")]
        public string LastName { get; set; }
        
        [Required,
            Display(Name = "Title")]
        public int TitleId { get; set; }
        public string Title { get;set; }
        
        [Required,
            Display(Name = "Gender")]
        public int GenderId { get; set; }
        public string Gender { get; set; }
        
        [Required,
            Display(Name = "Country")]
        public int CountryId { get; set; }
        public string Country { get; set; }
    }
}
