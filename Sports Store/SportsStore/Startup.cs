namespace SportsStore
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    using SportsStore.Data;
    using SportsStore.Models;

    public class Startup
    {
        private readonly IConfigurationRoot _configuration;

        public Startup(IHostingEnvironment env)
        {
            _configuration = new ConfigurationBuilder().SetBasePath(env.ContentRootPath)
                                                       .AddJsonFile("appsettings.json")
                                                       .Build();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<SportsStoreDbContext>(options => options.UseSqlServer(_configuration["Data:SportsStoreProducts:ConnectionString"]));

            services.AddTransient<IProductRepository, ProductRepository>();

            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseStatusCodePages();
            }

            app.UseMvc(routeBuilder => routeBuilder.MapRoute("default", "{controller=Product}/{action=List}/{id?}"));
            app.UseStaticFiles();

            Seed.EnsurePopulated(app);
        }
    }
}