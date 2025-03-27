using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class Address
    {
        [Key]
        public string AddressId { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string District { get; set; }
        public virtual User User { get; set; }
    }
}