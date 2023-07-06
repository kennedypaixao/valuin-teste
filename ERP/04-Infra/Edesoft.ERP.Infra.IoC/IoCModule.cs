using AutoMapper;
using Edesoft.ERP.Domain.Interfaces;
using Edesoft.ERP.Domain.Interfaces.Backoffice.Cliente;
using Edesoft.ERP.Domain.Interfaces.Edesoft;
using Edesoft.ERP.Domain.Interfaces.Security;
using Edesoft.ERP.DTO.Mapper;
using Edesoft.ERP.Infra.Repositories.Backoffice.Cliente;
using Edesoft.ERP.Infra.Repositories.Edesoft;
using Edesoft.ERP.Infra.Repositories.Global;
using Edesoft.ERP.Infra.Repositories.Security;
using Ninject.Modules;

namespace Edesoft.ERP.Infra.IoC
{
	public class IoCModule : NinjectModule
	{
		public override void Load()
		{

			Bind<ContextManager>().ToSelf();
			Bind(typeof(IUnitOfWork)).To(typeof(UnitOfWork));
			Bind(typeof(IRepositoryBase<>)).To(typeof(RepositoryBase<>));
			Bind<IMapper>().ToMethod(AutoMapper).InSingletonScope();

			Bind<ISecurityRepository>().To<SecurityRepository>();

			Bind<IClienteRepository>().To<ClienteRepository>();

			Bind<ICEPRepository>().To<CEPRepository>();
		}

		private IMapper AutoMapper(Ninject.Activation.IContext context)
		{
			MapperConfiguration config = AutoMapperConfig.Configure(); ;
			IMapper mapper = config.CreateMapper();
			return mapper;
		}
	}
}
