using AutoMapper;
using Edesoft.ERP.DTO.Backoffice.Ativos;
using Edesoft.ERP.DTO.Backoffice.Cliente;
using Edesoft.ERP.DTO.Backoffice.Setor;
using Edesoft.ERP.DTO.Backoffice.Usuario;
using Edesoft.ERP.DTO.Edesoft.CEP;

namespace Edesoft.ERP.DTO.Mapper
{
    public static class AutoMapperConfig
    {
        public static MapperConfiguration Configure()
        {
            MapperConfiguration mapperConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Domain.DataBase.Usuarios, UsuarioBackofficeDto>().ReverseMap();

				cfg.CreateMap<Domain.DataBase.Pais, PaisDto>().ReverseMap();
                cfg.CreateMap<Domain.DataBase.UF, UFDto>().ReverseMap();
				cfg.CreateMap<Domain.DataBase.Cidade, CidadeDto>().ReverseMap();
				cfg.CreateMap<Domain.DataBase.CEP, CEPDto>().ReverseMap();

                cfg.CreateMap<Domain.DataBase.Clientes, ClienteBackofficeDto>();
                cfg.CreateMap<ClienteBackofficeDto, Domain.DataBase.Clientes>();

                cfg.CreateMap<AtivosBackofficeDto, Domain.DataBase.Ativos>().ReverseMap();
                cfg.CreateMap<SetoresBackofficeDto, Domain.DataBase.Setores>().ReverseMap();
                cfg.CreateMap<SubSetoresBackofficeDto, Domain.DataBase.SubSetores>().ReverseMap();
            });

            return mapperConfiguration;
        }
    }
}
