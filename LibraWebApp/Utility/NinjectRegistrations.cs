using Libra.Dal.Context;
using LibraBll.Abstractions;
using LibraBll.Abstractions.Repositories;
using LibraBll.Common;
using LibraBll.DTOs;
using LibraBll.Repositories;
using Ninject.Modules;
using Ninject.Web.Common;

namespace LibraWebApp.Utility
{
	public class NinjectRegistrations : NinjectModule
	{
		public override void Load()
		{
			Bind<IUserRepository>().To<UserRepository>().InRequestScope();
			Bind<IPosRepository>().To<PosRepository>().InRequestScope();
			
			// bind DbContext
			Bind<LibraContext>().ToSelf().InSingletonScope();
		}
	}
}