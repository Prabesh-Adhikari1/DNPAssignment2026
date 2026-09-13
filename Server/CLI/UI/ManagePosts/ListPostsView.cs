using RepositoryContracts;

namespace CLI.UI.ManagePosts;

public class ListPostsView
{
    private readonly IPostRepository postRepository;

    public ListPostsView(IPostRepository postRepository)
    {
        this.postRepository = postRepository;
    }

    public async Task RunAsync()
    {
        Console.Clear();

        Console.WriteLine("===============");
        Console.WriteLine("    LIST POSTS");
        Console.WriteLine("===============");

        var posts = postRepository.GetMany();

        if (!posts.Any())
        {
            Console.WriteLine("No posts found.");
        }
        else
        {
            foreach (var post in posts)
            {
                Console.WriteLine($"ID: {post.id}");
                Console.WriteLine($"Title: {post.title}");
                Console.WriteLine($"Content: {post.content}");
                Console.WriteLine($"User ID: {post.userId}");
                Console.WriteLine("-------------------");
            }
        }

        Console.WriteLine();
        Console.WriteLine("Press Enter to continue...");
        Console.ReadLine();

        await Task.CompletedTask;
    }
}