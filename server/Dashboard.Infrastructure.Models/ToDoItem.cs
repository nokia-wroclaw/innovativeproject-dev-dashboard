using System.ComponentModel.DataAnnotations;

namespace Dashboard.Infrastructure.Models
{
    public class ToDoItem
    {
        public int Id { get; set; }
        public string Text { get; set; }
    }
}
