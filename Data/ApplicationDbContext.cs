using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ToDoListMVC.Models;

namespace ToDoListMVC.Data
{
    public class ApplicationDbContext : IdentityDbContext<AppUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // DbSets
        public virtual DbSet<ToDoItem> ToDoItems { get; set; } = default!;
        public virtual DbSet<Accessory> Accessories { get; set; } = default!;
    }
}