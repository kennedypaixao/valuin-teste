using AutoMapper;
using Edesoft.ERP.Application.Global;
using Edesoft.ERP.Domain.DataBase;
using Edesoft.ERP.Domain.Interfaces;
using Edesoft.ERP.DTO.Backoffice.Setor;
using Edesoft.ERP.Tools.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Edesoft.ERP.Application.Backoffice.Setor
{
    public class SetoresApplication : ApplicationBase
    {
        private IMapper _mapper;
        private readonly IRepositoryBase<Setores> _setorRepository;

        public SetoresApplication(IMapper mapper, IRepositoryBase<Setores> setorRepository)
        {
            _mapper = mapper;
            _setorRepository = setorRepository;
        }

        public List<SetoresBackofficeDto> GetAll()
        {
            var entities = _setorRepository.Get();
            var model = _mapper.Map<List<SetoresBackofficeDto>>(entities);

            return model;
        }

        public SetoresBackofficeDto Get(Guid Id)
        {
            var entities = _setorRepository.Get(Id);
            var model = _mapper.Map<SetoresBackofficeDto>(entities);
            return model;
        }

        public List<SetoresBackofficeDto> GetSetor(string setorNome)
        {
            var entities = _setorRepository.Get(x => x.Nome.Contains(setorNome));
            var model = _mapper.Map<List<SetoresBackofficeDto>>(entities);
            return model;
        }

        public void Insert(SetoresBackofficeDto setorBackoffice)
        {
            setorBackoffice.ValidateModel();
            BeginTransaction();

            try
            {
                var entity = _mapper.Map<Setores>(setorBackoffice);
                entity.Id = Guid.NewGuid();
                _setorRepository.Add(entity);

                Commit();
                SaveChanges();
            }
            catch (Exception ex)
            {
                Rollback();
                throw new Exception(ex.GetFullMessage());
            }
        }

        public void Update(Guid id, SetoresBackofficeDto setorBackoffice)
        {
            setorBackoffice.ValidateModel();

            BeginTransaction();
            try
            {

                var entity = _setorRepository.Get(id);
                if (entity == null)
                    throw new Exception("Setor não encontrado");

                entity = _mapper.Map(setorBackoffice, entity);

                _setorRepository.Update(entity);

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

                var entity = _setorRepository.Get(id);
                if (entity == null)
                    throw new Exception("Setor não encontrado");
                _setorRepository.Delete(entity);

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