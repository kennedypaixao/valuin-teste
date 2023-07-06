using Edesoft.ERP.Domain.DataBase.Seed.Custom;
using Edesoft.ERP.Shared;
using Edesoft.ERP.Shared.Roles;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Contexts;
using System.Text;

namespace Edesoft.ERP.Domain.DataBase
{
	public class EdesoftDataBaseContext : EdesoftDataBaseEntities
	{
		public void Initialize()
		{
			//EdesoftDataDaseSeeder.RunSeeders(this);
		}

	}

	public static class EdesoftDataDaseSeeder
	{
		private static EdesoftDataBaseContext _context;
		public static void RunSeeders(EdesoftDataBaseContext context)
		{
			_context = context;

			Roles();
			CorporacaoPadrao();



			EdesoftCustomSeed.RunCustomSeeders(context);
			_context.SaveChanges();
		}
		private static void Roles()
		{

			EdesoftCustomRolesSeeder rolesSeeder = new EdesoftCustomRolesSeeder(_context);
			rolesSeeder.Execute();

		}

		private static void CorporacaoPadrao()
		{
			var idCorporacaoPadrao = Guid.Parse(EdesoftCustomSeed.IdCorporacaoPadrao);
			var idContratantePadrao = Guid.Parse(EdesoftCustomSeed.IdContratantePadrao);
			var idUsuarioPadrao = Guid.Parse(EdesoftCustomSeed.IdUsuarioPadrao);
			var idPerfilPadrao = Guid.Parse(EdesoftCustomSeed.IdPerfilPadrao);

			var corporacao = _context.Corporacao.FirstOrDefault(p => p.Id == idCorporacaoPadrao);
			if (corporacao == null)
			{
				corporacao = new Corporacao()
				{
					Id = idCorporacaoPadrao,
					Nome = EdesoftCustomSeed.CorporacaoPadrao,
					Ativo = true
				};
				_context.Corporacao.Add(corporacao);
			}

			var contratante = corporacao.Contratante.FirstOrDefault(p => p.Id == idContratantePadrao);
			if (contratante == null)
			{
				contratante = new Contratante()
				{
					Id = idContratantePadrao,
					Nome = EdesoftCustomSeed.CorporacaoPadrao,
					Ativo = true,
				};
				corporacao.Contratante.Add(contratante);
			}

			var usuario = contratante.Usuarios.FirstOrDefault(p => p.Id == idUsuarioPadrao);
			if (usuario == null)
			{
				usuario = new Usuarios()
				{
					Id = idUsuarioPadrao,
					Nome = "Admin",
					Email = $"admin@{EdesoftCustomSeed.CorporacaoPadrao.ToLower()}.com.br",
					Senha = $"admin@{EdesoftCustomSeed.CorporacaoPadrao.ToLower()}@2023".Encrypt(),
					Ativo = true
				};
				contratante.Usuarios.Add(usuario);
			}
			var moduleBackoffice = _context.Modules.FirstOrDefault(p => p.InternalRoleId == (double)ModuleDefinition.Backoffice);
			var moduleContratante = contratante.ContratanteModules.FirstOrDefault(p => p.IdModule == moduleBackoffice.Id);
			if (moduleContratante == null)
			{
				contratante.ContratanteModules.Add(new ContratanteModules()
				{
					Ativo = true,
					IdModule = moduleBackoffice.Id
				});
			}

			var perfil = contratante.Perfis.FirstOrDefault(p=> p.Id == idPerfilPadrao);
			if (perfil == null)
			{
				perfil = new Perfis
				{
					Id = idPerfilPadrao,
					Ativo = true,
					Nome = "Administrador do Sistema",
				};
				contratante.Perfis.Add(perfil);
				perfil.Usuarios.Add(usuario);
				_context.Perfis.Add(perfil);
			}

			foreach(var role in moduleBackoffice.RoleModules.Where(p=>p.Role.IdParentRole != null))
			{
				if (!perfil.PerfisRoles.Any(p=> p.IdRole == role.IdRole))
				{
					perfil.PerfisRoles.Add(new PerfisRoles
					{
						IdRole = role.IdRole,
						CanDelete= true,
						CanGet = true,
						CanPost = true,
						CanPut = true,
					});
				}
			}

		}
	}
}