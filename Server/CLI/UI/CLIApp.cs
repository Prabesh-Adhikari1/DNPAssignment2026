using CLI.UI.ManagePosts;
using CLI.UI.ManageUsers;
using RepositoryContracts;

namespace CLI.UI;

public class CLIApp
{
    private readonly IPostRepository postRepository;
    private readonly IUserRepository userRepository;
    private readonly ICommentRepository commentRepository;

    public CLIApp(IUserRepository userRepository,
        IPostRepository postRepository,
        ICommentRepository commentRepository)
    {
        this.userRepository = userRepository;
        this.postRepository = postRepository;
        this.commentRepository = commentRepository;
    }

    public async Task RunAsync()
    {
        bool running = true;

        while (running)
        {
            Console.Clear();
            Console.WriteLine("======================");
            Console.WriteLine("Welcome to Forum App");
            Console.WriteLine("======================");
            Console.WriteLine("1.Manage users");
            Console.WriteLine("2.Manage posts");
            Console.WriteLine("3.Exit the program");
            Console.WriteLine("====================");
            Console.WriteLine("Choose an option");

            String? choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    ManageUsersView manageUsersView = new ManageUsersView(userRepository);
                    await manageUsersView.RunAsync();
                    break;
                case "2":
                    ManagePostsView managePostsView = new ManagePostsView(postRepository,
                        userRepository,
                        commentRepository);
                    await managePostsView.RunAsync();
                    break;
                case "3":
                    running = false;
                    Console.WriteLine("Exiting the applications...");
                    break;
                default:
                    Console.WriteLine("Invalid choice");
                    Console.WriteLine("Press any key to continue..");
                    Console.ReadLine();
                    break;
            }
        }
    }
}