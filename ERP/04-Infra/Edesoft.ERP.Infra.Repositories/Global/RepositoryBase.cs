using Edesoft.ERP.Domain.DataBase;
using Edesoft.ERP.Domain.DataBase.Seed;
using Edesoft.ERP.Domain.Interfaces;
using Microsoft.Practices.ServiceLocation;
using System;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Dynamic;
using System.Linq.Expressions;

namespace Edesoft.ERP.Infra.Repositories.Global
{
	public class RepositoryBase<TEntity> : IRepositoryBase<TEntity> where TEntity : class, new()
	{
		protected EdesoftDataBaseContext Context { get { return (EdesoftDataBaseContext)ServiceLocator.Current.GetInstance<ContextManager>().Context; } }
		protected SqlConnection SqlServerContext { get { return ServiceLocator.Current.GetInstance<ContextManager>().ContextSQLServer; } }

		public RepositoryBase()
		{
		}
		private void GenerateId(TEntity obj)
		{
			var property = obj.GetType().GetProperty("Id");
			if (property != null && (property.GetValue(obj) == null || (Guid)property.GetValue(obj) == Guid.Empty))
			{
				property.SetValue(obj, Guid.NewGuid());
			}
		}
		public TEntity Get(Guid id)
		{
			var retorno = Context.Set<TEntity>().Find(id);
			return retorno;
		}
		public IQueryable<TEntity> Get()
		{
			return Context.Set<TEntity>();
		}
		public IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> func)
		{
			return Context.Set<TEntity>().Where(func);
		}
		public bool Any()
		{
			return Context.Set<TEntity>().Any();
		}
		public bool Any(Expression<Func<TEntity, bool>> func)
		{
			var retorno = Context.Set<TEntity>().Where(func).Any();
			return retorno;
		}
		public void Add(TEntity obj)
		{
			GenerateId(obj);
			Context.Set<TEntity>().Add(obj);
		}

		public TEntity Clone(TEntity Origin)
		{
			var values = Context.Entry(Origin).CurrentValues.Clone();
			var clone = new TEntity();
			Add(clone);

			Context.Entry(clone).CurrentValues.SetValues(values);

			var property = clone.GetType().GetProperty("Id");
			if (property != null)
			{
				property.SetValue(clone, Guid.NewGuid());
			}

			return clone;
		}

		public void Delete(Guid id)
		{
			var obj = Get(id);
			Delete(obj);
		}
		public void Delete(TEntity obj)
		{
			Context.Entry(obj).State = System.Data.Entity.EntityState.Deleted;
		}
		public void Update(TEntity obj)
		{
			Context.Entry(obj).State = System.Data.Entity.EntityState.Modified;
		}

		public int Count(Expression<Func<TEntity, bool>> func)
		{
			var retorno = Context.Set<TEntity>().Where(func).Count();
			return retorno;
		}
	}
}
