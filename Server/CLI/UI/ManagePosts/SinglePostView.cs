using Entities;
using RepositoryContracts;

namespace CLI.UI.ManagePosts;

public class SinglePostView
{
    private readonly IPostRepository postRepository;
    private readonly ICommentRepository commentRepository;
    private readonly IUserRepository userRepository;


    public SinglePostView(IPostRepository postRepository, ICommentRepository commentRepository, 
        IUserRepository userRepository)
    {
        this.postRepository = postRepository;
        this.commentRepository = commentRepository;
        this.userRepository = userRepository;
    }

    public async Task RunAsync()
    {
        Console.Clear();

        Console.WriteLine("===============");
        Console.WriteLine("    VIEW POST");
        Console.WriteLine("===============");

        Console.Write("Enter post ID: ");
        string? input = Console.ReadLine();

        if (!int.TryParse(input, out int postId))
        {
            Console.WriteLine("Invalid post ID.");
            Console.WriteLine("Press Enter to continue...");
            Console.ReadLine();
            return;
        }

        Post? post = await postRepository.GetSingleAsync(postId);

        if (post is null)
        {
            Console.WriteLine("Post not found.");
        }
        else
        {
            Console.WriteLine();
            Console.WriteLine($"ID: {post.id}");
            Console.WriteLine($"Title: {post.title}");
            Console.WriteLine($"Content: {post.content}");
            Console.WriteLine($"User ID: {post.userId}");
            
            Console.WriteLine();
            Console.WriteLine("-----Comments----");
            
            var comments = commentRepository.GetMany()
                .Where(c => c.postId == postId)
                .ToList();

            if (comments.Count == 0)
            {
                Console.WriteLine("No comments yet.");
            }
            else
            {
                foreach (Comment comment in comments)
                {
                    User? author = userRepository.GetMany()
                        .FirstOrDefault(u => u.id == comment.userId);

                    string name = author?.username ?? $"User {comment.userId}";
                    Console.WriteLine($"{name}: {comment.body}");
                }
            }
        }

        Console.WriteLine();
        Console.WriteLine("Press Enter to continue...");
        Console.ReadLine();
    }
}