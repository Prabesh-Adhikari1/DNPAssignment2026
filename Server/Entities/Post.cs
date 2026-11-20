namespace Entities;

public class Post
{
    public int id { get; set; }
    public string title { get; set; }
    public string content { get; set; }
    public int userId { get; set; }
    
    //Navigation property
    public User user { get; set; } = null!;
    
    //Navigation property(Comments on this post)
    public ICollection<Comment>Comments { get; set; } = new List<Comment>();


}