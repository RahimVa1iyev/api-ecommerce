using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace E_commerceApp.Core.Repositories
{
    public interface IRepository<TEntity>
    {
        void Add(TEntity entity);

        void Remove(TEntity entity);

        IQueryable<TEntity> GetQueryable(Expression<Func<TEntity, bool>> predicate, params string[] includes);

        TEntity Get(Func<TEntity,bool> predicate ,params string[] includes);

        bool IsExist(Func<TEntity, bool> predicate,params string[] includes);

        int IsCommit();
    }
}
