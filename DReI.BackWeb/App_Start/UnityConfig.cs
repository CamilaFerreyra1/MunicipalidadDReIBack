using System.Web.Http;
using Unity;
using Unity.WebApi;
using Unity.Lifetime;
using DReI.BackWeb.Data;
using DReI.BackWeb.Services;
using DReI.BackWeb.Services.Utils;


namespace DReI.BackWeb
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();


            // Registrar servicios
            container.RegisterType<ApplicationDbContext>(new HierarchicalLifetimeManager());

            container.RegisterType<ContribuyentesService>();
            container.RegisterType<CaratulaService>();
            container.RegisterType<ConfiguracionTributariaService>();
            container.RegisterType<DeclaracionesService>();
            container.RegisterType<MonotributoService>();
            container.RegisterType<RegimenExcepcionService>();
            container.RegisterType<RutinasService>();
            container.RegisterType<VigenciasService>();
            container.RegisterType<TablasService>();
            container.RegisterType<NomencladorService>();
            container.RegisterType<RegimenSimplificadoService>();
            container.RegisterType<RubroContribuyenteService>();
            container.RegisterType<RubroActividadService>();
            container.RegisterType<RubroService>();


            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}