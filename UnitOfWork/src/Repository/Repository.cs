using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace UnitOfWork.Repository
{
    public class Repository<T> : IRepository<T>
        where T : class
    {
        private DbContext _context;
        private bool _disposedValue = false;

        public Repository(DbContext context)
        {
            _context = context;
        }

        public T Add(T entity)
        {
            _context.Set<T>().Add(entity);
            return entity;
        }

        public void AddRange(IEnumerable<T> entities)
        {
            _context.Set<T>().AddRange(entities);
        }

        public IQueryable<T> AsQueryable()
        {
            return _context.Set<T>().AsQueryable();
        }

        public void Attach(T entity)
        {
            _context.Set<T>().Attach(entity);
        }

        public void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

        public void Dispose()
        {
            Dispose(true);
            //GC.SuppressFinalize(this);
        }

        public T Find(int id)
        {
            return _context.Set<T>().Find(id);
        }

        public async Task<T> FindAsync(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public IIncludableQueryable<T, object> Include(Expression<Func<T, object>> expression)
        {
            return _context.Set<T>().Include(expression);
        }


        protected virtual void Dispose(bool disposing)
        {
            if (_disposedValue) return;
            if (!disposing) return;
            _context.Dispose();
            _context = null;
            _disposedValue = true;
        }
    }
}
