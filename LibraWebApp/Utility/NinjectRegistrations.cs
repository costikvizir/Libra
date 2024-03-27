using FluentValidation;
using Libra.Dal.Context;
using LibraBll.Abstractions.Repositories;
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
            Bind<IIssueRepository>().To<IssueRepository>().InRequestScope();

            // bind DbContext
            Bind<LibraContext>().ToSelf().InSingletonScope();
			// make a list of all validators in the assembly
			//var validatorsList = AssemblyScanner.FindValidatorsInAssemblyContaining<CreateUserModel>();
			var validatorsList = AssemblyScanner.FindValidatorsInAssemblyContaining<AddUserDTO>();

			// loop through the list of validators and bind them to their respective interfaces
			foreach (var validator in validatorsList)
			{
				Bind(validator.InterfaceType).To(validator.ValidatorType);
			}
		}
    }
}