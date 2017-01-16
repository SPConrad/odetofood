using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Routing;
using OdeToFood.Services;
using OdeToFood.Entities;
using Microsoft.EntityFrameworkCore;

namespace OdeToFood
{
    public class Startup
    {

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json");

            Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; set; }


        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddSingleton(Configuration);
            services.AddSingleton<IGreeter, Greeter>();
            services.AddScoped<IRestaurantData, SqlRestaurantData>();
            services.AddDbContext<OdeToFoodDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("OdeToFood")));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(
            IApplicationBuilder app, 
            IHostingEnvironment env, 
            ILoggerFactory loggerFactory,
            IGreeter greeter)
        {
            loggerFactory.AddConsole();

            if (env.IsDevelopment())
            {
                ///does the environment have a custom name?
                //env.IsEnvironment("spork");
                app.UseDeveloperExceptionPage();
            } else
            {
                app.UseExceptionHandler(new ExceptionHandlerOptions
                {
                    ExceptionHandler = context => context.Response.WriteAsync("Oops")
                });
            }

            /// "Terminal" middleware, nothing else will be called after this
            //app.UseWelcomePage();

            ///usewelcomepage will open if you navigate to /welcome
            //app.UseWelcomePage("/welcome");

            //app.UseDefaultFiles();
            //app.UseStaticFiles();
            //UseFileServer combines Default and Static
            app.UseFileServer();


            /*app.UseWelcomePage(new WelcomePageOptions
            {
                Path = "/welcome"
            });

            app.Run(async (context) =>
            {
                //throw new Exception("Something went wrong!");
                var message = greeter.GetGreeting();
                await context.Response.WriteAsync(message);
            });*/

            ///Now that we have added MVC.ASPNet.Core
            //app.UseMvcWithDefaultRoute();


            ///installs MVC but doesn't use any routing rules
            app.UseMvc(ConfigureRoutes);

            ///allows app to return Not Found if the route is not found
            app.Run(ctx => ctx.Response.WriteAsync("Not found"));
        }


        private void ConfigureRoutes(IRouteBuilder routeBuilder)
        {
            ///
            /// CONVENTION BASED ROUTING
            /// 

            // got a request for /Home/Index
            //MVC determines you want HomeController, and the "Index" action

            ///"{controller}" determines that "Controller" will be appended to whatever the first item in the URL is.
            /// So /Home/ becomes HomeController. =Home means if you can't find a Controller, the default will be Home.
            /// action dictates it will be the action, or method
            /// ID becomes an optional item with the ?
            routeBuilder.MapRoute("Default",
                "{controller=Home}/{action=Index}/{id?}");
            
        }
    }
}
