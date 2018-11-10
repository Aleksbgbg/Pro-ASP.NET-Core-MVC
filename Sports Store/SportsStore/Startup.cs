namespace SportsStore
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    using SportsStore.Data;
    using SportsStore.Models;

    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<SportsStoreDbContext>(options => options.UseSqlServer(_configuration["Data:SportsStoreProducts:ConnectionString"]));
            services.AddDbContext<AppIdentityDbContext>(options => options.UseSqlServer(_configuration["Data:SportsStoreIdentity:ConnectionString"]));

            services.AddIdentity<IdentityUser, IdentityRole>()
                    .AddEntityFrameworkStores<AppIdentityDbContext>()
                    .AddDefaultTokenProviders();

            services.AddTransient<IProductRepository, ProductRepository>();
            services.AddTransient<IOrderRepository, OrderRepository>();

            services.AddScoped<Cart>(SessionCart.Make);
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddMvc();
            services.AddMemoryCache();
            services.AddSession();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseStatusCodePages();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseStaticFiles();
            app.UseSession();
            app.UseAuthentication();

            app.UseMvc(routeBuilder =>
            {
                routeBuilder.MapRoute(name: null,
                                      template: "{category}/Page{page:int}",
                                      defaults: new
                                      {
                                          Controller = "Product",
                                          Action = "List"
                                      });

                routeBuilder.MapRoute(name: null,
                                      template: "Page{page:int}",
                                      defaults: new
                                      {
                                          Controller = "Product",
                                          Action = "List",
                                          Page = 1
                                      });

                routeBuilder.MapRoute(name: null,
                                      template: "{category}",
                                      defaults: new
                                      {
                                          Controller = "Product",
                                          Action = "List",
                                          Page = 1
                                      });

                routeBuilder.MapRoute(name: null,
                                      template: string.Empty,
                                      defaults: new
                                      {
                                          Controller = "Product",
                                          Action = "List",
                                          Page = 1
                                      });

                routeBuilder.MapRoute("default", "{controller=Product}/{action=List}/{id?}");
            });

            // Seed.EnsurePopulated(app);
            // IdentitySeed.EnsurePopulated(app).ConfigureAwait(false).GetAwaiter().GetResult();
        }
    }
}