using System.ComponentModel.DataAnnotations;

namespace eBookShop.Models;

public class Post
{
    [Key] public int Id { get; set; }
    public string Title { get; set; }
    public string HtmlContent { get; set; }
    public DateTime Date { get; set; } = DateTime.Now;
    public int UserId { get; set; }
    public User User { get; set; }
}