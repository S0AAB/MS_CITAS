using Autofac;
using Autofac.Integration.WebApi;
using MS_CITAS.Interfaces;
using MS_CITAS.Repositories;
using MS_CITAS.Services;
using System.Reflection;
using System.Web.Http;
using MS_CITAS.Domain.Models;
using MS_CITAS.Application.Services.Implementation;
using AutoMapper;
using MS_CITAS.Application.Mappings;

namespace MS_CITAS.App_Start
{
	public class AutofacConfig
    {
        public static void Register()
        {
            var builder = new ContainerBuilder();

            //Registro del contexto
            builder.RegisterType<CitasDBEntities>().InstancePerLifetimeScope();

            // Registrar Repositorio y Servicio
            builder.RegisterType<CitaRepository>().As<ICitaRepository>().InstancePerRequest();
            builder.RegisterType<CitaService>().As<ICitaService>().InstancePerRequest();

            //Registro servicio API Personas
            builder.RegisterType<PersonaServiceAPI>().SingleInstance();

            //Emisor RabbitMQ - Cambiar a scope
            builder.RegisterType<EmisorMQ>().AsSelf().SingleInstance();

            // Configurar Web API
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            //Configuracion automapper
            builder.Register(ctx => new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new CitaMapping());
            })).AsSelf().SingleInstance();  

            builder.Register(ctx => ctx.Resolve<MapperConfiguration>().CreateMapper())
                .As<IMapper>()
                .InstancePerLifetimeScope();

            var container = builder.Build();
            GlobalConfiguration.Configuration.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }
    }
}