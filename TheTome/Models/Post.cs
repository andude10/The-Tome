using System.ComponentModel.DataAnnotations;

namespace TheTome.Models;

public class Post
{
    [Key] public int Id { get; set; }
    public string Title { get; set; }
    public string HtmlContent { get; set; }
    public int Rating { get; set; }
    public DateTime Date { get; init; } = DateTime.Now;
    public int UserId { get; set; }
    public User User { get; set; }
}