namespace Entities;

public class User
{
    
    public int id { get; set; }
    public string username { get; set; }
    public string password { get; set; }

    //one user can create multiple posts
    public ICollection<Post> Posts { get; set; } = new List<Post>();
    
    //one User can write multiple comments
    public ICollection<Comment> Comments { get; set; } = new List<Comment>();

}