using System.ComponentModel.DataAnnotations;

namespace eBookShop.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.Now;

        public int UserId { get; set; }
        public virtual User User { get; set; }

        public virtual List<Book> Books { get; set; } = new List<Book>();
    }
}