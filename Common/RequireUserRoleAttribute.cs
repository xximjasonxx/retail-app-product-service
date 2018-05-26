
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ProductApi.Common
{
    public class RequireUserRoleAttribute : ActionFilterAttribute
    {
        public string[] AllowedRoles { get; set; }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var userContext = context.HttpContext.RequestServices.GetService(typeof(IUserDataContext)) as IUserDataContext;
            
            // if this isnt here, the order is wrong for our filters
            string userRole = userContext.CurrentUser.Role;
            if (AllowedRoles.Length > 0 && !AllowedRoles.Contains(userRole))
            {
                context.Result = new StatusCodeResult(403);
            }
        }
    }
}