using System.ComponentModel.DataAnnotations;

namespace CrudWebApi.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string? Name { get; set; }
        public string? Description { get; set; }
        [Required]
        [Range(0, int.MaxValue,ErrorMessage ="Please Provide a valid price value")]
        public decimal Price { get; set; }
        
        [Range(0, int.MaxValue, ErrorMessage = "Please Provide a valid price value")]
        public decimal Qty { get; set; }
        [Required]
        public Unit Unit { get; set; }
    }
}
