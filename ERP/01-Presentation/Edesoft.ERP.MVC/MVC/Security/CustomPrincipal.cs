using Edesoft.ERP.Shared.Roles;
using Edesoft.ERP.Tools.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace Edesoft.ERP.MVC.MVC.Security
{
	public class CustomPrincipal : IPrincipal
	{
		private AccountPersister account;

		public IIdentity Identity { get; set; }

		public CustomPrincipal(AccountPersister Account)
		{
			account = Account;
			Identity = new GenericIdentity(account.Email);
		}

		public bool IsInRole(List<Roles> roles)
		{
			return account.Roles.Any(x => roles.Any(p => p.RoleId == x.RoleId && p.Claim == x.Claim));
		}

		public bool IsInRole(string role)
		{
			throw new NotImplementedException();
		}
	}
}