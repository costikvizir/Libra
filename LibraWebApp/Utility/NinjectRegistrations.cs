using FluentValidation;
using Libra.Dal.Context;
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
            Bind<IIssueRepository>().To<IssueRepository>().InRequestScope();

            // bind DbContext
            Bind<LibraContext>().ToSelf().InSingletonScope();
			// make a list of all validators in the assembly
			//var validatorsList = AssemblyScanner.FindValidatorsInAssemblyContaining<CreateUserModel>();
			var validatorsUserList = AssemblyScanner.FindValidatorsInAssemblyContaining<AddUserDTO>();
            //var validatorsPosList = AssemblyScanner.FindValidatorsInAssemblyContaining<PosDTO>();

			//loop through the list of validators and bind them to their respective interfaces
			foreach (var validator in validatorsUserList)
			{
				Bind(validator.InterfaceType).To(validator.ValidatorType);
			}

			//foreach (var validator in validatorsPosList)
			//{
			//	Bind(validator.InterfaceType).To(validator.ValidatorType);
			//}
		}
    }
}