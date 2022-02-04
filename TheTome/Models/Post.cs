using System.ComponentModel.DataAnnotations;

namespace TheTome.Models;

public class Post
{
    [Key] public int Id { get; set; }
    public string Title { get; set; } = null!;
    public string HtmlContent { get; set; } = null!;
    public int Rating { get; set; }
    public DateTime Date { get; init; } = DateTime.Now;
    public int UserId { get; set; }
    public User User { get; set; } = null!;
}