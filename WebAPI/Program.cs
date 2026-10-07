using FileRepositories;
using RepositoryContracts;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddSingleton<IUserRepository, UserFileRepository>();
builder.Services.AddSingleton<ICommentRepository, CommentFileRepository>();
builder.Services.AddSingleton<IPostRepository, PostFileRepository>();

var app = builder.Build();

app.MapControllers();

app.Run();