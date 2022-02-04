using System.ComponentModel.DataAnnotations;

namespace TheTome.Models;

public class User
{
    [Key] public int Id { get; set; }
    public string Name { get; set; } = null!;
    [Required] public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string? AboutUser { get; set; }
    public List<Post> Posts { get; set; } = new();
    public List<Book> LikedBooks { get; set; } = new();

    public Uri Photo { get; set; } =
        new(
            "https://lun-eu.icons8.com/a/vc5_tdnfq0CP24l39ZX83A/ZhzxdnO4l0K6FIsUH1ZIyA/%D1%80%D0%B0%D0%B7%D0%BC%D1%8B%D1%88%D0%BB%D1%8F%D1%8F.png");

    /// User must contain at least one Order
    public List<Order> Orders { get; set; } = new();
}