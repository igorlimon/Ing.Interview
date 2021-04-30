using System.Collections.Generic;
using System.Linq;

namespace Ing.Interview.Application.Common.Interfaces
{
    /// <summary>
    ///     Wrapper interface over Entity Framework DbSet responsible to store objects of the type {T}.
    /// </summary>
    /// <typeparam name="T">The type of the object being saved in the database.</typeparam>
    public interface IStorage<T> : IQueryable<T>, IAsyncEnumerable<T>
    {
        /// <summary>
        ///     Begins tracking the given entities that they will be inserted into the database when
        ///     <see cref="IDesignStorageContext.Save()" /> is called.
        /// </summary>
        /// <param name="entities">The entities to add.</param>
        void AddRange(IEnumerable<T> entities);

        /// <summary>
        ///     Begins tracking the given entities that they will be removed from the database when
        ///     <see cref="IDesignStorageContext.Save()" /> is called.
        /// </summary>
        /// <param name="entities">The entities to remove.</param>
        void RemoveRange(IEnumerable<T> entities);

        /// <summary>
        ///     Begins tracking the given entity that they will be inserted into the database when
        ///     <see cref="IDesignStorageContext.Save()" /> is called.
        /// </summary>
        /// <param name="entity">The entity to add.</param>
        void Add(T entity);

        /// <summary>
        ///     Begins tracking the given entity that they will be updated into the database when
        ///     <see cref="IDesignStorageContext.Save()" /> is called.
        /// </summary>
        /// <param name="entity">The entity to update.</param>
        void Update(T entity);
    }
}