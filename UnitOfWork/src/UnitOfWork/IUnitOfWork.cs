using System;
using System.Threading;
using System.Threading.Tasks;
using UnitOfWork.Repository;

namespace UnitOfWork.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<T> Repository<T>() where T : class;

        int SaveChanges();
        int SaveChanges(bool acceptAllChangesOnSuccess);

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken));
        Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Opens database connection and begins transaction.
        /// </summary>
        /// 
        void BeginTransaction();

        /// <summary>
        /// Commits transaction and closes database connection.
        /// </summary>
        void Commit();

        /// <summary>
        /// Rollbacks transaction and closes database connection.
        /// </summary>
        void Rollback();
    }
}
