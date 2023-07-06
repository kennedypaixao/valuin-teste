using System.Collections.Generic;
using System.Data;
using System.Data.Entity;

namespace Edesoft.ERP.Domain.Interfaces
{ 
    public interface IUnitOfWork
    {
        void SaveChanges();
        void BeginTransaction();
        int Commit();
        void Rollback();
        void RefreshInstanceContext();
    }
}
