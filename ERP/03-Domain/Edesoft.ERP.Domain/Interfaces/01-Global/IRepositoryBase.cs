using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;

namespace Edesoft.ERP.Domain.Interfaces
{
    public interface IRepositoryBase<TEntity> where TEntity : class
    {
        void Add(TEntity obj);
        void Delete(TEntity obj);
        void Delete(Guid id);
        TEntity Get(Guid id);
        IQueryable<TEntity> Get();
        IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> func);
        bool Any();
        bool Any(Expression<Func<TEntity, bool>> func);
		void Update(TEntity obj);
        int Count(Expression<Func<TEntity, bool>> func);
        TEntity Clone(TEntity Origin);
    }
}
