using System.ComponentModel.DataAnnotations;

namespace eBookShop.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        public string Password { get; set; }
        public List<Order> Orders { get; set; } = new List<Order>();
    }
}