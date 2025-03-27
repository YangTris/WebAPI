using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain
{
    public class ProductVariant
    {
        [Key]
        public string ProductVariantId { get; set; }
        public string Color { get; set; }
        public string Size { get; set; }
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
        [ForeignKey("Product")]
        public string ProductId { get; set; }
        public virtual Product Product { get; set; }
    }
}