//using System.ComponentModel.DataAnnotations;
//using System.ComponentModel.DataAnnotations.Schema;
//using InventoryManagementShared.Models

//namespace InventoryManagement.Models
//{
//    public class Product
//    {
//        [Key]
//        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
//        public int Id { get; set; }

//        [Required]
//        [StringLength(100)]
//        public string Name { get; set; } = string.Empty;

//        public string? Category { get; set; }
//        public string? Description { get; set; }

//        [Required]
//        [Range(0, int.MaxValue)]
//        [Column(TypeName = "decimal(18,2)")]
//        public int Quantity { get; set; }

//        [Required]
//        [Range(0, double.MaxValue)]
//        public decimal Price { get; set; }

//        // Foreign Key
//        public int StoreId { get; set; }
//        public Store? Store { get; set; }
//    }
//}
