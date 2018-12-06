namespace UrlsAndRouting
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Routing;
    using Microsoft.Extensions.DependencyInjection;

    using UrlsAndRouting.Constraints;

    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<RouteOptions>(options =>
            {
                options.ConstraintMap.Add("exactValue", typeof(ExactValueConstraint));
            });

            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();

            app.UseMvc(routeBuilder =>
            {
                routeBuilder.MapRoute(name: "constraint",
                                      template: "{controller:regex(^H)}/{action:exactValue(SomeId)}/{id:int}");

                routeBuilder.MapRoute(name: "catchall",
                                      template: "{controller=Home}/{action=VariableLength}/{*catchall}");

                routeBuilder.MapRoute(name: "default",
                                      template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}