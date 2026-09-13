namespace Entities;

public class Comment
{
    public int id { get; set; }
    public int userId { get; set; }
    public int postId { get; set; }
    public string body{ get; set; }
    
}