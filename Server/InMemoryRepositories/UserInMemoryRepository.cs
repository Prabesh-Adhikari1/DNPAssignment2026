using Entities;
using RepositoryContracts;

namespace InMemoryRepositories;

public class UserInMemoryRepository: IUserRepository
{
 private readonly List<User> _users = new();

 public Task<User> AddAsync(User user)
 {
  user.id = _users.Any() ? _users.Max(u => u.id) + 1 : 1;
  _users.Add(user);
  return Task.FromResult(user);
 }

 public Task UpdateAsync(User user)
 {
  // we need to find the existing user
  User? existingUser = _users.SingleOrDefault(u => u.id == user.id);
  if (existingUser is null)
  {
   throw new InvalidOperationException($"User with Id '{user.id}' not found");
  }

  _users.Remove(existingUser);
  _users.Add(user);
  return Task.CompletedTask;
 }

 public Task DeleteAsync(int id)
 {
  User? userToRemove = _users.SingleOrDefault(u=> u.id==id);
  if (userToRemove is null)
  {
   throw new InvalidOperationException($"User with ID{id} not found");
  }

  _users.Remove(userToRemove);
  return Task.CompletedTask;

 }

 public Task<User> GetSingleAsync(int id)
 {
  User? user = _users.FirstOrDefault(u => u.id == id);
  return Task.FromResult(user);
 }

 public IQueryable<User> GetMany()
 {
  return _users.AsQueryable();
 }
}