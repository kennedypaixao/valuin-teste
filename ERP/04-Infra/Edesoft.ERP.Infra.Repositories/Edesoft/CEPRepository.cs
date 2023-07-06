using Edesoft.ERP.Domain.DataBase;
using Edesoft.ERP.Domain.Interfaces.Edesoft;
using Edesoft.ERP.Infra.Repositories.Global;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Edesoft.ERP.Infra.Repositories.Edesoft
{
	public class CEPRepository : RepositoryBase<CEP>, ICEPRepository
	{
		public CEP GetByCEP(string nrCep)
		{
			return Context.CEP.FirstOrDefault(p => p.NrCep == nrCep);
		}
	}
}
