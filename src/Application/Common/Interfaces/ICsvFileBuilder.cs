using Ing.Interview.Application.TodoLists.Queries.ExportTodos;
using System.Collections.Generic;

namespace Ing.Interview.Application.Common.Interfaces
{
    public interface ICsvFileBuilder
    {
        byte[] BuildTodoItemsFile(IEnumerable<TodoItemRecord> records);
    }
}
