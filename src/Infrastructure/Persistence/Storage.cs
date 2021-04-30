using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using Ing.Interview.Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Ing.Interview.Infrastructure.Persistence
{
    /// <inheritdoc />
    /// <remarks><see cref="Storage{T}"/> represent generic implementation of the repository class. It's using <see cref="DbSet{TEntity}"/> to handle persistence for specific entity class.</remarks>
    public class Storage<T> : IStorage<T> where T : class
    {
        /// <summary>
        /// Entity framework type to perform database operation.
        /// </summary>
        private readonly DbSet<T> _data;

        /// <summary>
        /// Initializes a new instance of the <see cref="IStorage{T}"/> interface.
        /// </summary>
        /// <param name="data">Entity framework type to perform database operation.</param>
        public Storage(DbSet<T> data)
        {
            _data = data;
        }

        /// <inheritdoc />
        public IEnumerator<T> GetEnumerator()
        {
            return _data.AsNoTracking().AsEnumerable().GetEnumerator();
        }

        /// <inheritdoc />
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <inheritdoc />
        public void AddRange(IEnumerable<T> entities)
        {
            _data.AddRange(entities);
        }

        /// <inheritdoc />
        public void RemoveRange(IEnumerable<T> entities)
        {
            _data.RemoveRange(entities);
        }

        /// <inheritdoc />
        public void Add(T entity)
        {
            _data.Add(entity);
        }

        /// <inheritdoc />
        public void Update(T entity)
        {
            _data.Update(entity);
        }

        /// <inheritdoc />
        public Expression Expression => _data.AsQueryable().Expression;

        /// <inheritdoc />
        public Type ElementType => _data.AsQueryable().ElementType;

        /// <inheritdoc />
        public IQueryProvider Provider => _data.AsQueryable().Provider;

        public IAsyncEnumerator<T> GetAsyncEnumerator(CancellationToken cancellationToken = new CancellationToken())
        {
            return _data.AsAsyncEnumerable().GetAsyncEnumerator(cancellationToken);
        }
    }
}