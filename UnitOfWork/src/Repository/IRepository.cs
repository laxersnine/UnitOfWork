using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Query;

namespace UnitOfWork.Repository
{
    public interface IRepository<T> : IDisposable where T : class
    {
        /// <summary>
        /// Returns IQueryable for further queries
        /// </summary>
        /// <returns>IQueryable</returns>
        IQueryable<T> AsQueryable();
        /// <summary>
        /// Try to find the object by the specific primary key
        /// </summary>
        /// <param name="id">Key</param>
        /// <returns>Null if not found</returns>
        T Find(int id);
        /// <summary>
        /// Try to find the object by the specific primary key asynchronously
        /// </summary>
        /// <param name="id">Key</param>
        /// <returns>Null if not found</returns>
        Task<T> FindAsync(int id);
        /// <summary>
        /// Add the entity to the memory set.
        /// *** Will not save to database automatically ***
        /// </summary>
        /// <param name="entity">The entity to add</param>
        /// <returns>The object with auto-generated key(if it is set in Fluent API or table schema)</returns>
        T Add(T entity);
        /// <summary>
        /// Add entities to the memory set.
        /// *** Will not save to database automatically ***
        /// </summary>
        /// <param name="entities">The entities to add</param>
        void AddRange(IEnumerable<T> entities);
        /// <summary>
        /// Mark the entity state as 'Deleted'
        /// </summary>
        /// <param name="entity">The entity</param>
        void Delete(T entity);
        /// <summary>
        /// Begin tracking the entity state. Note that if the primary key is not set, this entity will be marked as 'Added'
        /// </summary>
        /// <param name="entity">The entity</param>
        void Attach(T entity);
        IIncludableQueryable<T, object> Include(Expression<Func<T, object>> expression);
    }
}
