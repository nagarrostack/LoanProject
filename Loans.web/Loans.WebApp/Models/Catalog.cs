using System.ComponentModel.DataAnnotations;

namespace Loans.WebApp.Models
{
    public class Catalog
    {
        public int Id { get; set; }
        
        [Required]
        public int TypeCatalogId { get; set; }
        
        [Required,
            StringLength(200)]
        public string Name { get; set; }
    }
}
