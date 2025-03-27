using Microsoft.AspNetCore.Identity;

namespace Domain
{
    public class User: IdentityUser
    {
        //role
        public virtual ICollection<Address> Addresses { get; set; }
        public virtual ICollection<Cart> Carts { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}
