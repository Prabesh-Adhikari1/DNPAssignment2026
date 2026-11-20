namespace Entities;

public class Comment
{
    public int id { get; set; }
    public int userId { get; set; }
    public int postId { get; set; }
    public string body{ get; set; }
    
    // The user who wrote this comment
    public User User { get; set; } = null!;

    // The post this comment belongs to
    public Post Post { get; set; } = null!;
    
}