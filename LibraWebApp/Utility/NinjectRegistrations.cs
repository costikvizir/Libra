using Libra.Dal.Context;
using LibraBll.Abstractions.Repositories;
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
            Bind<IIssueRepository>().To<IssueRepository>().InRequestScope();

            // bind DbContext
            Bind<LibraContext>().ToSelf().InSingletonScope();
        }
    }
}