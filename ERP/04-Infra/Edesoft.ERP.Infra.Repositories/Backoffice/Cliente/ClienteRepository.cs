using Edesoft.ERP.Domain.DataBase;
using Edesoft.ERP.Domain.DataBase.Seed.Custom;
using Edesoft.ERP.Domain.Interfaces.Backoffice.Cliente;
using Edesoft.ERP.Infra.Repositories.Global;
using Edesoft.ERP.Shared.Constants;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations.Model;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Dapper.SqlMapper;

namespace Edesoft.ERP.Infra.Repositories.Backoffice.Cliente
{
    public class ClienteRepository : RepositoryBase<Clientes>, IClienteRepository
    {

        private void RefreshUserData(Usuarios User)
        {
            User.Nome = User.Clientes.Nome;
            User.Email = User.Clientes.Email;
            User.Ativo = User.Clientes.Ativo;
            User.IdContratante = Guid.Parse(EdesoftCustomSeed.IdContratantePadrao);
        }
        public void Insert(Clientes Entity)
        {
            var newUser = new Usuarios
            {
                Id = Guid.NewGuid(),
                Senha = DefaultValues.Password.Encrypt(),
            };
            //newUser.Roles.Add(Context.Roles.FirstOrDefault(p => p.Name == Shared.Constants.RolesConstants.User));
            Context.Perfis.FirstOrDefault().Usuarios.Add(newUser);
            newUser.Clientes = Entity;
            Entity.Usuarios = newUser;
            RefreshUserData(newUser);
            Context.Clientes.Add(Entity);
        }

        public void UpdateCliente(Clientes Entity)
        {
            Update(Entity);
            RefreshUserData(Entity.Usuarios);
        }

        public void DeleteCliente(Clientes Entity)
        {
            Context.Usuarios.Remove(Entity.Usuarios);
            Delete(Entity);
        }
    }
}
