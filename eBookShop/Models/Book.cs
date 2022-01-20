using System.ComponentModel.DataAnnotations;

namespace eBookShop.Models;

//TODO: implement cloning
public class Book : ICloneable
{
    [Key] public int Id { get; set; }
    public string Title { get; set; }
    public double Price { get; set; }
    public string Description { get; set; }
    public string Author { get; set; }
    public Uri CoverUrl { get; set; }
    public DateTime CreatedDateTime { get; set; }
    public double Stars { get; set; }
    public virtual List<Category> Categories { get; set; } = new();
    public virtual List<User> UsersWhoLike { get; set; } = new();
    public virtual List<Order> Orders { get; set; } = new();
    public object Clone()
    {
        var book = (Book)MemberwiseClone();

        book.Categories = new List<Category>(Categories);

        book.UsersWhoLike = new List<User>(UsersWhoLike);

        book.Orders = new List<Order>(Orders);

        return book;
    }
}