using Edesoft.ERP.Shared.Roles;
using System;
using System.Collections.Generic;
using System.IO;

namespace Edesoft.ERP.Tools.Security
{
    public class AccountPersister
    {
        public Guid Id { get; set; }
		public Guid IdContratante { get; set; }
		public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool Active { get; set; }
		public List<MenuSideBarPersister> SideBarItens { get; set; }
        public List<Roles> Roles { get; set; } 
	}
}
