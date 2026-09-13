
using CLI.UI.ManageComments;
using RepositoryContracts;

namespace CLI.UI.ManagePosts;

public class ManagePostsView
{
    private readonly CreatePostView createPostView;
    private readonly ListPostsView listPostsView;
    private readonly SinglePostView singlePostView;
    private readonly UpdatePostView updatePostView;
    private readonly DeletePostView deletePostView;
    private readonly CreateCommentView createCommentView;

    public ManagePostsView(
        IPostRepository postRepository,
        IUserRepository userRepository,ICommentRepository commentRepository)
    {
        createPostView = new CreatePostView(
            postRepository,
            userRepository);

        listPostsView = new ListPostsView(postRepository);

        singlePostView = new SinglePostView(postRepository,commentRepository,userRepository);

        updatePostView = new UpdatePostView(
            postRepository,
            userRepository);

        deletePostView = new DeletePostView(postRepository);

        createCommentView = new CreateCommentView(commentRepository,
            postRepository, userRepository);
    }

    public async Task RunAsync()
    {
        bool running = true;

        while (running)
        {
            Console.Clear();

            Console.WriteLine("======================");
            Console.WriteLine("     MANAGE POSTS");
            Console.WriteLine("======================");
            Console.WriteLine("1. Create post");
            Console.WriteLine("2. List posts");
            Console.WriteLine("3. View single post");
            Console.WriteLine("4. Update post");
            Console.WriteLine("5. Delete post");
            Console.WriteLine("6. Create comment");
            Console.WriteLine("7. Back");
            Console.WriteLine("======================");

            Console.Write("Choose an option: ");
            string? choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    await createPostView.RunAsync();
                    break;

                case "2":
                    await listPostsView.RunAsync();
                    break;

                case "3":
                    await singlePostView.RunAsync();
                    break;

                case "4":
                    await updatePostView.RunAsync();
                    break;

                case "5":
                    await deletePostView.RunAsync();
                    break;
                case "6":
                    await createCommentView.RunAsync();
                    break;

                case "7":
                    running = false;
                    break;

                default:
                    Console.WriteLine("Invalid option.");
                    Console.WriteLine("Press Enter to continue...");
                    Console.ReadLine();
                    break;
            }
        }
    }
}

