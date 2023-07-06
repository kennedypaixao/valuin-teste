using System;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Edesoft.ERP.Shared.Roles
{
	public enum ClaimRole
	{
		CanGet = 0,
		CanPost = 1,
		CanPut = 2, 
		CanDelete = 3,
	}
	public class Roles
	{
		public Roles(double roleId, ClaimRole claim)
		{
			RoleId = roleId;
			Claim = claim;
		}
		public double RoleId { get; set; }
		public ClaimRole Claim { get; set; }

		public static Roles GetRole(double roleId, ClaimRole claim)
		{
			return new Roles(roleId, claim);
		}
	}

	public enum ModuleDefinition
	{
		Default = 0,
		Backoffice
	}

	public static class RolesDefinition
	{
		public const double All = 0;

		public const double ModuloHome = 1;
		#region Roles
		public const double RoleHomeIndex = 1.1;
		#endregion

		public const double ModuloSeguranca = 10;
		#region Roles
		public const double RoleUsuariosIndex = 10.1;
		public const double RolePerfisIndex = 10.2;
        #endregion

        public const double ModuloBackoffice = 100;
		#region Roles
		public const double RoleClientesIndex = 100.1;
		public const double RoleAtivosIndex = 100.2;
		public const double RoleSetoresIndex = 100.3;
		public const double RoleSubSetoresIndex = 100.4;
		#endregion
	}

}
