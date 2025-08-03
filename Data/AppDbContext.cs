using Microsoft.EntityFrameworkCore;
using _net_taskApiSimple.Models;

namespace _net_taskApiSimple.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) { }

    public DbSet<TaskItem> Tasks => Set<TaskItem>();
    public DbSet<User> Users => Set<User>();

}
