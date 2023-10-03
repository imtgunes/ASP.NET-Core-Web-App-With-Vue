using Microsoft.AspNetCore.Mvc.Filters;

namespace WebProjeAkademisyenProfil.Areas.Admin.Filters
{
    public class AdminUserSecurityAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.HttpContext.Session.GetString("Kullanici_ID") == null)
            {
                filterContext.HttpContext.Response.Redirect("../HomeAdmin/ErrorHandling");
            }
            
            base.OnActionExecuting(filterContext);
        }
    }
}
