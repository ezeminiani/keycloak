using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Owin;
using Owin.Security.Keycloak;
using System;

[assembly: OwinStartup(typeof(KeyCloakWebappSample.Startup))]

namespace KeyCloakWebappSample
{
    public class Startup
    {
        /*
         * Ajuda retirada de:
         * https://github.com/mattmorg55/Owin.Security.Keycloak
         * https://github.com/mattmorg55/Owin.Security.Keycloak/issues/17
         * https://github.com/keycloak/keycloak
         * https://github.com/dylanplecki/KeycloakOwinAuthentication/wiki/ASP.NET-MVC-Tutorial
         * 
         */




        public void Configuration(IAppBuilder app)
        {
            // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=316888

            const string persistentAuthType = "KeycloakWebappSample_cookie_auth";

            
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = persistentAuthType
            });

            app.SetDefaultSignInAsAuthenticationType(persistentAuthType); // Cookie is primary session store

            // --- Keycloak Authentication Middleware - Connects to central Keycloak database
            app.UseKeycloakAuthentication(new KeycloakAuthenticationOptions
            {
                Realm = "transurc",
                ClientId = "keycloak-sample-webapp-frontend",
                //ClientSecret = "72b65eaf-0527-4d66-8ef1-cc4eaf4b4724",        // só é necessário se o cliente é confidencial.
                KeycloakUrl = "http://localhost:8080/auth",
                AuthenticationType = persistentAuthType,
                SignInAsAuthenticationType = persistentAuthType,

                AllowUnsignedTokens = false,
                DisableIssuerSigningKeyValidation = false,
                DisableIssuerValidation = false,
                DisableAudienceValidation = false,
                DisableAllRefreshTokenValidation = true,
                TokenClockSkew = TimeSpan.FromSeconds(2)

            });

        }
    }
}
