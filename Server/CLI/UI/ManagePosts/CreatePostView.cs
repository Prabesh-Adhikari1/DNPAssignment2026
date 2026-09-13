using Entities;
using RepositoryContracts;

namespace CLI.UI.ManagePosts;

public class CreatePostView
{
    private readonly IPostRepository postRepository;
    private readonly IUserRepository userRepository;

    public CreatePostView(
        IPostRepository postRepository,
        IUserRepository userRepository)
    {
        this.postRepository = postRepository;
        this.userRepository = userRepository;
    }
     public async Task RunAsync()
    {
        Console.Clear();

        Console.WriteLine("===============");
        Console.WriteLine("    CREATE POST");
        Console.WriteLine("===============");

        Console.Write("Enter title: ");
        string? title = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(title))
        {
            Console.WriteLine("Title cannot be empty.");
            Console.WriteLine("Press Enter to continue...");
            Console.ReadLine();
            return;
        }

        Console.Write("Enter content: ");
        string? content = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(content))
        {
            Console.WriteLine("Content cannot be empty.");
            Console.WriteLine("Press Enter to continue...");
            Console.ReadLine();
            return;
        }

        Console.Write("Enter user ID: ");
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

        Post post = new Post
        { 
            title = title,
            content = content,
            userId = userId
        };

        Post createdPost = await postRepository.AddAsync(post);

        Console.WriteLine();
        Console.WriteLine("Post created successfully!");
        Console.WriteLine($"Post ID: {createdPost.id}");
        Console.WriteLine($"Title: {createdPost.title}");
        Console.WriteLine($"User ID: {createdPost.userId}");

        Console.WriteLine();
        Console.WriteLine("Press Enter to continue...");
        Console.ReadLine();
    }

}