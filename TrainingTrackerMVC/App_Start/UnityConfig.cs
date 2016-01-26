using System.Web.Mvc;
using Microsoft.Practices.Unity;
using Unity.Mvc5;
using Business;
using TrainingTrackerMVC.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using System.Web;

namespace TrainingTrackerMVC
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
            var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();
            container.RegisterType<IUserStore<ApplicationUser>, UserStore<ApplicationUser>>(new InjectionConstructor(typeof(ApplicationDbContext)));
            container.RegisterType<IAuthenticationManager>(new InjectionFactory(c => HttpContext.Current.GetOwinContext().Authentication));
            container.RegisterType<ApplicationDbContext>();
            container.RegisterType<ApplicationSignInManager>();
            container.RegisterType<ApplicationUserManager>();
            container.RegisterType<IPersonBusiness, PersonBusiness>();
            container.RegisterType<ILogBusiness, LogBusiness>();
            container.RegisterType<IExerciseBusiness, ExerciseBusiness>();
            container.RegisterType<ISetBusiness, SetBusiness>();

            BusinessUnityConfig.RegisterTypes(container);

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}