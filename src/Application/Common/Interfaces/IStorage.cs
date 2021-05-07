using System.Collections.Generic;
using System.Linq;

namespace Ing.Interview.Application.Common.Interfaces
{
    public interface IStorage<T> : IQueryable<T>, IAsyncEnumerable<T>
    {
        void Add(T entity);
    }
}