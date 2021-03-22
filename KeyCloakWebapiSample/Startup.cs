using Microsoft.Owin;
using Owin;
using Owin.Security.Keycloak;

[assembly: OwinStartup(typeof(KeyCloakWebapiSample.Startup))]

namespace KeyCloakWebapiSample
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=316888

            const string persistentAuthType = "KeycloakWebapiSample_cookie_auth";
  

            // --- Keycloak Authentication Middleware - Connects to central Keycloak database
            app.UseKeycloakAuthentication(new KeycloakAuthenticationOptions
            {
                Realm = "transurc",
                ClientId = "keycloak-sample-webapp-backend",
                ClientSecret = "d138891d-59be-424b-ad7a-28afd462e610",        // só é necessário se o cliente é confidencial.
                KeycloakUrl = "http://localhost:8080/auth",
                EnableWebApiMode = true,
                
                AuthenticationType = persistentAuthType

            });

        }
    }
}
