using Entities;
using RepositoryContracts;

namespace CLI.UI.ManageComments;

public class CreateCommentView
{
    private readonly ICommentRepository commentRepository;
    private readonly IPostRepository postRepository;
    private readonly IUserRepository userRepository;

    public CreateCommentView(
        ICommentRepository commentRepository,
        IPostRepository postRepository,
        IUserRepository userRepository)
    {
        this.commentRepository = commentRepository;
        this.postRepository = postRepository;
        this.userRepository = userRepository;
    }

    public async Task RunAsync()
    {
        Console.Clear();

        Console.WriteLine("======================");
        Console.WriteLine("     CREATE COMMENT");
        Console.WriteLine("======================");

        Console.Write("Enter comment: ");
        string? body = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(body))
        {
            Console.WriteLine("Comment cannot be empty.");
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

        Console.Write("Enter post ID: ");
        string? postInput = Console.ReadLine();

        if (!int.TryParse(postInput, out int postId))
        {
            Console.WriteLine("Invalid post ID.");
            Console.WriteLine("Press Enter to continue...");
            Console.ReadLine();
            return;
        }

        bool postExists = postRepository.GetMany()
            .Any(post => post.id == postId);

        if (!postExists)
        {
            Console.WriteLine("Post does not exist.");
            Console.WriteLine("Press Enter to continue...");
            Console.ReadLine();
            return;
        }

        Comment comment = new Comment
        {
            body = body,
            userId = userId,
            postId = postId
        };

        Comment createdComment =
            await commentRepository.AddAsync(comment);

        Console.WriteLine();
        Console.WriteLine("Comment created successfully!");
        Console.WriteLine($"Comment ID: {createdComment.id}");
        Console.WriteLine($"User ID: {createdComment.userId}");
        Console.WriteLine($"Post ID: {createdComment.postId}");

        Console.WriteLine();
        Console.WriteLine("Press Enter to continue...");
        Console.ReadLine();
    }
}