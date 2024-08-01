using System.Web.Http;
using Authentication.Library.API.App_Start;

namespace Authentication.Library.API
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            UnityConfig.RegisterComponent();

            config.MessageHandlers.Add(UnityConfig.JwtAuthenticationHandler);
            config.MapHttpAttributeRoutes();

            GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Serialize;
            GlobalConfiguration.Configuration.Formatters.Remove(GlobalConfiguration.Configuration.Formatters.XmlFormatter);

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
