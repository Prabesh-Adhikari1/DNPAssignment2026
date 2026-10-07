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
    return Created($"/Posts/{createdPost.id}", createdPost);
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
    Post post = await PostRepository.GetSingleAsync(id);

    return Ok(post);
  }
  
  [HttpGet]
  public ActionResult<IQueryable<Post>> GetMany(
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

    return Ok(posts);
  }
  [HttpDelete("{id}")]
  public async Task<ActionResult> DeletePost(int id)
  {
    await PostRepository.DeleteAsync(id);

    return NoContent();
  }

}