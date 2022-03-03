using System.ComponentModel.DataAnnotations;

namespace TheTome.Core.Models;

public class Category
{
    [Key] public int Id { get; set; }
    [Required] public string Name { get; set; } = null!;
    public List<Book> Books { get; set; } = new();
}