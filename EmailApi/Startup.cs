using EmailApi.API.Filters;
using EmailApi.CrossCutting;
using EmailApi.Domain.IRepository;
using EmailApi.Infrastructure;
using EmailApi.Infrastructure.Contexto;
using EmailApi.Infrastructure.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmailApi
{
    public class Startup
    {

        private const string APP_NAME = "Api Email";
        private const string CORS_PROD = "_cors_prod";
        private const string CORS_DEV = "_cors_dev";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                //options.AddPolicy(name: CORS_PROD, builder => { builder.WithOrigins("https://www.site.com.br/").AllowAnyHeader().AllowAnyMethod(); });
                options.AddPolicy(name: CORS_PROD, builder => { builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod(); });
                options.AddPolicy(name: CORS_DEV, builder => { builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod(); });
            });

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "EmailApi", Version = "v1" });
            });


            services.AddAutoMapper(AssemblyUteis.GetCurrentAssemblies());

            services.AddResolvedorDeDependencias();

            services.AddDbContext<DBContexto>(options => options.UseSqlServer(
                                                        Configuration.GetConnectionString("DefaultConnection"),
                                                        sqlServerOptions => sqlServerOptions.CommandTimeout(120)));

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "EmailApi v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
