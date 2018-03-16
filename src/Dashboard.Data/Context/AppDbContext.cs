using Dashboard.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Dashboard.Data.Context
{
    public class AppDbContext : DbContext
    {
        public DbSet<ToDoItem> ToDoItems { get; set; }
        public DbSet<Pipeline> Pipelines { get; set; }
        public DbSet<Project> Projects { get; set; }

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

            builder.Entity<Project>(model =>
            {
                model.HasKey(p => p.Id);
                model.Property(p => p.ApiAuthenticationToken).IsRequired();
                model.Property(p => p.DataProviderName).IsRequired();
                model.Property(p => p.ApiHostUrl).IsRequired();
                model.HasMany(p => p.Pipelines);
            });

        }
    }
}
