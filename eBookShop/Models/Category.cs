using System.ComponentModel.DataAnnotations;

namespace eBookShop.Models;

//TODO: implement cloning
public class Category : ICloneable
{
    [Key] public int Id { get; set; }
    [Required] public string Name { get; set; }
    public virtual List<Book> Books { get; set; } = new();
    public object Clone()
    {
        var category = (Category)MemberwiseClone();

        category.Books = new List<Book>(Books);

        return category;
    }
}