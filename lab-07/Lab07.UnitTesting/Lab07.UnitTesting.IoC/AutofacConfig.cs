using System.Diagnostics.CodeAnalysis;
using System.Web;
using Autofac;
using AutoMapper;
using Lab07.UnitTesting.BusinessLogic.Interfaces;
using Lab07.UnitTesting.BusinessLogic.Services;
using Lab07.UnitTesting.DAL.Core.Context;
using Lab07.UnitTesting.DAL.Interfaces;
using Lab07.UnitTesting.DAL.Repositories;
using Microsoft.Owin.Security;
using Lab07.UnitTesting.DAL.Interfaces.Repositories;
using Autofac.Integration.Mvc;
using System.Reflection;
using Lab07.UnitTesting.AutoMapper;

namespace Lab07.UnitTesting.IoC
{
    [ExcludeFromCodeCoverage]
    public static class AutofacConfig
    {
        private static void RegisterServices(ContainerBuilder builder)
        {
            builder.RegisterType<UserService>().As<IUserService>().InstancePerRequest();
            builder.RegisterType<StoreService>().As<IStoreService>().InstancePerRequest();
            builder.RegisterType<StoreTypeService>().As<IStoreTypeService>().InstancePerRequest();
        }

        private static void RegisterRepository(ContainerBuilder builder)
        {
            builder.RegisterType<StoreTypeReposiroty>().As<IStoreTypeRepository>().InstancePerRequest();
            builder.RegisterType<StoreRepository>().As<IStoreRepository>().InstancePerRequest();
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerRequest();
        }

        private static void RegisterContext(ContainerBuilder builder)
        {
            builder.RegisterType<GodelBenefitContext>().AsSelf().InstancePerRequest();
            builder.Register(c => HttpContext.Current.GetOwinContext().Authentication).As<IAuthenticationManager>();
        }

        private static void RegisterAutomapper(ContainerBuilder builder)
        {
            builder.RegisterType<AutomapperConfig>().AsSelf().SingleInstance();
            builder.Register(c => new MapperConfiguration(cfg => cfg.AddProfile(c.Resolve<AutomapperConfig>()))).AsSelf().SingleInstance();
            builder.Register(c => c.Resolve<MapperConfiguration>().CreateMapper(c.Resolve)).As<IMapper>().InstancePerLifetimeScope();
        }
        private static Assembly GetWebEntryAssembly()
        {
            var proxyApplicationInstance = HttpContext.Current.ApplicationInstance;
            var proxyApplicationInstanceType = proxyApplicationInstance.GetType();
            var baseApplicationInstanceType = proxyApplicationInstanceType.BaseType;
            return baseApplicationInstanceType?.Assembly;
        }

        public static ContainerBuilder ConfigureContainer()
        {
            var builder = new ContainerBuilder();
            var entryAssembly = GetWebEntryAssembly();
            builder.RegisterControllers(entryAssembly);
            RegisterServices(builder);
            RegisterContext(builder);
            RegisterRepository(builder);
            RegisterAutomapper(builder);
            return builder;
        }
    }
}