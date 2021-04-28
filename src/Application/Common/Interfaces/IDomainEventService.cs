using Ing.Interview.Domain.Common;
using System.Threading.Tasks;

namespace Ing.Interview.Application.Common.Interfaces
{
    public interface IDomainEventService
    {
        Task Publish(DomainEvent domainEvent);
    }
}
