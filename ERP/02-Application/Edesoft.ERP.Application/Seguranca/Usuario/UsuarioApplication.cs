using AutoMapper;
using Edesoft.ERP.Application.Global;
using Edesoft.ERP.Domain.DataBase;
using Edesoft.ERP.Domain.Interfaces;
using Edesoft.ERP.DTO.Backoffice.Usuario;
using Edesoft.ERP.Tools.Extensions;
using Edesoft.ERP.Tools.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Edesoft.ERP.Application.Seguranca.Usuario
{
	public class UsuarioApplication : ApplicationBase
	{
		private IMapper _mapper;
		private readonly IRepositoryBase<Usuarios> _usuarioRepository;

		public UsuarioApplication(IMapper mapper,
								  IRepositoryBase<Usuarios> usuarioRepository)
		{
			_mapper = mapper;
			_usuarioRepository = usuarioRepository;
		}

		public List<UsuarioBackofficeDto> GetAll()
		{
			var entities = _usuarioRepository.Get();
			var model = _mapper.Map<List<UsuarioBackofficeDto>>(entities);

			return model;
		}

		public UsuarioBackofficeDto Get(Guid Id)
		{
			var entities = _usuarioRepository.Get(Id);
			var model = _mapper.Map<UsuarioBackofficeDto>(entities);

			return model;
		}

		public void Insert(UsuarioBackofficeDto usuarioBackoffice)
		{
			usuarioBackoffice.ValidateModel();

			BeginTransaction();
			try
			{
				var entity = _mapper.Map<Usuarios>(usuarioBackoffice);
				entity.Senha = entity.Senha.Encrypt();
				entity.IdContratante = SessionPersister.User.IdContratante;
				_usuarioRepository.Add(entity);

				Commit();
				SaveChanges();
			}
			catch (Exception ex) 
			{
				Rollback();
				throw new Exception(ex.GetFullMessage());
			}

		}

		public void Update(Guid id, UsuarioBackofficeDto usuarioBackoffice)
		{
			usuarioBackoffice.ValidateModel();

			BeginTransaction();
			try
			{

				var entity = _usuarioRepository.Get(id);
				if (entity == null)
					throw new Exception("Usuário não encontrado");

				var lastPassword = entity.Senha;
				entity = _mapper.Map(usuarioBackoffice, entity);

				if (entity.Senha != lastPassword) entity.Senha = entity.Senha.Encrypt();

				_usuarioRepository.Update(entity);

				Commit();
				SaveChanges();
			}
			catch (Exception ex)
			{
				Rollback();
				throw new Exception(ex.GetFullMessage());
			}

		}

		public void Delete(Guid id)
		{

			BeginTransaction();
			try
			{

				var entity = _usuarioRepository.Get(id);
				if (entity == null)
					throw new Exception("Usuário não encontrado");
				_usuarioRepository.Delete(entity);

				Commit();
				SaveChanges();
			}
			catch (Exception ex)
			{
				Rollback();
				throw new Exception(ex.GetFullMessage());
			}

		}
	}
}
