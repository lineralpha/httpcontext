namespace System.Web
{
    using Microsoft.AspNetCore.Http;

    /// <summary>
    /// This <see cref="System.Web.HttpContext"/> class defines property <see cref="Current"/>
    /// that allows client code to access HttpContext for the current request session.
    /// <para>
    /// This helps migrating existing ASP.NET code to ASPCore.Net.  
    /// </para>
    /// </summary>
    public static class HttpContext
    {
        private static IHttpContextAccessor _contextAccesspr;

        public static Microsoft.AspNetCore.Http.HttpContext Current
        {
            get
            {
                
                var accessor = _contextAccesspr ?? throw new InvalidOperationException();
                return accessor.HttpContext;
            }
        }

        internal static void Configure(IHttpContextAccessor contextAccessor)
        {
            _contextAccesspr = contextAccessor;
        }
    }
}
