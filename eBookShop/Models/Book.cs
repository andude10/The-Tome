using System.ComponentModel.DataAnnotations;

namespace eBookShop.Models
{
    public class Book
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public double Price { get; set; }
        public string Description { get; set;}
        public string Author { get; set;}
        public DateTime CreatedDateTime { get; set; }

        public List<Category> Categories { get; set; }
        public List<Order> Orders { get; set; }
    }
}
