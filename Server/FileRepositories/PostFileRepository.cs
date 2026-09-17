using System.Text.Json;
using Entities;
using RepositoryContracts;

namespace FileRepositories;

public class PostFileRepository:IPostRepository
{
    private readonly string filepath = "posts.json";

    public PostFileRepository()
    {
        if (!File.Exists(filepath))
        {
            File.WriteAllText(filepath, "[]");
        }
    }

    public async Task<Post> AddAsync(Post post)
    {
        string postsAsJson = await File.ReadAllTextAsync(filepath);

        List<Post> posts =
            JsonSerializer.Deserialize<List<Post>>(postsAsJson)!;

        post.id = posts.Any() ? posts.Max(p => p.id) + 1 : 1;

        posts.Add(post);

        postsAsJson = JsonSerializer.Serialize(posts);

        await File.WriteAllTextAsync(filepath, postsAsJson);
        return post;
        
    }

    public async Task UpdateAsync(Post post)
    {
        string postsAsJson = await File.ReadAllTextAsync(filepath);

        List<Post> posts =
            JsonSerializer.Deserialize<List<Post>>(postsAsJson)!;

        Post? existingPost = posts.FirstOrDefault(p => p.id == post.id);

        if (existingPost == null)
        {
            throw new InvalidOperationException(
                $"Post with id {post.id} was not found.");
        }

        posts.Remove(existingPost);
        posts.Add(post);

        postsAsJson = JsonSerializer.Serialize(posts);

        await File.WriteAllTextAsync(filepath, postsAsJson);
    }

    public async  Task DeleteAsync(int id)
    {
        string postsAsJson = await File.ReadAllTextAsync(filepath);

        List<Post> posts =
            JsonSerializer.Deserialize<List<Post>>(postsAsJson)!;

        Post? existingPost = posts.FirstOrDefault(p => p.id == id);

        if (existingPost == null)
        {
            throw new InvalidOperationException(
                $"Post with id {id} was not found.");
        }

        posts.Remove(existingPost);

        postsAsJson = JsonSerializer.Serialize(posts);

        await File.WriteAllTextAsync(filepath, postsAsJson);
    }

    public async  Task<Post> GetSingleAsync(int id)
    {
        string postsAsJson = await File.ReadAllTextAsync(filepath);

        List<Post> posts =
            JsonSerializer.Deserialize<List<Post>>(postsAsJson)!;

        Post? post = posts.FirstOrDefault(p => p.id == id);

        return post!;

    }

    public IQueryable<Post> GetMany()
    {
        string postsAsJson = File.ReadAllText(filepath);

        List<Post> posts =
            JsonSerializer.Deserialize<List<Post>>(postsAsJson)!;

        return posts.AsQueryable();
    }
}