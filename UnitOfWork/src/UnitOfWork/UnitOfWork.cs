using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using UnitOfWork.Repository;

namespace UnitOfWork.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private DbContext _context;
        private bool _disposedValue = false;
        private readonly IDictionary<Type, object> _repositories;
        private IDbContextTransaction _transaction;

        public UnitOfWork(DbContext context)
        {
            _context = context;
            _repositories = new Dictionary<Type, object>();
        }

        public void Dispose()
        {
            Dispose(true);
        }

        public IRepository<T> Repository<T>() where T : class
        {
            var type = typeof(T);
            if (_repositories.ContainsKey(type))
            {
                return _repositories[type] as IRepository<T>;
            }

            var repo = new Repository<T>(_context);
            _repositories.Add(type, repo);

            return repo;
        }

        public int SaveChanges()
        {
            return _context.SaveChanges();
        }

        public int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            return _context.SaveChanges(acceptAllChangesOnSuccess);
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            return await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default(CancellationToken))
        {
            return await _context.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        public void BeginTransaction()
        {
            _transaction = _context.Database.BeginTransaction();
        }

        public void Commit()
        {
            _transaction.Commit();
            _transaction.Dispose();
        }

        public void Rollback()
        {
            _transaction.Rollback();
            _transaction.Dispose();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposedValue) return;
            if (disposing)
            {
                // Handle managed states (Managed objects)
                _transaction?.Dispose();
                _context.Dispose();
            }

            // Release unmanaged resources (unmanaged objects) and override finalizers below.
            // Set large fields to null.
            _transaction = null;
            _context = null;

            _disposedValue = true;
        }
    }
}
