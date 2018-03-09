using System;
using System.Collections.Generic;
using System.Text;
using Dashboard.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace Dashboard.Infrastructure.Data.Context
{
    public class AppDbContext : DbContext
    {
        public DbSet<ToDoItem> ToDoItems { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            //Fluent API
            builder.Entity<ToDoItem>().HasKey(t => t.Id);
            builder.Entity<ToDoItem>().Property(t => t.Text).IsRequired();
        }
    }
}
