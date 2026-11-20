using Entities;
using Microsoft.EntityFrameworkCore;

namespace EfcRepositories;

public class DnpDbContext:DbContext
{
    public DnpDbContext(DbContextOptions<DnpDbContext> options)
        : base(options)
    {
        
    }
    public DbSet<User> Users { get; set; }
    public DbSet<Post> Posts { get; set; }
    public DbSet<Comment> Comments { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        
        // Primary Keys
        modelBuilder.Entity<User>().HasKey(u => u.id);
        modelBuilder.Entity<Post>().HasKey(p => p.id);
        modelBuilder.Entity<Comment>().HasKey(c => c.id);
        
        // One User can have many Posts
        modelBuilder.Entity<Post>()
            .HasOne(p => p.user)
            .WithMany(u => u.Posts)
            .HasForeignKey(p => p.userId)
            .OnDelete(DeleteBehavior.Restrict);

        // One User can write many Comments
        modelBuilder.Entity<Comment>()
            .HasOne(c => c.User)
            .WithMany(u => u.Comments)
            .HasForeignKey(c => c.userId)
            .OnDelete(DeleteBehavior.Restrict);

        // One Post can have many Comments
        modelBuilder.Entity<Comment>()
            .HasOne(c => c.Post)
            .WithMany(p => p.Comments)
            .HasForeignKey(c => c.postId)
            .OnDelete(DeleteBehavior.Cascade);
    
    }
    
}