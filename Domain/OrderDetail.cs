using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain
{
    public class OrderDetail
    {
        [Key]
        public string OrderDetailId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }

        [ForeignKey("Order")]
        public string OrderId { get; set; }
        public virtual Order Order { get; set; }

        [ForeignKey("ProductVariant")]
        public string ProductVariantId { get; set; }
        public virtual ProductVariant ProductVariant { get; set; }
    }
}