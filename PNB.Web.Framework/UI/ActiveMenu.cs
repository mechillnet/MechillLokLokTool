using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;

namespace PNB.Web.Framework.UI
{
    public static class ActiveMenu
    {
     public static string IsActive(this IHtmlHelper htmlHelper, string controller, string action)
    {
        var routeData = htmlHelper.ViewContext.RouteData;

        var routeAction = routeData.Values["action"].ToString();
        var routeController = routeData.Values["controller"].ToString();

        var returnActive = (controller == routeController && (action == routeAction ));

        return returnActive ? "active" : "";
    }
        public static string IsActive(this IHtmlHelper htmlHelper, string controller)
        {
            var routeData = htmlHelper.ViewContext.RouteData;

       
            var routeController = routeData.Values["controller"].ToString();

            var returnActive = (controller == routeController);

            return returnActive ? "active" : "";
        }
    }
   
}
