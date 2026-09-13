using Entities;
using RepositoryContracts;

namespace CLI.UI.ManageUsers;

public class CreateUserView
{
    private readonly IUserRepository userRepository;

    public CreateUserView(IUserRepository userRepository)
    {
       this.userRepository = userRepository; 
    }

    public async Task RunAsync()
    {
        Console.Clear();
        Console.WriteLine("===============");
        Console.WriteLine("     CREATE USER    ");
        Console.WriteLine("===============");
        
        Console.WriteLine("Enter username");
        string? username = Console.ReadLine();
        Console.WriteLine("Enter password");
        string? password = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(username))
        {
            Console.WriteLine("Username cannot be empty.");
            Console.WriteLine("Press Enter to continue...");
            Console.ReadLine();
            return;
        }

        if (string.IsNullOrWhiteSpace(password))
        {
            Console.WriteLine("Password cannot be empty.");
            Console.WriteLine("Press Enter to continue...");
            Console.ReadLine();
            return;
        }

        bool usernameexists = userRepository.GetMany()
            .Any(user => user.username == username);
        if (usernameexists)
        {
            Console.WriteLine("Username already exists.");
            Console.WriteLine("Press Enter to continue...");
            Console.ReadLine();
            return;
        }

        User user = new User
        {
            username = username,
            password = password
        };

        User createdUser = await userRepository.AddAsync(user);
        Console.WriteLine();
        Console.WriteLine("User created successfully!");
        Console.WriteLine($"User ID: {createdUser.id}");
        Console.WriteLine($"Username: {createdUser.username}");

        Console.WriteLine();
        Console.WriteLine("Press Enter to continue...");
        Console.ReadLine();

    }
}
