using System.ComponentModel.DataAnnotations;

namespace eBookShop.Models;

public class User : ICloneable
{
    [Key] public int Id { get; set; }
    public string Name { get; set; }
    [Required] public string Email { get; set; }
    public string Password { get; set; }
    public virtual List<Book> LikedBooks { get; set; } = new();
    
    /// User must contain at least one Order
    public virtual List<Order> Orders { get; set; } = new();

    public object Clone()
    {
        var user = (User)MemberwiseClone();

        user.LikedBooks = new List<Book>(LikedBooks);

        user.Orders = new List<Order>(Orders);

        return user;
    }
}