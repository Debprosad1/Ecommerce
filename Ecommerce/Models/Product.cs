using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Discription { get; set; }
        public int Price { get; set; }
        public string? Image { get; set; }
      
        [Display(Name = "Available")]
        public bool IsAvailable { get; set; }

        [Display(Name = "Category")]
        [Required]
        public int categoryId { get; set; }
        [ForeignKey("categoryId")]
        public virtual Category category { get; set; }
        

    }
}
