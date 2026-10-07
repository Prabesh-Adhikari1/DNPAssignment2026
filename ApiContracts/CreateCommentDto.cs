namespace ApiContracts;

public class CreateCommentDto
{
    public int UserId { get; set; }
    public int PostId { get; set; }
    public required string Body { get; set; }
}