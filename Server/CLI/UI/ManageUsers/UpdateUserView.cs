
using Entities;
using RepositoryContracts;

namespace CLI.UI.ManageUsers;

public class UpdateUserView
{
    private readonly IUserRepository userRepository;

    public UpdateUserView(IUserRepository userRepository)
    {
        this.userRepository = userRepository;
    }

    public async Task RunAsync()
    {
        Console.Clear();

        Console.WriteLine("================");
        Console.WriteLine("    UPDATE USER");
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

        User? user = userRepository.GetMany()
            .FirstOrDefault(u => u.id == userId);

        if (user == null)
        {
            Console.WriteLine("User not found.");
            Console.WriteLine("Press Enter to continue...");
            Console.ReadLine();
            return;
        }

        Console.WriteLine($"Current username: {user.username}");

        Console.Write("Enter new username: ");
        string? username = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(username))
        {
            Console.WriteLine("Username cannot be empty.");
            Console.WriteLine("Press Enter to continue...");
            Console.ReadLine();
            return;
        }

        bool usernameExists = userRepository.GetMany()
            .Any(u => u.username == username && u.id != userId);

        if (usernameExists)
        {
            Console.WriteLine("Username already exists.");
            Console.WriteLine("Press Enter to continue...");
            Console.ReadLine();
            return;
        }

        Console.Write("Enter new password: ");
        string? password = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(password))
        {
            Console.WriteLine("Password cannot be empty.");
            Console.WriteLine("Press Enter to continue...");
            Console.ReadLine();
            return;
        }

        user.username = username;
        user.password = password;

        await userRepository.UpdateAsync(user);

        Console.WriteLine();
        Console.WriteLine("User updated successfully!");
        Console.WriteLine($"User ID: {user.id}");
        Console.WriteLine($"Username: {user.username}");

        Console.WriteLine();
        Console.WriteLine("Press Enter to continue...");
        Console.ReadLine();
    }
}

