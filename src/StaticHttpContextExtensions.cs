using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace System.Web
{
    public static class StaticHttpContextExtensions
    {
        public static IApplicationBuilder UseStaticHttpContext(this IApplicationBuilder app)
        {
            // https://www.strathweb.com/2016/12/accessing-httpcontext-outside-of-framework-components-in-asp-net-core/

            // this call throws nicely if the IHttpContextAccessor hasn't been injected
            var httpContextAccessor = app.ApplicationServices.GetRequiredService<IHttpContextAccessor>();
            // after this configuration, System.Web.HttpContext.Current returns the Http Context
            // of the current request session.
            System.Web.HttpContext.Configure(httpContextAccessor);
            return app;
        }
    }
}
