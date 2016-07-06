using Microsoft.AspNetCore.Builder;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using iBalekaService.Data.Configurations;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;

namespace iBalekaService.Core
{
    public class Startup
    {

        public IConfigurationRoot Configuration { get; }
        public Startup()
        {

        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddMvc();           
            var connection = @"Server=(localdb)\mssqllocaldb;Database=iBalekaDB;Trusted_Connection=True;";
            services.AddDbContext<IBalekaContext>(options => options.UseSqlServer(connection));

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app)
        {

            app.UseMvc();
            app.SeedData();
            //app.UseMvcWithDefaultRoute();
        }
    }
}
