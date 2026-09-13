using CLI.UI;
using Entities;
using InMemoryRepositories;
using RepositoryContracts;

Console.WriteLine("Starting cli app..");
IUserRepository userRepository= new UserInMemoryRepository();
ICommentRepository commentRepository= new CommentInMemoryRepository();
IPostRepository postRepository= new PostInMemoryRepository();

// fake users //

await userRepository.AddAsync(new User
{
    username = "Prabesh",
    password = "sonim"
});

await userRepository.AddAsync(new User
{
    username = "sonim",
    password = "prabesh"
});


// fake posts//
await postRepository.AddAsync(new Post
{
  title="21st September coming",
  content = "applying, hope it works this time",
  userId = 1
});

await postRepository.AddAsync(new Post
{
    title = "Talk with Carsten",
    content = "hope he helps ",
    userId = 2
});


await commentRepository.AddAsync(new Comment
{
    userId = 1,
    postId = 1,
    body = "Don't worry"
});

await commentRepository.AddAsync(new Comment
{
    userId = 2,
    postId = 2,
    body = "Ofc, he helps"
});

CLIApp cliApp= new CLIApp(userRepository, postRepository, commentRepository);
await cliApp.RunAsync();