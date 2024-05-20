using FluentValidation;
using Libra.Dal.Context;
using LibraBll.Abstractions.Repositories;
using LibraBll.DTOs.User;
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

            Bind<LibraContext>().ToSelf().InSingletonScope();
 
            //var validatorsList = AssemblyScanner.FindValidatorsInAssemblyContaining<CreateUserModel>();
            var validatorsUserList = AssemblyScanner.FindValidatorsInAssemblyContaining<AddUserDTO>();
            //var validatorsPosList = AssemblyScanner.FindValidatorsInAssemblyContaining<PosDTO>();

            //loop through the list of validators and bind them to their respective interfaces
            foreach (var validator in validatorsUserList)
            {
                Bind(validator.InterfaceType).To(validator.ValidatorType);
            }
        }
    }
}