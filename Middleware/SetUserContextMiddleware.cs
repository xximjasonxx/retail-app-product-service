
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Builder;
using ProductApi.Common;
using ProductApi.Extensions;
using ProductApi.Models;
using System;

namespace ProductApi.Middleware
{
    public class SetUserContextMiddleware
    {
        private readonly RequestDelegate nextDelegate;
        public SetUserContextMiddleware(RequestDelegate del)
        {
            this.nextDelegate = del;
        }

        public async Task Invoke(HttpContext context)
        {
            // verify that the user supplied the Authorization Header
            if (context.Request.Headers.Keys.Contains("Authorization"))
            {
                string authorizationToken = context.Request.Headers["Authorization"];
                User registeredUser = await authorizationToken.ValidateToken();

                // add it to the request context
                IUserDataContext dataContext = (IUserDataContext)context.RequestServices.GetService(typeof(IUserDataContext));
                dataContext.SetCurrentUser(registeredUser);
            }

            await this.nextDelegate.Invoke(context);
        }
    }
}