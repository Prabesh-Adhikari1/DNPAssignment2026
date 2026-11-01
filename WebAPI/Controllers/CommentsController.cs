using ApiContracts;
using Entities;
using Microsoft.AspNetCore.Mvc;
using RepositoryContracts;

namespace WebAPI.Controllers;
[ApiController]
[Route("[controller]")]
public class CommentsController:ControllerBase
{
    private readonly ICommentRepository commentRepository;
    private readonly IUserRepository userRepository;

    public CommentsController(
        ICommentRepository commentRepository,
        IUserRepository userRepository)
    {
        this.commentRepository = commentRepository;
        this.userRepository = userRepository;
    }
    private static CommentDto ToDto(Comment comment)
    {
        return new CommentDto
        {
            Id = comment.id,
            UserId = comment.userId,
            PostId = comment.postId,
            Body = comment.body
        };
    }
    [HttpGet]
    public ActionResult<IQueryable<CommentDto>> GetMany(
        [FromQuery] int? userId,
        [FromQuery] string? userName,
        [FromQuery] int? postId)
    {
        IQueryable<Comment> comments = commentRepository.GetMany();

        if (userId.HasValue)
        {
            comments = comments.Where(comment =>
                comment.userId == userId.Value);
        }

        if (!string.IsNullOrEmpty(userName))
        {
            var matchingUserIds = userRepository.GetMany()
                .Where(user => user.username.Contains(userName))
                .Select(user => user.id)
                .ToList();

            comments = comments.Where(comment =>
                matchingUserIds.Contains(comment.userId));
        }

        if (postId.HasValue)
        {
            comments = comments.Where(comment =>
                comment.postId == postId.Value);
        }

        return Ok(comments.Select(ToDto).ToList());
    }

 
    
    [HttpGet("{id}")]
    public async Task<ActionResult<CommentDto>> GetSingle(int id)
    {
        Comment? comment = await commentRepository.GetSingleAsync(id);
        if (comment is null)
        {
            return NotFound();
        }
        return Ok(ToDto(comment));
    }

    [HttpPost]
    public async Task<ActionResult<CommentDto>> AddComment(
        [FromBody] CreateCommentDto request)
    {
        Comment comment = new Comment
        {
            userId = request.UserId,
            postId = request.PostId,
            body = request.Body
        };

        Comment createdComment =
            await commentRepository.AddAsync(comment);
        CommentDto dto = ToDto(createdComment);

        return Created($"/Comments/{dto.Id}", dto);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateComment(
        int id,
        [FromBody] UpdateComment request)
    {
        Comment comment = new Comment
        {
            id = id,
            userId = request.UserId,
            postId = request.PostId,
            body = request.Body
        };

        await commentRepository.UpdateAsync(comment);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteComment(int id)
    {
        await commentRepository.DeleteAsync(id);

        return NoContent();
    }
    
}