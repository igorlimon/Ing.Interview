using Ing.Interview.Domain.Common;
using Ing.Interview.Domain.Entities;

namespace Ing.Interview.Domain.Events
{
    public class TodoItemCreatedEvent : DomainEvent
    {
        public TodoItemCreatedEvent(TodoItem item)
        {
            Item = item;
        }

        public TodoItem Item { get; }
    }
}
