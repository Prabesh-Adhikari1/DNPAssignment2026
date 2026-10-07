namespace ApiContracts;

public class UpdateComment
{
    public int UserId { get; set; }
    public int PostId { get; set; }
    public required string Body { get; set; }
}