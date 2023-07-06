using Edesoft.ERP.Domain.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Edesoft.ERP.Domain.Interfaces.Backoffice.Cliente
{
    public interface IClienteRepository: IRepositoryBase<Clientes>
    {
        void Insert(Clientes Entity);
        void UpdateCliente(Clientes Entity);
        void DeleteCliente(Clientes Entity);
    }
}
