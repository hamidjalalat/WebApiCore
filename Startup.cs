using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;

namespace webapicore
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
            services.AddControllers();
            services.AddCors(options =>
            {
                options.AddPolicy(name: "alaki",
                                  policy =>
                                  {
                                      policy.AllowAnyOrigin()
                                                          .AllowAnyHeader()
                                                          .AllowAnyMethod(); 
                                      
                                  });
            });
            services.AddDbContext<Data.DatabaseContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("hjconnection"));
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            //app.UseMyMiddleware();
            //app.Map("/hamid", exec);
            //app.Use(async (context,next)=>
            //{
            //    await context.Response.WriteAsync("hamid jalalat1");
            //    await next();
            //    await context.Response.WriteAsync("<h1> hamid jalallat3 </h1>");
            //});


            //app.Run(async contaxt =>
            //{
            //    await contaxt.Response.WriteAsync("<h1> hamid jalallat2 </h1>");

            //});
            //app.UseHttpsRedirection();
            app.UseCors("alaki");

            app.UseRouting();

            //app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
        //private void exec(IApplicationBuilder app)
        //{
        //    app.Run(async contaxt =>
        //    {
        //        await contaxt.Response.WriteAsync("<h1> hamid jalallat2 with map </h1>");

        //    });
        //}
    }
}
