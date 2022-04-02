using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Core;
using System.Linq;

namespace Alfatraining.Vertrag.Db.Repository
{
    public class EFGenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        DbContext _context;
        DbSet<TEntity> _dbSet;

        public EFGenericRepository(DbContext context)
        {
            _context = context;
            _dbSet = context.Set<TEntity>();

        }

        public IEnumerable<TEntity> Get()
        {
            return _dbSet.AsNoTracking().ToList();
        }

        public IEnumerable<TEntity> Take(int count)
        {
            return _dbSet.AsNoTracking().Take(count).ToList();
        }

        public IEnumerable<TEntity> Take(int count, Func<TEntity, bool> predicate)
        {
            return _dbSet.AsNoTracking().Where(predicate).Take(count).ToList();
        }

        public TEntity FindById(int id)
        {
            return _dbSet.Find(id);
        }

        public TEntity Create(TEntity item)
        {
            var itemNew = _dbSet.Add(item);
            _context.SaveChanges();
            return itemNew.Entity;
        }
        public TEntity Update(TEntity item, byte[] Timestamp)
        {
            if (item is IEntity)
            {
                _context.Entry(item).OriginalValues["RowVersion"] = Timestamp;
            }
                _context.Entry(item).State = EntityState.Modified;
                _context.SaveChanges();
                _context.Entry(item).State = EntityState.Detached;
                _context.SaveChanges();
                

            return item;
        }
        public void Remove(TEntity item)
        {
            _dbSet.Attach(item);
            _dbSet.Remove(item);
            _context.SaveChanges();
        }
        public TEntity Reload(int id)
        {
            var item = _dbSet.Find(id);
            if (item == null)
            {
                return null;
            }
            _context.Entry(item).State = EntityState.Detached;
            var result = _context.Entry(item).GetDatabaseValues();
            if (result == null)
            {
                return null;
            }
            else
            {
                return (TEntity)result.ToObject();
            }
        }
        public TEntity FindByIdForReload(int id)
        {
            var item = _dbSet.Find(id);
            if (item != null)
            {
                _context.Entry(item).Reload();
            }

            return item;
        }

        void IGenericRepository<TEntity>.Update(TEntity item)
        {
            throw new NotImplementedException();
        }
    }
}
