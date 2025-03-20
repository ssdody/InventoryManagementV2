using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryManagementShared.Models
{
    public class Store
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        [RegularExpression(@"^[A-Za-z\s]+$", ErrorMessage = "Name can only contain alphabetic characters and spaces.")]
        public string Name { get; set; } = string.Empty;

        [StringLength(500)]
        [RegularExpression(@"^[A-Za-z0-9\s,.\-]+$", ErrorMessage = "Description can only contain alphanumeric characters, spaces, commas, periods, and hyphens.")]
        public string? Description { get; set; }

        // Navigation Property (One-to-Many Relationship)
        public ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
