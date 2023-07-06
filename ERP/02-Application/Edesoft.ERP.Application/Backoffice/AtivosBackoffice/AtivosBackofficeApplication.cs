using AutoMapper;
using Edesoft.ERP.Application.Global;
using Edesoft.ERP.Domain.DataBase;
using Edesoft.ERP.Domain.Interfaces;
using Edesoft.ERP.DTO.Backoffice.Ativos;
using Edesoft.ERP.Tools.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Edesoft.ERP.Application.Backoffice.AtivosBackoffice
{
    public class AtivosBackofficeApplication : ApplicationBase
    {
        private IMapper _mapper;
        private readonly IRepositoryBase<Ativos> _ativoRepository;
        private readonly IRepositoryBase<Setores> _setorRepository;
        private readonly IRepositoryBase<SubSetores> _subSetorRepository;
        public AtivosBackofficeApplication(IMapper mapper,
            IRepositoryBase<Ativos> ativoRepository,
            IRepositoryBase<Setores> setorRepository,
            IRepositoryBase<SubSetores> subSetorRepository)
        {
            _mapper = mapper;
            _ativoRepository = ativoRepository;

            _setorRepository = setorRepository;
            _subSetorRepository = subSetorRepository;
        }

        public List<AtivosBackofficeDto> GetAll()
        {
            var entities = _ativoRepository.Get();
            var model = _mapper.Map<List<AtivosBackofficeDto>>(entities);
            return model;
        }

        public AtivosBackofficeDto Get(Guid Id)
        {
            var entities = _ativoRepository.Get(Id);
            var model = _mapper.Map<AtivosBackofficeDto>(entities);
            return model;
        }

        public void Insert(AtivosBackofficeDto ativoBackoffice)
        {
            ativoBackoffice.ValidateModel();

            BeginTransaction();

            try
            {
                ativoBackoffice.Logotipo = "";

                var entity = _mapper.Map<Ativos>(ativoBackoffice);
                entity.Id = Guid.NewGuid();
                if (entity.SetorId == Guid.Empty)
                {
                    entity.Setores = new Setores()
                    {
                        Id = Guid.NewGuid(),
                        Nome = ativoBackoffice.NomeSetor,
                        SubSetores = new List<SubSetores>()
                        {
                            new SubSetores()
                            {
                                Id = Guid.NewGuid(),
                                Nome = ativoBackoffice.NomeSubSetor
                            }
                        }
                    };
                    entity.SetorId = entity.Setores.Id;
                    entity.SubSetorId = entity.Setores.SubSetores.FirstOrDefault().Id;
                }
                else if (entity.SubSetorId == Guid.Empty)
                {
                    SubSetores novoSubSetor = new SubSetores()
                    {
                        Id = Guid.NewGuid(),
                        Nome = ativoBackoffice.NomeSubSetor
                    };
                    entity.SubSetores = novoSubSetor;
                    entity.Setores.SubSetores.Add(novoSubSetor);
                    entity.SubSetorId = novoSubSetor.Id;
                }
                _ativoRepository.Add(entity);

                Commit();
                SaveChanges();
            }
            catch (Exception ex)
            {
                Rollback();
                throw new Exception(ex.GetFullMessage());
            }
        }

        public void Update(Guid id, AtivosBackofficeDto ativoBackoffice)
        {
            ativoBackoffice.ValidateModel();

            BeginTransaction();
            try
            {
                ativoBackoffice.Logotipo = "";

                var entity = _ativoRepository.Get(id);
                if (entity == null)
                    throw new Exception("Ativo não encontrado");

                entity = _mapper.Map(ativoBackoffice, entity);

                if (entity.SetorId == Guid.Empty)
                {
                    entity.Setores = new Setores()
                    {
                        Id = Guid.NewGuid(),
                        Nome = ativoBackoffice.NomeSetor,
                        SubSetores = new List<SubSetores>()
                        {
                            new SubSetores()
                            {
                                Id = Guid.NewGuid(),
                                Nome = ativoBackoffice.NomeSubSetor
                            }
                        }
                    };
                    entity.SetorId = entity.Setores.Id;
                    entity.SubSetorId = entity.Setores.SubSetores.FirstOrDefault().Id;
                }
                else if (entity.SubSetorId == Guid.Empty)
                {
                    SubSetores novoSubSetor = new SubSetores()
                    {
                        Id = Guid.NewGuid(),
                        Nome = ativoBackoffice.NomeSubSetor
                    };
                    entity.SubSetores = novoSubSetor;
                    entity.Setores.SubSetores.Add(novoSubSetor);
                    entity.SubSetorId = novoSubSetor.Id;
                }

                _ativoRepository.Update(entity);

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
                var entity = _ativoRepository.Get(id);
                if (entity == null)
                    throw new Exception("Ativo não encontrado");
                _ativoRepository.Delete(entity);

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
