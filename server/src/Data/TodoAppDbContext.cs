using Microsoft.EntityFrameworkCore;
using TodoApp.Data.Models;

namespace TodoApp.Data
{
    public class TodoAppDbContext: DbContext 
    {
        public TodoAppDbContext() { }

        public TodoAppDbContext(DbContextOptions<TodoAppDbContext> options) : base(options) { }

        public DbSet<Todo> Todos { get; set; }
    }
}
