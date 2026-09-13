
using Entities;
using RepositoryContracts;

namespace CLI.UI.ManagePosts;

public class UpdatePostView
{
    private readonly IPostRepository postRepository;
    private readonly IUserRepository userRepository;

    public UpdatePostView(
        IPostRepository postRepository,
        IUserRepository userRepository)
    {
        this.postRepository = postRepository;
        this.userRepository = userRepository;
    }

    public async Task RunAsync()
    {
        Console.Clear();

        Console.WriteLine("================");
        Console.WriteLine("    UPDATE POST");
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

        Post? post = postRepository.GetMany()
            .FirstOrDefault(p => p.id == postId);

        if (post == null)
        {
            Console.WriteLine("Post not found.");
            Console.WriteLine("Press Enter to continue...");
            Console.ReadLine();
            return;
        }

        Console.Write("Enter new title: ");
        string? title = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(title))
        {
            Console.WriteLine("Title cannot be empty.");
            Console.WriteLine("Press Enter to continue...");
            Console.ReadLine();
            return;
        }

        Console.Write("Enter new content: ");
        string? content = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(content))
        {
            Console.WriteLine("Content cannot be empty.");
            Console.WriteLine("Press Enter to continue...");
            Console.ReadLine();
            return;
        }

        Console.Write("Enter new user ID: ");
        string? userInput = Console.ReadLine();

        if (!int.TryParse(userInput, out int userId))
        {
            Console.WriteLine("Invalid user ID.");
            Console.WriteLine("Press Enter to continue...");
            Console.ReadLine();
            return;
        }

        bool userExists = userRepository.GetMany()
            .Any(user => user.id == userId);

        if (!userExists)
        {
            Console.WriteLine("User does not exist.");
            Console.WriteLine("Press Enter to continue...");
            Console.ReadLine();
            return;
        }

        post.title = title;
        post.content = content;
        post.userId = userId;

        await postRepository.UpdateAsync(post);

        Console.WriteLine();
        Console.WriteLine("Post updated successfully!");

        Console.WriteLine();
        Console.WriteLine("Press Enter to continue...");
        Console.ReadLine();
    }
}

