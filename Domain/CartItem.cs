using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain
{
    public class CartItem
    {
        [Key]
        public string CartItemId { get; set; }
        [ForeignKey("Cart")]
        public string CartId { get; set; }
        public virtual Cart Cart { get; set; }
        [ForeignKey("ProductVariant")]
        public string ProductVariantId { get; set; }
        public virtual ProductVariant ProductVariant { get; set; }
        public int Quantity { get; set; }
    }
}