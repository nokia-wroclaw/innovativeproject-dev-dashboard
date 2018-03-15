using Dashboard.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Dashboard.Data.Context
{
    public class AppDbContext : DbContext
    {
        public DbSet<ToDoItem> ToDoItems { get; set; }
        public DbSet<Pipeline> Pipelines { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            //Fluent API
            builder.Entity<ToDoItem>().HasKey(t => t.Id);
            builder.Entity<ToDoItem>().Property(t => t.Text).IsRequired();

            builder.Entity<Pipeline>().HasKey(p => p.Id);
        }
    }
}
