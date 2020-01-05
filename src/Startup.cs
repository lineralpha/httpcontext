using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace NetCoreHttpContextApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            // injects the default implementation of IHttpContextAccessor service,
            // which can be retrieved later from the service collection.
            //
            // really, it should be AddDefaultHttpContextAccessor.
            //
            // for each incoming http request, the default implementation of IHttpContextFactory,
            // which is instantiated and passed into the app in the process of bootstraping,
            // initializes an HttpContext instance, and looks up the DI container (service collection)
            // for IHttpContextAccessor. If an IHttpContextAccessor is there, the factory will wire the
            // HttpContext to the accessor.
            // https://docs.microsoft.com/en-us/aspnet/core/fundamentals/http-context?view=aspnetcore-3.1#use-httpcontext-from-custom-components
            services.AddHttpContextAccessor();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            // the injected IHttpContextAccessor is bound to IHttpContextFactory
            app.UseStaticHttpContext();
            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
