using Microsoft.AspNetCore.Mvc;

namespace MySalonWeb.Areas.Admin.Helpers
{
    public static class NavigationIndicatorHelper
    {
        public static Boolean MakeActiveClass(this IUrlHelper urlHelper, string controller, string action)
        {
            try
            {
                string? controllerName = urlHelper.ActionContext.RouteData.Values["controller"]?.ToString();
                string? methodName = urlHelper.ActionContext.RouteData.Values["action"]?.ToString();
                if (string.IsNullOrEmpty(controllerName)) return false;
                if (controllerName.Equals(controller, StringComparison.OrdinalIgnoreCase))
                {
                    if (methodName != null && methodName.Equals(action, StringComparison.OrdinalIgnoreCase))
                    {
                        return true;
                    }
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static Boolean CheckActiveController(this IUrlHelper urlHelper, string controller)
        {
            try
            {
                string? controllerName = urlHelper.ActionContext.RouteData.Values["controller"]?.ToString();
                if (string.IsNullOrEmpty(controllerName)) return false;
                if (controllerName.Equals(controller, StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
