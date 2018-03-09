using System.Collections.Generic;
using Dashboard.Infrastructure.Models;

namespace Dashboard.Infrastructure.Data.Context
{
    public abstract class AppDbContextSeed
    {
        public static void Seed(AppDbContext ctx)
        {
            SeedItems.ForEach(x => ctx.Add(x));

            ctx.SaveChanges();
        }

        private static List<ToDoItem> SeedItems => new List<ToDoItem>()
        {
            new ToDoItem() {Id = 1, Text = "Cyka"},
            new ToDoItem() {Id = 2, Text = "Hello"},
            new ToDoItem() {Id = 3, Text = "Ta"},
        };
    }
}
