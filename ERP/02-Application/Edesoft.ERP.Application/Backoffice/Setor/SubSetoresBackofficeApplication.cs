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
    public class SubSetoresApplication : ApplicationBase
    {
        private IMapper _mapper;
        private readonly IRepositoryBase<SubSetores> _subSetorRepository;
        private readonly IRepositoryBase<Setores> _setorRepository;

        public SubSetoresApplication(
            IMapper mapper,
            IRepositoryBase<SubSetores> subSetorRepository,
            IRepositoryBase<Setores> setorRepository)
        {
            _mapper = mapper;
            _subSetorRepository = subSetorRepository;
            _setorRepository = setorRepository;
        }

        public List<SubSetoresBackofficeDto> GetAll()
        {
            var entities = _subSetorRepository.Get();
            var model = _mapper.Map<List<SubSetoresBackofficeDto>>(entities);
            return model;
        }

        public SubSetoresBackofficeDto Get(Guid Id)
        {
            var entities = _subSetorRepository.Get(Id);
            var model = _mapper.Map<SubSetoresBackofficeDto>(entities);
            return model;
        }

        public SubSetoresBackofficeDto GetBySetor(Guid Id)
        {
            var entities = _subSetorRepository.Get(sub => sub.SetorId == Id);
            var model = _mapper.Map<SubSetoresBackofficeDto>(entities);
            return model;
        }

        public List<SubSetoresBackofficeDto> GetSubSetorBySetorIdNome(Guid? setorId, string subSetorNome)
        {
            if (setorId == null)
                return new List<SubSetoresBackofficeDto>();
            var entities = GetAll().Where(x => x.SetorId == setorId && x.Nome.Contains(subSetorNome));
            var model = _mapper.Map<List<SubSetoresBackofficeDto>>(entities);
            return model;
        }

        public dynamic Insert(SubSetoresBackofficeDto subSetorBackoffice)
        {
            subSetorBackoffice.ValidateModel();

            BeginTransaction();
            try
            {
                var entitieSetor = _setorRepository.Get(subSetorBackoffice.SetorId);

                if (entitieSetor == null)
                    throw new Exception("Setor não encontrado");

                var entity = _mapper.Map<SubSetores>(subSetorBackoffice);
                entity.Id = Guid.NewGuid();
                entity.Setores = entitieSetor;
                _subSetorRepository.Add(entity);

                Commit();
                SaveChanges();

                return entity.Id;
            }
            catch (Exception ex)
            {
                Rollback();
                throw new Exception(ex.GetFullMessage());
            }
        }

        public void Update(Guid id, SubSetoresBackofficeDto subSetorBackoffice)
        {
            subSetorBackoffice.ValidateModel();

            BeginTransaction();
            try
            {

                var entity = _subSetorRepository.Get(id);
                if (entity == null)
                    throw new Exception("SubSetor não encontrado");

                var entitieSetor = _setorRepository.Get(subSetorBackoffice.SetorId);
                if (entitieSetor == null)
                    throw new Exception("Setor não encontrado");

                entity = _mapper.Map(subSetorBackoffice, entity);

                _subSetorRepository.Update(entity);

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

                var entity = _subSetorRepository.Get(id);
                if (entity == null)
                    throw new Exception("SubSetor não encontrado");

                _subSetorRepository.Delete(entity);

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