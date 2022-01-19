using System.ComponentModel.DataAnnotations;

namespace eBookShop.Models;

//TODO: implement cloning
public class Order
{
    [Key] public int Id { get; set; }
    public DateTime OrderDate { get; set; } = DateTime.Now;
    public bool IsCompleted { get; set; } = false;
    public int UserId { get; set; }
    public virtual User User { get; set; }
    public virtual List<Book> Books { get; set; } = new();
}