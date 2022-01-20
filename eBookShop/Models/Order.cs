using System.ComponentModel.DataAnnotations;

namespace eBookShop.Models;

//TODO: implement cloning
public class Order : ICloneable
{
    [Key] public int Id { get; set; }
    public DateTime OrderDate { get; set; } = DateTime.Now;
    public bool IsCompleted { get; set; } = false;
    public int UserId { get; set; }
    public virtual User User { get; set; }
    public virtual List<Book> Books { get; set; } = new();
    public object Clone()
    {
        var order = (Order)MemberwiseClone();

        order.User = (User)User.Clone();

        order.Books = new List<Book>(Books);

        return order;
    }
}