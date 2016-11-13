using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using FileStorageMVC.Models;
using Microsoft.EntityFrameworkCore;

namespace FileStorageMVC
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            services.AddLogging();
            var dir = System.IO.Directory.GetCurrentDirectory();
            if(System.IO.File.Exists("./db/FileStorage.db"))
            {
                services.AddDbContext<FileContext>(options=>options.UseSqlite("Filename=./db/FileStorage.db"));
            }
            else
            {
                services.AddDbContext<FileContext>(options=>options.UseSqlite("Filename=./FileStorage.db"));
            }
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // app.Run(async (context) =>
            // {
            //     await context.Response.WriteAsync("Hello World!");
            // });

            app.UseMvc();
        }
    }
}
