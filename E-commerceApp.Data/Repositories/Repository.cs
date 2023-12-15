using E_commerceApp.Core.Repositories;
using E_commerceApp.Data.DAL;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace E_commerceApp.Data.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly WatchesDbContext _context;

        public Repository(WatchesDbContext context)
        {
            _context = context;
        }

        public void Add(TEntity entity)
        {
            _context.Add(entity);
        }

        public TEntity Get(Func<TEntity, bool> predicate, params string[] includes)
        {
            var query = _context.Set<TEntity>().AsQueryable();
            if (includes != null && includes.Length > 0)
            {
                foreach (var item in includes)
                {
                    query = query.Include(item);
                }
            }
            return query.FirstOrDefault(predicate);
        }

        public IQueryable<TEntity> GetQueryable(Expression<Func<TEntity, bool>> predicate, params string[] includes)
        {
            var query = _context.Set<TEntity>().AsQueryable();
            if (includes != null && includes.Length > 0)
            {
                foreach (var item in includes)
                {
                    query = query.Include(item);
                }
            }
            return query.Where(predicate);
        }


        public int IsCommit()
        {
            return _context.SaveChanges();
        }

        public bool IsExist(Func<TEntity, bool> predicate, params string[] includes)
        {
            var query = _context.Set<TEntity>().AsQueryable();
            if (includes != null && includes.Length > 0)
            {
                foreach (var item in includes)
                {
                    query = query.Include(item);
                }
            }
            return query.Any(predicate);
        }

        public void Remove(TEntity entity)
        {
            _context.Remove(entity);
        }
    }
}

