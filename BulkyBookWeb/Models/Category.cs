using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BulkyBookWeb.Models
{
    public class Category       
    {
        [Key]
        public int Id { get; set; }
        [Required]  
        public string? Name { get; set; }
        [DisplayName("Category")]
        [Range(1, 50, ErrorMessage = "Display order must be between 1 and 50 degits")]
        public int DisplayOrder { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.Now; 
    }
}
