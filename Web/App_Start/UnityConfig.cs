using DAL.Implementations;
using DAL.Interfaces;
using Microsoft.Practices.Unity;
using System.Web.Http;
using Services.Implementations;
using Services.Interfaces;
using Unity.WebApi;

namespace Web
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            container.RegisterType<IThemesService, ThemesService>(new HierarchicalLifetimeManager());
            container.RegisterType<IMessageService, MessageService>(new HierarchicalLifetimeManager());
            container.RegisterType<IUnitOfWork, UnitOfWork>(new HierarchicalLifetimeManager());

            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}