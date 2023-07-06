using Edesoft.ERP.Application.Global;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Edesoft.ERP.Domain.Interfaces;
using Edesoft.ERP.Domain.DataBase;
using Edesoft.ERP.Tools.Security;
using Edesoft.ERP.Domain.Interfaces.Security;
using System.Runtime.Remoting.Messaging;
using System.Web.UI.WebControls;
using Edesoft.ERP.Shared.Roles;

namespace Edesoft.ERP.Application.Security
{
	public class SecurityApplication : ApplicationBase
	{
		private ISecurityRepository _securityRepository;

		public SecurityApplication(ISecurityRepository securityRepository)
		{
			_securityRepository = securityRepository;
		}

		public AccountPersister Login(string Email, string Password)
		{
			var usuario = _securityRepository.Login(Email, Password);

			if (usuario == null)
			{
				throw new Exception("Usuário ou senha inválido!");
			}

			var persister = new AccountPersister()
			{
				Id = usuario.Id,
				IdContratante = usuario.IdContratante,
				Email = usuario.Email,
				Password = usuario.Senha,
				Name = usuario.Nome,
				Active = usuario.Ativo,
				SideBarItens = GetSideBarItens(usuario),
				Roles = GetRoles(usuario)
			};

			return persister;
		}

		private List<Roles> GetRoles(Usuarios usuario)
		{
			var model = new List<Roles>();

			foreach (var perfil in usuario.Perfis.Where(p => p.Ativo))
			{
				foreach (var role in perfil.PerfisRoles.Where(p => p.CanPost || p.CanPut || p.CanDelete || p.CanGet))
				{
					if (role.CanGet) model.Add(new Roles(role.Role.InternalRoleId, ClaimRole.CanGet));
					if (role.CanPost) model.Add(new Roles(role.Role.InternalRoleId, ClaimRole.CanPost));
					if (role.CanPut) model.Add(new Roles(role.Role.InternalRoleId, ClaimRole.CanPut));
					if (role.CanDelete) model.Add(new Roles(role.Role.InternalRoleId, ClaimRole.CanDelete));
				}
			}

			return model;
		}

		private List<MenuSideBarPersister> GetSideBarItens(Usuarios usuario)
		{
			var model = new List<MenuSideBarPersister>();

			MenuSideBarPersister addMenu(Role role)
			{
				var menuItem = model.FirstOrDefault(p=> p.Id == role.Id);

				if (menuItem == null)
				{
					menuItem = new MenuSideBarPersister
					{
						Id = role.Id,
						InternalRoleId = role.InternalRoleId,
						Name = role.Name,
						Icon = role.Icon,
						Slug = role.Slug,
						Hash = role.Hash,
						OrdMenu = role.OrdMenu,
						Itens = new List<MenuSideBarPersister>()
					};
				}

				if (role.IdParentRole != null)
				{
					var parent = addMenu(role.Role2);
					parent.Itens.Add(menuItem);
					return parent;
				}

				return menuItem;
			}


			foreach (var perfil in usuario.Perfis.Where(p=> p.Ativo))
			{
				foreach(var role in perfil.PerfisRoles.Where(p=> p.CanPost || p.CanPut || p.CanDelete || p.CanGet ))
				{
					var menu = addMenu(role.Role);
					if (!model.Any(p=> p.Id == menu.Id))
						model.Add(menu);
				}
			}

			return model.OrderBy(p=> p.OrdMenu).ToList();
		}
	}
}
