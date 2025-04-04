using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Product> Products { get; set; } = new List<Product>();
    }
}