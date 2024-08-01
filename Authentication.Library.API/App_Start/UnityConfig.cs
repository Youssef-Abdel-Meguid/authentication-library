using Authentication.Library.AuthenticationManager;
using Authentication.Library.AuthenticationManager.Contract;
using Authentication.Library.AuthenticationManager.Handlers;
using Authentication.Library.AuthenticationManager.Models;
using Authentication.Library.BLL;
using System.Web.Http;
using Unity;
using Unity.WebApi;

namespace Authentication.Library.API.App_Start
{
    public class UnityConfig
    {
        public static AuthenticationBLL AuthenticationBLL;
        public static JwtAuthenticationHandler JwtAuthenticationHandler;

        public static void RegisterComponent()
        {
            var container = new UnityContainer();

            container.RegisterInstance(new JwtTokenConfiguration
            {
                Issuer = "LinkDev Team",
                Audience = "Consumer",
                Secret = "36B23F7F4B2E5A4C0F17B7C4B4BDDC16",
                AccessTokenExpiration = 1
            });

            container.RegisterType<IAuthenticationManager, JwtAuthentication>();

            AuthenticationBLL = container.Resolve<AuthenticationBLL>();
            JwtAuthenticationHandler = container.Resolve<JwtAuthenticationHandler>();

            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}