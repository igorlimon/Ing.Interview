using Ing.Interview.Domain.Common;
using Ing.Interview.Domain.Entities;

namespace Ing.Interview.Domain.Events
{
    public class TodoItemCompletedEvent : DomainEvent
    {
        public TodoItemCompletedEvent(TodoItem item)
        {
            Item = item;
        }

        public TodoItem Item { get; }
    }
}
