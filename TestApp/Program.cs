using Entities;
using InMemoryRepositories;

var userrepository = new UserInMemoryRepository();

var user1= new User
{
    username="Prabesh",
    password="Prabesh"
};
var user2 = new User
{
    username = "bob",
    password= "5678"
};

await userrepository.AddAsync(user1);
await userrepository.AddAsync(user2);

var users=userrepository.GetMany();
foreach (var user in users)
{
    Console.WriteLine($"{user.id}: {user.username}");
}

