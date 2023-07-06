using AutoMapper;
using Edesoft.ERP.Application.Global;
using Edesoft.ERP.Domain.DataBase;
using Edesoft.ERP.Domain.Interfaces.Backoffice.Cliente;
using Edesoft.ERP.DTO.Backoffice.Cliente;
using Edesoft.ERP.Tools.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Edesoft.ERP.Application.Backoffice.Cliente
{
    public class ClienteApplication : ApplicationBase
    {
        private IMapper _mapper;
        private readonly IClienteRepository _clienteRepository;

        public ClienteApplication(IMapper mapper, IClienteRepository clienteRepository)
        {
            _mapper = mapper;
            _clienteRepository = clienteRepository;
        }

        public List<ClienteBackofficeDto> GetAll()
        {
            var entities = _clienteRepository.Get();
            var model = _mapper.Map<List<ClienteBackofficeDto>>(entities);

            return model;
        }

        public ClienteBackofficeDto Get(Guid id)
        {
            var entities = _clienteRepository.Get(id);
            var model = _mapper.Map<ClienteBackofficeDto>(entities);

            return model;
        }

        public void Insert(ClienteBackofficeDto clienteBackoffice)
        {
            clienteBackoffice.ValidateModel();

            BeginTransaction();
            try
            {
                var entity = _mapper.Map<Clientes>(clienteBackoffice);

                var check = _clienteRepository.Get(c => c.Email == clienteBackoffice.Email).FirstOrDefault();

                if (check != null)
                    throw new Exception("Cliente já cadastrado com esse Email");

                _clienteRepository.Insert(entity);

                Commit();
                SaveChanges();
            }
            catch (Exception ex)
            {
                Rollback();
                throw new Exception(ex.GetFullMessage());
            }

        }

        public void Update(Guid id, ClienteBackofficeDto clienteBackoffice)
        {
            clienteBackoffice.ValidateModel();

            BeginTransaction();
            try
            {

                var entity = _clienteRepository.Get(id);
                if (entity == null)
                    throw new Exception("Cliente não encontrado");

                entity = _mapper.Map(clienteBackoffice, entity);

                _clienteRepository.UpdateCliente(entity);

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

                var entity = _clienteRepository.Get(id);
                if (entity == null)
                    throw new Exception("Cliente não encontrado");
                _clienteRepository.DeleteCliente(entity);

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
