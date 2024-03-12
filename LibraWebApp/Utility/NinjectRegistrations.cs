using Libra.Dal.Context;
using LibraBll.Abstractions;
using LibraBll.Common;
using LibraBll.DTOs;
using LibraBll.Repositories;
using Ninject.Modules;

namespace LibraWebApp.Utility
{
	public class NinjectRegistrations : NinjectModule
	{
		public override void Load()
		{
			Bind<IRepository<UserDTO>>().To<UserRepository>();
			Bind<IRepository<PosDTO>>().To<PosRepository>();
			
			// bind DbContext
			Bind<LibraContext>().ToSelf().InSingletonScope();

		}
	}
}