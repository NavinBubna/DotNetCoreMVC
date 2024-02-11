using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BulkyBooks.Models
{
    public class CategoryModel
    {
        [Key]   // Id or {className}Id is the default name for primary key. Other than that we can also use [Key] annotation
        public int Id { get; set; }
        [Required]
        [DisplayName("Category Name")]
        [MaxLength(20)]
        public string Name { get; set; }
        [DisplayName("Display Order")]
        [Range(1, 100, ErrorMessage = "The field Display Order must be between 1-100")]
        public int DisplayOrder { get; set; }
    }
}
