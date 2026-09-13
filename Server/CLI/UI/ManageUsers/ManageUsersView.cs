
using RepositoryContracts;

namespace CLI.UI.ManageUsers;

public class ManageUsersView
{
    private readonly CreateUserView createUserView;
    private readonly ListUsersView listUsersView;
    private readonly UpdateUserView updateUserView;
    private readonly DeleteUserView deleteUserView;

    public ManageUsersView(IUserRepository userRepository)
    {
        createUserView = new CreateUserView(userRepository);
        listUsersView = new ListUsersView(userRepository);
        updateUserView = new UpdateUserView(userRepository);
        deleteUserView = new DeleteUserView(userRepository);
    }

    public async Task RunAsync()
    {
        bool running = true;

        while (running)
        {
            Console.Clear();

            Console.WriteLine("================================");
            Console.WriteLine("         MANAGE USERS");
            Console.WriteLine("================================");
            Console.WriteLine("1. Create User");
            Console.WriteLine("2. List Users");
            Console.WriteLine("3. Update User");
            Console.WriteLine("4. Delete User");
            Console.WriteLine("5. Back");
            Console.WriteLine("================================");

            Console.Write("Choose an option: ");

            string? choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    await createUserView.RunAsync();
                    break;

                case "2":
                    await listUsersView.RunAsync();
                    break;

                case "3":
                    await updateUserView.RunAsync();
                    break;

                case "4":
                    await deleteUserView.RunAsync();
                    break;

                case "5":
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

