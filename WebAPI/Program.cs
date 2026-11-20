using EfcRepositories;
using FileRepositories;
using Microsoft.EntityFrameworkCore;
using RepositoryContracts;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

var databasepath = Path.Combine(builder.Environment.ContentRootPath,
    "app.db");

builder.Services.AddDbContext<DnpDbContext>(options=> 
    options.UseSqlite($"Data Source={databasepath}"));

builder.Services.AddScoped<IUserRepository, UserEfcRepository>();
builder.Services.AddScoped<IPostRepository, PostEfcRepository>();
builder.Services.AddScoped<ICommentRepository, CommentEfcRepository>();


//builder.Services.AddSingleton<IUserRepository, UserFileRepository>();
//builder.Services.AddSingleton<ICommentRepository, CommentFileRepository>();
//builder.Services.AddSingleton<IPostRepository, PostFileRepository>();
//builder.Services.AddSingleton<IUserRepository, UserFileRepository>();

var app = builder.Build();

app.MapControllers();

app.Run();