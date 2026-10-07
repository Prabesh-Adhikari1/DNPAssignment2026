using System.Collections.Immutable;
using System.Text.Json;
using Entities;
using RepositoryContracts;

namespace FileRepositories;

public class UserFileRepository: IUserRepository
{
    private readonly string filepath = "users.json";

    public UserFileRepository()
    {
        if (!File.Exists(filepath))
        {
            File.WriteAllText(filepath,"[]");
        }
    }

    public async Task<User> AddAsync(User user)
    {
        string usersAsJson=await File.ReadAllTextAsync(filepath);
        List<User> users = JsonSerializer.Deserialize<List<User>>(usersAsJson);
        //calculating the next id
        user.id = users.Any() ? users.Max(u => u.id) + 1 : 1;
        users.Add(user);// adding the user to the users list
        // convert the list back to the json
        usersAsJson = JsonSerializer.Serialize(users);
        await File.WriteAllTextAsync(filepath, usersAsJson);
        return user;
    }

    public async Task UpdateAsync(User user)
    {
     string usersAsJson = await File.ReadAllTextAsync(filepath);
     List<User> users = JsonSerializer.Deserialize<List<User>>(usersAsJson);

     User? existingUser = users.FirstOrDefault(u => u.id == user.id);

     if (existingUser is null)
     {
         throw new InvalidOperationException($"User with Id '{user.id}' not found");
     }

     users.Remove(existingUser);
     users.Add(user);
     usersAsJson = JsonSerializer.Serialize(users);
     await File.WriteAllTextAsync(filepath, usersAsJson);
    
    }

    public async Task DeleteAsync(int id)
    {
        string usersAsJson = await File.ReadAllTextAsync(filepath);
        List<User> users = JsonSerializer.Deserialize<List<User>>(usersAsJson)!;

        User? userToRemove = users.FirstOrDefault(u => u.id == id);

        if (userToRemove is null)
        {
            throw new InvalidOperationException(
                $"User with ID '{id}' not found");
        }

        users.Remove(userToRemove);

        usersAsJson = JsonSerializer.Serialize(users);
        await File.WriteAllTextAsync(filepath, usersAsJson);
    }

    public async Task<User> GetSingleAsync(int id)
    {
        string usersAsJson = await File.ReadAllTextAsync(filepath);
        List<User> users = JsonSerializer.Deserialize<List<User>>(usersAsJson)!;

        User? user = users.FirstOrDefault(u => u.id == id);

        return user!;
    }

    public IQueryable<User> GetMany()
    {
        string usersAsJson = File.ReadAllTextAsync(filepath).Result;

        List<User> users =
            JsonSerializer.Deserialize<List<User>>(usersAsJson)!;

        return users.AsQueryable();
    }
}