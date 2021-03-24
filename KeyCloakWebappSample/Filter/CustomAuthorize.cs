using Microsoft.Owin.Security.Authorization.Mvc;
using System.Web.Mvc;
using System.Web.Routing;

namespace KeyCloakWebappSample.Filter
{
    /// <summary>
    /// Atributo Authorize customizado.
    /// Tive que sobrescrever o 'ResourceAuthorizeAttribute' do pacote 'Microsoft.Owin.Security.Authorization.Mvc'
    /// em num novo filtro para forçar o redirecionamento após o falha de autorização.
    /// </summary>
    public class CustomAuthorize : ResourceAuthorizeAttribute
    {
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            if (!filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                filterContext.Result = new HttpUnauthorizedResult();
            }
            else
            {
                filterContext.Result = new RedirectToRouteResult(new
                    RouteValueDictionary(new { controller = "Home", action = "NaoAutorizado" }));
            }
        }
    }



    /*
    public class CustomAuthorize : AuthorizeAttribute, IAuthorizeData
    {
        public string Policy { get; set; }
        public string ActiveAuthenticationSchemes { get; set; }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            if (!filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                filterContext.Result = new HttpUnauthorizedResult();
            }
            else
            {
                filterContext.Result = new RedirectToRouteResult(new
                    RouteValueDictionary(new { controller = "Home", action = "NaoAutorizado" }));
            }
        }
    }
    */
}