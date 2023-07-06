using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Edesoft.ERP.Tools.Security
{
	public class MenuSideBarPersister
	{
		public Guid Id { get; set; }
		public double InternalRoleId { get; set; }
		public string Name { get; set; }
		public string Icon { get; set; }
		public string Slug { get; set; }
		public string Hash { get; set; } = null;
		public double OrdMenu { get; set; }
		public List<MenuSideBarPersister> Itens { get; set; }

	}
}
