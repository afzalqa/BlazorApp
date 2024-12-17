using BlazorApp.Models;
using Microsoft.EntityFrameworkCore;
using BlazorApp.Models;

public class AppDbContext : DbContext
{
    public DbSet<TodoItem> TodoItems { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) { }
}
