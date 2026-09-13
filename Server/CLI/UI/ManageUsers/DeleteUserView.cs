
using RepositoryContracts;

namespace CLI.UI.ManageUsers;

public class DeleteUserView
{
    private readonly IUserRepository userRepository;

    public DeleteUserView(IUserRepository userRepository)
    {
        this.userRepository = userRepository;
    }

    public async Task RunAsync()
    {
        Console.Clear();

        Console.WriteLine("================");
        Console.WriteLine("    DELETE USER");
        Console.WriteLine("================");

        Console.Write("Enter user ID: ");
        string? input = Console.ReadLine();

        if (!int.TryParse(input, out int userId))
        {
            Console.WriteLine("Invalid user ID.");
            Console.WriteLine("Press Enter to continue...");
            Console.ReadLine();
            return;
        }

        var user = userRepository.GetMany()
            .FirstOrDefault(u => u.id == userId);

        if (user == null)
        {
            Console.WriteLine("User not found.");
            Console.WriteLine("Press Enter to continue...");
            Console.ReadLine();
            return;
        }

        Console.WriteLine($"User: {user.username}");
        Console.Write("Are you sure you want to delete this user? (y/n): ");

        string? confirmation = Console.ReadLine();

        if (confirmation?.ToLower() != "y")
        {
            Console.WriteLine("Delete cancelled.");
            Console.WriteLine("Press Enter to continue...");
            Console.ReadLine();
            return;
        }

        await userRepository.DeleteAsync(userId);

        Console.WriteLine();
        Console.WriteLine("User deleted successfully!");

        Console.WriteLine();
        Console.WriteLine("Press Enter to continue...");
        Console.ReadLine();
    }
}
