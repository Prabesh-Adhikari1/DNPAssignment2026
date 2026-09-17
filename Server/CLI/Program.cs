using CLI.UI;
using Entities;
using FileRepositories;
using InMemoryRepositories;
using RepositoryContracts;

Console.WriteLine("Starting cli app..");
IUserRepository userRepository = new UserFileRepository();
ICommentRepository commentRepository = new CommentFileRepository();
IPostRepository postRepository = new PostFileRepository();

CLIApp cliApp= new CLIApp(userRepository, postRepository, commentRepository);
await cliApp.RunAsync();