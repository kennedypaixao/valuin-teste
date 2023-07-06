using Edesoft.ERP.Domain.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Edesoft.ERP.Domain.Interfaces.Edesoft
{
	public interface ICEPRepository : IRepositoryBase<CEP>
	{
		CEP GetByCEP(string nrCep);
	}
}
