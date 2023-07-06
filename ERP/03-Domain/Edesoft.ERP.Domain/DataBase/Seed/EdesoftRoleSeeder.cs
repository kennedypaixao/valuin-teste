using Edesoft.ERP.Shared.Roles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Edesoft.ERP.Domain.DataBase.Seed
{
    public abstract class EdesoftRoleSeeder
    {
        private EdesoftDataBaseContext _context;

        public EdesoftRoleSeeder(EdesoftDataBaseContext context)
        {
            _context = context;
        }

        public void Execute()
        {
            //CustomRoles();

            //var topModuloDefault = SeedTopModulo(ModuleDefinition.Default, "Módulo Default", "fa-network-wired", 1);
            //Modules[] ModuleDefault()
            //{
            //    return new Modules[] { topModuloDefault };
            //}
            //Modules[] AllModules()
            //{
            //    return _context.Modules.ToArray();
            //}

            //var sideMenuHome = SeedRoleModulo(RolesDefinition.ModuloHome, "Bem Vindo", true, "fa-house", ModuleDefault());
            //SeedRoleProgram(RolesDefinition.RoleHomeIndex, sideMenuHome, "Home", "Home/Index", "fa-home", true, ModuleDefault());

            //var menuSeguranca = SeedRoleModulo(RolesDefinition.ModuloSeguranca, "Segurança", true, "fa-user-secret", AllModules());
            //#region Opções Segurança
            //SeedRoleProgram(RolesDefinition.RoleUsuariosIndex, menuSeguranca, "Usuários", "Usuarios/Index", "fa-users", true, AllModules());
            //SeedRoleProgram(RolesDefinition.RolePerfisIndex, menuSeguranca, "Perfis de Acesso", "Perfis/Index", "fa-lock-keyhole-open", true, AllModules());
            //#endregion

        }

        protected Modules SeedTopModulo(ModuleDefinition Role, string Nome, string Icon, decimal Ordem, Modules Parent = null)
        {
            string slug = Nome.GenerateSlug();
            var modulo = _context.Modules.FirstOrDefault(p => p.InternalRoleId == (double)Role);
            if (modulo == null)
            {
                modulo = new Modules();
                modulo.Id = Guid.NewGuid();
                modulo.InternalRoleId = (int)Role;
                modulo.Ativo = true;
                _context.Modules.Add(modulo);
            }
            modulo.Slug = slug;
            modulo.Nome = Nome;
            modulo.Icon = Icon;
            modulo.OrdMenu = (double)Role;
            modulo.IdParentModule = Parent?.Id;

            _context.SaveChanges();
            return modulo;
        }
        protected Role SeedRole(double RoleId, string Nome, bool Ativo, string Icon, string Hash = null)
        {
            var menu = _context.Role.FirstOrDefault(x => x.InternalRoleId == RoleId);
            if (menu == null)
            {
                menu = new Role
                {
                    Id = Guid.NewGuid(),
                    InternalRoleId = RoleId
                };
                _context.Role.Add(menu);
            }

            menu.Name = Nome;
            menu.Hash = null;
            menu.Icon = Icon;
            menu.Ativo = Ativo;
            menu.OrdMenu = RoleId;
            menu.Slug = Nome.GenerateSlug();
            _context.SaveChanges();
            return menu;
        }
        protected Role SeedRoleModulo(double RoleId, string Nome, bool Ativo, string Icon, Modules[] Modulos)
        {
            Role menu = SeedRole(RoleId, Nome, Ativo, Icon);

            foreach (var modulo in Modulos)
            {
                if (!menu.RoleModules.Any(p => p.IdModule == modulo.Id))
                {
                    menu.RoleModules.Add(new RoleModules
                    {
                        Id = Guid.NewGuid(),
                        IdRole = menu.Id,
                        IdModule = modulo.Id
                    });
                }
            }

            _context.SaveChanges();

            return menu;
        }
        protected Role SeedRoleMenu(double RoleId, Role ModuloSideBarMenu, string Nome, string Icon, bool Ativo, Modules[] Modulos)
        {
            var menu = SeedRole(RoleId, Nome, Ativo, Icon);
            menu.Hash = null;
            menu.IdParentRole = ModuloSideBarMenu.Id;

            foreach (var modulo in Modulos)
            {
                if (!menu.RoleModules.Any(p => p.Id == modulo.Id))
                {
                    menu.RoleModules.Add(new RoleModules
                    {
                        Id = Guid.NewGuid(),
                        IdRole = menu.Id,
                        IdModule = modulo.Id
                    });
                }
            }

            _context.SaveChanges();
            return menu;
        }
        protected void SeedRoleProgram(double RoleId, Role SideMenu, string Nome, string Hash, string Icon, bool Ativo, Modules[] Modulos)
        {
            var programa = SeedRole(RoleId, Nome, Ativo, Icon, Hash);
            programa.Hash = Hash;
            if (SideMenu != null)
                programa.IdParentRole = SideMenu.Id;

            foreach (var modulo in Modulos)
            {
                if (!programa.RoleModules.Any(s => s.IdModule == modulo.Id))
                {
                    programa.RoleModules.Add(new RoleModules
                    {
                        Id = Guid.NewGuid(),
                        IdRole = programa.Id,
                        IdModule = modulo.Id
                    });
                }
            }
            _context.SaveChanges();
        }

        public abstract void CustomRoles();

    }
}
