using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain
{
    public class CartItem
    {
        [Key]
        public int CartItemId { get; set; }
        [ForeignKey("Cart")]
        public int CartId { get; set; }
        public virtual Cart Cart { get; set; }
        [ForeignKey("ProductVariant")]
        public int ProductVariantId { get; set; }
        public virtual ProductVariant ProductVariant { get; set; }
        public int Quantity { get; set; }
    }
}