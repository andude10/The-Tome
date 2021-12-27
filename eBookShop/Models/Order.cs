namespace eBookShop.Models
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.Now;

        public int UserId { get; set; }
        public User User { get; set; }
        public List<Book> Books { get; set;}
    }
}