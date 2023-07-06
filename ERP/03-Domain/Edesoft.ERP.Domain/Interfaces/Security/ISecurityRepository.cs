using Edesoft.ERP.Domain.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Edesoft.ERP.Domain.Interfaces.Security
{
    public interface ISecurityRepository: IRepositoryBase<Usuarios>
    {
		Usuarios Login(string Email, string Password);
    }
}
