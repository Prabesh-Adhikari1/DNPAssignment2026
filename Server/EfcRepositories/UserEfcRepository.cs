using Entities;
using RepositoryContracts;
using Microsoft.EntityFrameworkCore;

namespace EfcRepositories;

public class UserEfcRepository : IUserRepository
{
    private readonly DnpDbContext context;

    public UserEfcRepository(DnpDbContext context)
    {
        this.context = context;
    }

    public async Task<User> AddAsync(User user)
    {
        await context.Users.AddAsync(user);
        await context.SaveChangesAsync();

        return user;
    }

    public async Task UpdateAsync(User user)
    {
        User? existingUser = await context.Users.FindAsync(user.id);

        if (existingUser is null)
        {
            throw new InvalidOperationException(
                $"User with ID {user.id} not found");
        }

        existingUser.username = user.username;
        existingUser.password = user.password;

        await context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        User? user = await context.Users.FindAsync(id);

        if (user is null)
        {
            throw new InvalidOperationException(
                $"User with ID {id} not found");
        }

        context.Users.Remove(user);
        await context.SaveChangesAsync();
    }

    public async Task<User> GetSingleAsync(int id)
    {
        return (await context.Users.FindAsync(id))!;
    }

    public IQueryable<User> GetMany()
    {
        return context.Users;
    }
}