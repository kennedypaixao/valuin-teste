using Edesoft.ERP.Domain.Interfaces;
using Microsoft.Practices.ServiceLocation;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Diagnostics;

namespace Edesoft.ERP.Infra.Repositories.Global
{
    public class UnitOfWork : IUnitOfWork
    {
        private  DbContext _context { get; set; }
		private DbContextTransaction _dbContextTransaction;

        public UnitOfWork()
        {
            var contextManager = ServiceLocator.Current.GetInstance<ContextManager>();
            _context = contextManager.Context;
        }

        public void RefreshInstanceContext()
        {
            _context.Database.Connection.Close();
            ObjectContext ctx = ((IObjectContextAdapter)_context).ObjectContext;
            ctx.Refresh(RefreshMode.StoreWins,ctx.ObjectStateManager.GetObjectStateEntries(EntityState.Added
                                                           | EntityState.Modified
                                                           | EntityState.Deleted));
        }

        public void SaveChanges()
        {
            _context.Database.Log = (s) => { Debug.WriteLine(s); };
            _context.SaveChanges();
            
        }

        public void BeginTransaction()
        {
            _dbContextTransaction = _context.Database.BeginTransaction();
        }

        public int Commit()
        {
			_dbContextTransaction.Commit();
			return 0; // _context.SaveChanges();
        }

        public void Rollback()
        {
            _dbContextTransaction.Rollback();
            RefreshInstanceContext();
        }

    }
}
