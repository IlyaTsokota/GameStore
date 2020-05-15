using GameStore.Web.App_Start;
using GameStore.Web.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace GameStore.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            Bootstrapper.Run();
        }

        protected void Application_Error(Object sender, EventArgs e)
        {
            var httpContext = ((MvcApplication)sender).Context;
            var currentController = string.Empty;
            var currentAction = string.Empty;
            var currentRouteData = RouteTable.Routes.GetRouteData(new HttpContextWrapper(httpContext));

            if (currentRouteData != null)
            {
                if (!string.IsNullOrEmpty(currentRouteData.Values["controller"].ToString()))
                {
                    currentController = currentRouteData.Values["controller"].ToString();
                }
                if (!string.IsNullOrEmpty(currentRouteData.Values["action"].ToString()))
                {
                    currentAction = currentRouteData.Values["action"].ToString();
                }
            }

            // пойманное исключение
            var ex = Server.GetLastError();

            //// тут запись в мой журнал, в этой же точке можно отправлять письма админам
            Logger.Log.Error(ex);

            var controller = new ErrorController();
            var routeData = new RouteData();
            // метод по умолчанию в контроллере
            var action = "Index";

            // если это ошибки HTTP, а не моего кода, то для них свои представления
            if (ex is HttpException)
            {
                switch (((HttpException)ex).GetHttpCode())
                {
                    case 403:
                        action = "AccessDenied";
                        break;
                    case 404:
                        action = "NotFound";
                        break;
                    default:
                        action = "HttpError";
                        break;
                        // можно добавить свои методы контроллера для любых кодов ошибок
                }
            }
            httpContext.ClearError();
            httpContext.Response.Clear();
            httpContext.Response.StatusCode = ex is HttpException ? ((HttpException)ex).GetHttpCode() : 500;
            httpContext.Response.TrySkipIisCustomErrors = true;

            routeData.Values["controller"] = "Error";
            routeData.Values["action"] = action;

            controller.ViewData.Model = new HandleErrorInfo(ex, currentController, currentAction);
            ((IController)controller).Execute(new RequestContext(new HttpContextWrapper(httpContext), routeData));
        }
    }
}
