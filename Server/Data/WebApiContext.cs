using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using TodoApi.Server.Models;

namespace TodoApi.Server.Data
{
    public class ApplicationDbContext : IdentityDbContext<WebApiUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<TodoItem> TodoItems { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<TodoItem>()
            .HasOne(t => t.WebApiUser)
            .WithMany(u => u.TodoItems)
            .HasForeignKey(t => t.WebApiUserId)
            .OnDelete(DeleteBehavior.Cascade);
        }

    }
}