using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain
{
    public class Order
    {
        [Key]
        public string OrderId { get; set; }
        public virtual Address Address { get; set; }
        public DateTime OrderDate { get; set; }
        public string Status { get; set; }
        [ForeignKey("User")]
        public string UserId { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
        public virtual Payment Payment { get; set; }
    }
}