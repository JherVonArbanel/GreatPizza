using AutoMapper;
using GreatPizza.Server.Models;
using GreatPizza.Common.Dtos;
using System.Linq;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;

namespace GreatPizza.Server
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            BundleConfig.RegisterBundles(BundleTable.Bundles);


            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Toping, TopingDto>();
                cfg.CreateMap<Pizza, PizzaDto>()
                   .ForMember(x => x.Topings, y => y.MapFrom(s => s.PizzaTopings
                                                                   .Select(t=> t.Toping)));
            });
        }
    }
}
