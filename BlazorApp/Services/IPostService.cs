using ApiContracts;

namespace BlazorApp.Services;

public interface IPostService
{
    Task<PostDto> AddPostAsync(CreatePostDto request);
    Task<List<PostDto>> GetPostsAsync();      // all posts, no parameter
    Task<PostDto> GetPostAsync(int id);       // one post, by id
}