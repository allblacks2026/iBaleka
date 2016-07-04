using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
<<<<<<< HEAD
=======
using Microsoft.AspNetCore.Server.IISIntegration;
>>>>>>> 1d33643fbaf15f98ba7a817b3e1159d536cd8a52
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
<<<<<<< HEAD
=======
using iBalekaService.Data.Configurations;
>>>>>>> 1d33643fbaf15f98ba7a817b3e1159d536cd8a52

namespace iBalekaService.Core
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();
<<<<<<< HEAD

            app.UseMvc();
=======
            app.UseMvc();
            app.SeedData();
            //app.UseMvcWithDefaultRoute();
>>>>>>> 1d33643fbaf15f98ba7a817b3e1159d536cd8a52
        }
    }
}
