using System.ComponentModel.DataAnnotations;

namespace eBookShop.Models;

public class User 
{
    [Key] public int Id { get; set; }
    public string Name { get; set; }
    [Required] public string Email { get; set; }
    public string Password { get; set; }
    public string AboutUser { get; set; }
    public List<Post> Posts { get; set; } = new();
    public virtual List<Book> LikedBooks { get; set; } = new();
    
    /// User must contain at least one Order
    public virtual List<Order> Orders { get; set; } = new();
}