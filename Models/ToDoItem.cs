using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ToDoListMVC.Models
{
    public class ToDoItem
    {
        public int Id { get; set; }
        [Required]
        public string? Name { get; set; }
        public string? AppUserId { get; set; }
        [Display(Name = "Date Created")]
        public DateTimeOffset DateCreated { get; set; }
        [NotMapped]
        [Display(Name = "Due Date")]
        public DateTimeOffset DueDate { get; set; }
        public bool Completed { get; set; }

        public virtual AppUser? AppUser { get; set; }

        public virtual ICollection<Accessory> Accessories { get; set; } = new HashSet<Accessory>();
    }
}
