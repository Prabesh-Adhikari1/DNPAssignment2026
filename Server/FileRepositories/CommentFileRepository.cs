using System.Text.Json;
using Entities;
using RepositoryContracts;

namespace FileRepositories;

public class CommentFileRepository:ICommentRepository
{
      private readonly string filepath = "comments.json";

    public CommentFileRepository()
    {
        if (!File.Exists(filepath))
        {
            File.WriteAllText(filepath, "[]");
        }
    }

    public async Task<Comment> AddAsync(Comment comment)
    {
        string commentsAsJson = await File.ReadAllTextAsync(filepath);

        List<Comment> comments =
            JsonSerializer.Deserialize<List<Comment>>(commentsAsJson)!;

        // Calculating the next id
        comment.id = comments.Any()
            ? comments.Max(c => c.id) + 1
            : 1;

        // Adding the comment to the list
        comments.Add(comment);

        // Convert the list back to JSON
        commentsAsJson = JsonSerializer.Serialize(comments);

        // Save the updated list
        await File.WriteAllTextAsync(filepath, commentsAsJson);

        return comment;
    }

    public async Task UpdateAsync(Comment comment)
    {
        string commentsAsJson = await File.ReadAllTextAsync(filepath);

        List<Comment> comments =
            JsonSerializer.Deserialize<List<Comment>>(commentsAsJson)!;

        Comment? existingComment =
            comments.FirstOrDefault(c => c.id == comment.id);

        if (existingComment == null)
        {
            throw new InvalidOperationException(
                $"Comment with id {comment.id} was not found.");
        }

        comments.Remove(existingComment);
        comments.Add(comment);

        commentsAsJson = JsonSerializer.Serialize(comments);

        await File.WriteAllTextAsync(filepath, commentsAsJson);
    }

    public async Task DeleteAsync(int id)
    {
        string commentsAsJson = await File.ReadAllTextAsync(filepath);

        List<Comment> comments =
            JsonSerializer.Deserialize<List<Comment>>(commentsAsJson)!;

        Comment? existingComment =
            comments.FirstOrDefault(c => c.id == id);

        if (existingComment == null)
        {
            throw new InvalidOperationException(
                $"Comment with id {id} was not found.");
        }

        comments.Remove(existingComment);

        commentsAsJson = JsonSerializer.Serialize(comments);

        await File.WriteAllTextAsync(filepath, commentsAsJson);
    }

    public async Task<Comment> GetSingleAsync(int id)
    {
        string commentsAsJson = await File.ReadAllTextAsync(filepath);

        List<Comment> comments =
            JsonSerializer.Deserialize<List<Comment>>(commentsAsJson)!;

        Comment? comment =
            comments.FirstOrDefault(c => c.id == id);

        return comment!;
    }

    public IQueryable<Comment> GetMany()
    {
        string commentsAsJson = File.ReadAllText(filepath);

        List<Comment> comments =
            JsonSerializer.Deserialize<List<Comment>>(commentsAsJson)!;

        return comments.AsQueryable();
    }
}