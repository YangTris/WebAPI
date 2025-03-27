using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain
{
    public class Cart
    {
        [Key]
        public string CartId { get; set; }
        [ForeignKey("User")]
        public string UserId { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<CartItem> CartItems { get; set; }
    }
}