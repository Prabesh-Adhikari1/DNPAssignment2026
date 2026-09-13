using RepositoryContracts;

namespace CLI.UI.ManageUsers;

public class ListUsersView
{
    private readonly IUserRepository userRepository;

    public ListUsersView(IUserRepository userRepository)
    {
        this.userRepository = userRepository;
    }
    public async Task RunAsync()
    {
        Console.Clear();

        Console.WriteLine("===============");
        Console.WriteLine("    LIST USERS");
        Console.WriteLine("===============");

        var users = userRepository.GetMany();

        if (!users.Any())
        {
            Console.WriteLine("No users found.");
        }
        else
        {
            foreach (var user in users)
            {
                Console.WriteLine($"ID: {user.id}");
                Console.WriteLine($"Username: {user.username}");
                Console.WriteLine("-------------------");
            }
        }

        Console.WriteLine();
        Console.WriteLine("Press Enter to continue...");
        Console.ReadLine();

        await Task.CompletedTask;
    }
}