using Entities;
using RepositoryContracts;

namespace EfcRepositories;

public class CommentEfcRepository : ICommentRepository
{
    private readonly DnpDbContext context;

    public CommentEfcRepository(DnpDbContext context)
    {
        this.context = context;
    }

    public async Task<Comment> AddAsync(Comment comment)
    {
        await context.Comments.AddAsync(comment);
        await context.SaveChangesAsync();

        return comment;
    }

    public async Task UpdateAsync(Comment comment)
    {
        Comment? existingComment =
            await context.Comments.FindAsync(comment.id);

        if (existingComment is null)
        {
            throw new InvalidOperationException(
                $"Comment with ID {comment.id} not found");
        }

        existingComment.body = comment.body;
        existingComment.userId = comment.userId;
        existingComment.postId = comment.postId;

        await context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        Comment? comment = await context.Comments.FindAsync(id);

        if (comment is null)
        {
            throw new InvalidOperationException(
                $"Comment with ID {id} not found");
        }

        context.Comments.Remove(comment);
        await context.SaveChangesAsync();
    }

    public async Task<Comment> GetSingleAsync(int id)
    {
        return (await context.Comments.FindAsync(id))!;
    }

    public IQueryable<Comment> GetMany()
    {
        return context.Comments;
    }
}