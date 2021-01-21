using TodoApp.Data;

namespace TodoApp.Services.Todo
{
    public class TodoService : Todos.TodosBase 
    {
        private readonly TodoAppDbContext context;
        
        public TodoService(TodoAppDbContext context)
        {
            this.context = context;
        }
    }
}
