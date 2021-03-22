using Microsoft.AspNet.Identity;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Jwt;
using Newtonsoft.Json;
using Owin;
using System.Net.Http;

[assembly: OwinStartup(typeof(KeyCloakWebapiSample.Startup))]

namespace KeyCloakWebapiSample
{
    public class Startup
    {
        /*
         * Utilizando o 'Microsoft.Owin.Security.Jwt'
         * faz a validação do Bearer Token.
         */

        public void Configuration(IAppBuilder app)
        {
            using (var http = new HttpClient())
            {
                var keysResponse = http.GetAsync("http://localhost:8080/auth/realms/transurc/protocol/openid-connect/certs").Result;

                var rawKeys = keysResponse.Content.ReadAsStringAsync().Result;

                JsonWebKeySet jsonWebKeySet = JsonConvert.DeserializeObject<JsonWebKeySet>(rawKeys);

                app.UseJwtBearerAuthentication(new JwtBearerAuthenticationOptions
                {
                    AuthenticationType = DefaultAuthenticationTypes.ExternalBearer,
                    AuthenticationMode = AuthenticationMode.Active,
                    Realm = "transurc",


                    TokenValidationParameters = new TokenValidationParameters()
                    {
                        AuthenticationType = "Bearer",
                        ValidateIssuer = true,
                        ValidateIssuerSigningKey = true,
                        ValidAudiences = new string[] { "keycloak-sample-webapp-backend", "keycloak-sample-webapp-frontend" },
                        ValidIssuer = "http://localhost:8080/auth/realms/transurc",
                        ValidateLifetime = true,
                        ValidateAudience = true,
                        IssuerSigningKeys = jsonWebKeySet.GetSigningKeys(),

                    }
                });
            }
        }



        /*
         * Utilizando o componente 'Owin.Security.Keycloak-3' para o UseKeycloakAuthentication não funciona corretamente, 
         * ao passar o token via Headers não faz a validação.
         * 
        public void Configuration(IAppBuilder app)
        {
            // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=316888

            // const string persistentAuthType = "KeycloakWebapiSample_cookie_auth";

            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = "Bearer"
            });

            app.UseKeycloakAuthentication(new KeycloakAuthenticationOptions
            {
                Realm = "transurc",
                ClientId = "keycloak-sample-webapp-backend",
                ClientSecret = "d138891d-59be-424b-ad7a-28afd462e610",

                KeycloakUrl = "http://localhost:8080/auth",
                AuthenticationType = "Bearer",
                SignInAsAuthenticationType = "Bearer",

                AllowUnsignedTokens = false,
                DisableIssuerSigningKeyValidation = false,
                DisableIssuerValidation = false,
                UseRemoteTokenValidation = true,
                EnableWebApiMode = true,
                DisableAudienceValidation = false,
                Scope = "openid",

            });

        }
        */

    }
}
