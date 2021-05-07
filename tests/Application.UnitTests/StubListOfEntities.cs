using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Ing.Interview.Application.Common.Interfaces;
using Moq;

namespace Ing.Interview.Application.UnitTests
{
    internal class StubListOfEntities<T> : IStorage<T>
    {
        private readonly List<T> _data;

        public StubListOfEntities(List<T> data)
        {
            _data = data;
        }

        /// <inheritdoc />
        public void Add(T entity)
        {
            _data.Add(entity);
        }

        /// <inheritdoc />
        public IEnumerator<T> GetEnumerator()
        {
            return _data.AsEnumerable().GetEnumerator();
        }

        /// <inheritdoc />
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <inheritdoc />
        public Expression Expression => _data.AsQueryable().Expression;

        /// <inheritdoc />
        public Type ElementType => _data.AsQueryable().ElementType;

        /// <inheritdoc />
        public IQueryProvider Provider => _data.AsQueryable().Provider;

        public IAsyncEnumerator<T> GetAsyncEnumerator(CancellationToken cancellationToken = new CancellationToken())
        {
            var mock = new Mock<IAsyncEnumerator<T>>();

            // ReSharper disable once GenericEnumeratorNotDisposed
            var enumerator = _data.GetEnumerator();
            mock
                .Setup(m => m.MoveNextAsync())
                .Returns(() => new ValueTask<bool>(enumerator.MoveNext()));

            mock
                .Setup(m => m.Current)
                .Returns(() => enumerator.Current);

            return mock.Object;
        }
    }
}