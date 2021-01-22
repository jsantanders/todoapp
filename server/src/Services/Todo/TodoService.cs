using System.Threading.Tasks;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Microsoft.EntityFrameworkCore;
using TodoApp.Data;
using models = TodoApp.Data.Models;

namespace TodoApp.Services.Todo
{
    public class TodoService : Todos.TodosBase
    {
        private readonly TodoAppDbContext context;

        public TodoService(TodoAppDbContext context)
        {
            this.context = context;
        }

        public override async Task<ListReply> List(Empty request, ServerCallContext context)
        {
            var reply = new ListReply();
            var todos = await this.context.Todos.ToListAsync();
            foreach (var todo in todos)
            {
                reply.Todos.Add(Serialize(todo));
            }

            return reply;
        }

        public override async Task<Todo> Create(Todo request, ServerCallContext context)
        {
            if (request.Text is null)
            {
                throw new RpcException(new Status(StatusCode.InvalidArgument, ""), "field text cannot be empty");
            }

            var todo = new models.Todo
            {
                Text = request.Text,
                Done = request.Done
            };

            this.context.Todos.Add(todo);
            await this.context.SaveChangesAsync();

            return Serialize(todo);
        }

        public override async Task<Todo> Update(Todo request, ServerCallContext context)
        {
            var todo = await this.context.Todos.FindAsync(request.Name);
            if (todo is null)
            {
                throw new RpcException(new Status(StatusCode.NotFound, ""), $"todo with id {request.Name} does not exist");
            }
            if (request.Text is null)
            {
                throw new RpcException(new Status(StatusCode.InvalidArgument, ""), "field text cannot be empty");
            }

            todo.Text = request.Text;
            todo.Done = request.Done;

            await this.context.SaveChangesAsync();

            return Serialize(todo);
        }

        public override async Task<Empty> Delete(DeleteRequest request, ServerCallContext context)
        {
            var todo = await this.context.Todos.FindAsync(request.Name);
            if (todo is null)
            {
                throw new RpcException(new Status(StatusCode.NotFound, ""), $"todo with id {request.Name} does not exist");
            }

            this.context.Todos.Remove(todo);
            await this.context.SaveChangesAsync();

            return new Empty();
        }

        private Todo Serialize(models.Todo todo)
        {
            return new Todo
            {
                Name = todo.Id,
                Text = todo.Text,
                Done = todo.Done
            };
        }
    }
}
