using Entities;
using RepositoryContracts;

namespace EfcRepositories;

public class PostEfcRepository : IPostRepository
{
    private readonly DnpDbContext context;

    public PostEfcRepository(DnpDbContext context)
    {
        this.context = context;
    }

    public async Task<Post> AddAsync(Post post)
    {
        await context.Posts.AddAsync(post);
        await context.SaveChangesAsync();

        return post;
    }

    public async Task UpdateAsync(Post post)
    {
        Post? existingPost = await context.Posts.FindAsync(post.id);

        if (existingPost is null)
        {
            throw new InvalidOperationException(
                $"Post with ID {post.id} not found");
        }

        existingPost.title = post.title;
        existingPost.content = post.content;
        existingPost.userId = post.userId;

        await context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        Post? post = await context.Posts.FindAsync(id);

        if (post is null)
        {
            throw new InvalidOperationException(
                $"Post with ID {id} not found");
        }

        context.Posts.Remove(post);
        await context.SaveChangesAsync();
    }

    public async Task<Post> GetSingleAsync(int id)
    {
        return (await context.Posts.FindAsync(id))!;
    }

    public IQueryable<Post> GetMany()
    {
        return context.Posts;
    }
}