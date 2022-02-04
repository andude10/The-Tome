using System.ComponentModel.DataAnnotations;

namespace TheTome.Models;

public class Book
{
    [Key] public int Id { get; set; }
    public string Title { get; set; } = null!;
    public double Price { get; set; }
    public string Description { get; set; } = null!;
    public string Author { get; set; } = null!;
    public Uri CoverUrl { get; set; } = null!;
    public DateTime CreatedDateTime { get; set; }
    public double Stars { get; set; }
    public List<Category> Categories { get; set; } = new();
    public List<User> UsersWhoLike { get; set; } = new();
    public List<Order> Orders { get; set; } = new();
}