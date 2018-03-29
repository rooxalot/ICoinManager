using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using System.Web.Http;
using Microsoft.Owin.Cors;
using Microsoft.Owin.Security.OAuth;
using ICoinManager.API.Models;

[assembly: OwinStartup(typeof(ICoinManager.API.Startup))]

namespace ICoinManager.API
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var config = new HttpConfiguration();
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            //Habilita CORS nas requisições da API
            app.UseCors(CorsOptions.AllowAll);

            //Habilita a utilização de Tokens
            EnableTokens(app);

            //Habilita a API
            app.UseWebApi(config);
        }

        public void EnableTokens(IAppBuilder app)
        {
            var authOptions = new OAuthAuthorizationServerOptions
            {
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromHours(1),
                Provider = new ProviderTeste() // Posteriormente,criar um provider real.
            };

            app.UseOAuthAuthorizationServer(authOptions);
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());
        }
    }
}
