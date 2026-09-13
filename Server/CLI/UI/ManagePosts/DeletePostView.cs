
using RepositoryContracts;

namespace CLI.UI.ManagePosts;

public class DeletePostView
{
    private readonly IPostRepository postRepository;

    public DeletePostView(IPostRepository postRepository)
    {
        this.postRepository = postRepository;
    }

    public async Task RunAsync()
    {
        Console.Clear();

        Console.WriteLine("================");
        Console.WriteLine("    DELETE POST");
        Console.WriteLine("================");

        Console.Write("Enter post ID: ");
        string? input = Console.ReadLine();

        if (!int.TryParse(input, out int postId))
        {
            Console.WriteLine("Invalid post ID.");
            Console.WriteLine("Press Enter to continue...");
            Console.ReadLine();
            return;
        }

        var post = postRepository.GetMany()
            .FirstOrDefault(p => p.id == postId);

        if (post == null)
        {
            Console.WriteLine("Post not found.");
            Console.WriteLine("Press Enter to continue...");
            Console.ReadLine();
            return;
        }

        Console.WriteLine($"Title: {post.title}");
        Console.Write("Are you sure you want to delete this post? (y/n): ");

        string? confirmation = Console.ReadLine();

        if (confirmation?.ToLower() != "y")
        {
            Console.WriteLine("Delete cancelled.");
            Console.WriteLine("Press Enter to continue...");
            Console.ReadLine();
            return;
        }

        await postRepository.DeleteAsync(postId);

        Console.WriteLine();
        Console.WriteLine("Post deleted successfully!");

        Console.WriteLine();
        Console.WriteLine("Press Enter to continue...");
        Console.ReadLine();
    }
}
