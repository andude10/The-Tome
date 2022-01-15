using System.ComponentModel.DataAnnotations;

namespace eBookShop.Models;

public class User
{
    [Key] public int Id { get; set; }

    public string Name { get; set; }

    [Required] public string Email { get; set; }

    public string Password { get; set; }
    public virtual List<Order> Orders { get; set; } = new();
}