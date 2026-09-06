using Entities;
using RepositoryContracts;


namespace InMemoryRepositories;

public class CommentInMemoryRepository : ICommentRepository
{
    private readonly List<Comment> _comments = new();

    public Task<Comment> AddAsync(Comment comment)
    {
        comment.id = _comments.Any() ? _comments.Max(c => c.id) + 1 : 1;
        _comments.Add(comment);
        return Task.FromResult(comment);
    }

    public Task UpdateAsync(Comment comment)
    {
        Comment? existingComment = _comments.SingleOrDefault(c => c.id == comment.id);
        if (existingComment is null)
        {
            throw new InvalidOperationException($"Comment with ID '{comment.id}' not found");
        }
        
        _comments.Remove(existingComment);
        _comments.Add(comment);
        return Task.CompletedTask;
    }

    public Task DeleteAsync(int id)
    {
        Comment? commentToRemove = _comments.SingleOrDefault(c => c.id == id);
        if (commentToRemove is null)
        {
            throw new InvalidOperationException($"Comment with ID '{id}' not found");
        }
        
        _comments.Remove(commentToRemove);
        return Task.CompletedTask;
    }

    public Task<Comment> GetSingleAsync(int id)
    {
        Comment? comment = _comments.SingleOrDefault(c => c.id == id);
        if (comment is null)
        {
            throw new InvalidOperationException($"Comment with ID '{id}' not found");
        }
        
        return Task.FromResult(comment);
    }

    public IQueryable<Comment> GetMany()
    {
        return _comments.AsQueryable();
    }
}