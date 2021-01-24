using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using TodoApp.Data.Models;

namespace TodoApp.Data
{
    public class SeedContext : TodoAppDbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer(new ConfigurationBuilder().AddJsonFile("appsettings.Development.json").Build().GetConnectionString("database"));
        }
    }

    public class Seed
    {
        public static async Task Fill()
        {
            using var ctx = new SeedContext();
            ctx.Database.EnsureCreated();

            var todos = new List<Todo>()
            {
                new Todo
                {
                    Id = 1,
                    Text = "Buy some eggs",
                    Done = false
                },
                new Todo
                {
                    Id = 1,
                    Text = "Buy milk",
                    Done = false
                },
                new Todo
                {
                    Id = 1,
                    Text = "Buy butter",
                    Done = true
                }
            };

            ctx.Todos.AddRange(todos);
            await ctx.SaveChangesAsync();
        }
    }
}
