using System.ComponentModel.DataAnnotations;

namespace eBookShop.Models;

public class Category 
{
    [Key] public int Id { get; set; }
    [Required] public string Name { get; set; }
    public virtual List<Book> Books { get; set; } = new();
}