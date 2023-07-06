using Edesoft.ERP.Shared.Roles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Edesoft.ERP.Domain.DataBase.Seed.Custom
{
    public class EdesoftCustomRolesSeeder : EdesoftRoleSeeder
    {
        private EdesoftDataBaseContext _context;
        public EdesoftCustomRolesSeeder(EdesoftDataBaseContext context) : base(context)
        {
            _context = context;
        }

        public override void CustomRoles()
        {
            var topModuleBackoffice = SeedTopModulo(ModuleDefinition.Backoffice, "Backoffice", "fa-phone-office", 2);
            Modules[] ModulesBackoffice()
            {
                return new Modules[] { topModuleBackoffice };
            }

            var menuSeguranca = SeedRoleModulo(EdesoftCustomRolesDefinition.ModuleBackoffice, "BackOffice", true, "fa-industry-windows", ModulesBackoffice());
            #region Opções clientes
            var menuBackoffice = SeedRoleMenu(EdesoftCustomRolesDefinition.MenuCliente, menuSeguranca, "Clientes", "fa-chess-rook", true, ModulesBackoffice());
            SeedRoleProgram(EdesoftCustomRolesDefinition.RoleClienteIndex, menuSeguranca, "Clientes", "ClienteBackoffice/Index", "fa-chess", true, ModulesBackoffice());
            SeedRoleProgram(EdesoftCustomRolesDefinition.RoleAtivosIndex, menuSeguranca, "Ativos", "AtivosBackoffice/Index", "fa-chess", true, ModulesBackoffice());
            SeedRoleProgram(EdesoftCustomRolesDefinition.RoleSetoresIndex, null, "Setores", "SetoresBackoffice/Index", "fa-chess", true, ModulesBackoffice());
            SeedRoleProgram(EdesoftCustomRolesDefinition.RoleSubSetoresIndex, null, "SubSetores", "SubSetoresBackoffice/Index", "fa-chess", true, ModulesBackoffice());
            #endregion

        }
    }
}
