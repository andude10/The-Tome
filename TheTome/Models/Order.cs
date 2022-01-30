using System.ComponentModel.DataAnnotations;

namespace TheTome.Models;

public class Order
{
    [Key] public int Id { get; set; }
    public DateTime OrderDate { get; set; } = DateTime.Now;
    public bool IsCompleted { get; set; } = false;
    public int UserId { get; set; }
    public User User { get; set; }
    public List<Book> Books { get; set; } = new();
}