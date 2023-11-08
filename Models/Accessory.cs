using System.ComponentModel.DataAnnotations;

namespace ToDoListMVC.Models
{
    public class Accessory
    {
        public int Id { get; set; }
        [Required]
        public string? Name { get; set; }
        public string? AppUserId { get; set; }

        public virtual AppUser? AppUser { get; set; }

        public virtual ICollection<ToDoItem> ToDoItems { get; set; } = new HashSet<ToDoItem>();
    }
}
