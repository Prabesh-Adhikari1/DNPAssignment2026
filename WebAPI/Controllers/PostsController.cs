using ApiContracts;
using Entities;
using Microsoft.AspNetCore.Mvc;
using RepositoryContracts;

namespace WebAPI.Controllers;
[ApiController]
[Route("[controller]")]
public class PostsController: ControllerBase
{
  private readonly IPostRepository PostRepository;
  private readonly IUserRepository userRepository;
  public PostsController(IPostRepository PostRepository,
    IUserRepository userRepository)
  {
    this.PostRepository = PostRepository;
    this.userRepository = userRepository;
  }
  private static PostDto ToDto(Post post)
  {
    return new PostDto
    {
      Id = post.id,
      Title = post.title,
      Content = post.content,
      UserId = post.userId
    };
  }

  [HttpPost]
  public async Task<ActionResult<Post>> Post([FromBody] CreatePostDto
    request)
  {
    Post post = new Post
    {
      title = request.Title,
      content = request.Content,
      userId = request.UserId
    };
    Post createdPost = await PostRepository.AddAsync(post);
    PostDto dto= ToDto(createdPost);
    return Created($"/Posts/{dto.Id}", dto);
  }
  [HttpPut("{id}")]
  public async Task<ActionResult> UpdatePost(
    int id,
    [FromBody] UpdatePostDto request)
  {
    Post post = new Post
    {
      id = id,
      title = request.Title,
      content = request.Content,
      userId = request.UserId
    };

    await PostRepository.UpdateAsync(post);

    return NoContent();
  }
  
  [HttpGet("{id}")]
  public async Task<ActionResult<Post>> GetSingle(int id)
  {
    Post? post = await PostRepository.GetSingleAsync(id);

    if (post is null)
    {
      return NotFound();
    }
    return Ok(ToDto(post));
  }
  
  [HttpGet]
  public ActionResult<IQueryable<PostDto>> GetMany(
    [FromQuery] string? title,
    [FromQuery] int? userId,
    [FromQuery] string? userName)
  {
    IQueryable<Post> posts = PostRepository.GetMany();

    if (!string.IsNullOrEmpty(title))
    {
      posts = posts.Where(post =>
        post.title.Contains(title));
    }

    if (userId.HasValue)
    {
      posts = posts.Where(post =>
        post.userId == userId.Value);
    }

    if (!string.IsNullOrEmpty(userName))
    {
      var matchingUserIds = userRepository.GetMany()
        .Where(user => user.username.Contains(userName))
        .Select(user => user.id)
        .ToList();

      posts = posts.Where(post =>
        matchingUserIds.Contains(post.userId));
    }

    return Ok(posts.Select(ToDto).ToList());
  }
  [HttpDelete("{id}")]
  public async Task<ActionResult> DeletePost(int id)
  {
    await PostRepository.DeleteAsync(id);

    return NoContent();
  }

}