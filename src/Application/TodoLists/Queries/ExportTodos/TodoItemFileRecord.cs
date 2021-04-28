using Ing.Interview.Application.Common.Mappings;
using Ing.Interview.Domain.Entities;

namespace Ing.Interview.Application.TodoLists.Queries.ExportTodos
{
    public class TodoItemRecord : IMapFrom<TodoItem>
    {
        public string Title { get; set; }

        public bool Done { get; set; }
    }
}
