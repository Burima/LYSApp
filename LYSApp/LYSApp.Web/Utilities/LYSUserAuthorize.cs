using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace LYSApp.Web.Utilities
{
    public class LYSUserAuthorize : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (LYSApp.Web.Services.SessionManager.GetSessionUser()!= null)
            {                
                return true;
            }
            return false;
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            if (filterContext.HttpContext.Request.IsAjaxRequest())
            {
                var urlHelper = new UrlHelper(filterContext.RequestContext);
                filterContext.HttpContext.Response.StatusCode = 403;
                filterContext.Result = new JsonResult
                {
                    Data = new
                    {
                        Error = "NotAuthorized",
                        LogOnUrl = urlHelper.Action("Login", "Account", new { SessionTimeout = true })
                    },
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            }
            else
            {
                var routeValues = new RouteValueDictionary();

                routeValues.Add("Controller", "Account");
                routeValues.Add("Action", "Login");
                routeValues.Add("SessionTimeout", true);
                if (filterContext.HttpContext.Request.UrlReferrer != null)
                    routeValues.Add("ReturnUrl", filterContext.HttpContext.Request.UrlReferrer.PathAndQuery);
                filterContext.Result = new RedirectToRouteResult(routeValues);
            }
        }
    }
}