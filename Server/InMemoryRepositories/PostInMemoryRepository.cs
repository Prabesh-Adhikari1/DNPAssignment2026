using Entities;
using RepositoryContracts;

namespace InMemoryRepositories;

public class PostInMemoryRepository: IPostRepository
{
    private readonly List<Post> posts = new();

    public Task<Post> AddAsync(Post post)
    {
        post.id = posts.Any() ? posts.Max(p => p.id) + 1 : 1;
        posts.Add(post);
        return Task.FromResult(post);
    }

    public Task UpdateAsync(Post post)
    {
        Post? existingPost = posts.FirstOrDefault(p => p.id == post.id);
        if (existingPost is null)
        {
            throw new InvalidOperationException(
                $"User with Id{post.id}not found");
        }

        posts.Remove(post);
        posts.Add(post);
        return Task.CompletedTask;
    }

    public Task DeleteAsync(int id)
    {
        Post? post = posts.SingleOrDefault(p => p.id == id);
        if (post is null)
        {
            throw new InvalidOperationException($"Post with post id {id} not found");
        }

        posts.Remove(post);
        return Task.CompletedTask;
    }

    public Task<Post> GetSingleAsync(int id)
    {
        Post? post = posts.SingleOrDefault(p => p.id == id);
        return Task.FromResult(post);
    }

    public IQueryable<Post> GetMany()
    {
        return posts.AsQueryable();
    }
}