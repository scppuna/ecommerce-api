using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using SprintCinco.Data;
using System;
using Microsoft.EntityFrameworkCore;
using SprintCinco.Service;
using System.Data;
using MySql.Data.MySqlClient;
using SprintCinco.Dao;
using SprintCinco.Services;
using SprintCinco.Middleware;

namespace SprintCinco
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
            services.AddScoped<CategoriaDao>();
            services.AddScoped<SubcategoriaDao>();
            services.AddScoped<ProdutoDao>();
            services.AddScoped<ProdutoService>();
            services.AddScoped<CDDao>();
            services.AddScoped<CentroService>();
            services.AddDbContext<AppDbContext>(opts => opts.UseLazyLoadingProxies().UseMySQL(Configuration.GetConnectionString("CategoriaConnection")));
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "SprintCinco", Version = "v1" });
            });
            services.AddTransient<IDbConnection>((sp) => new MySqlConnection(Configuration.GetConnectionString("CategoriaConnection")));
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "SprintCinco v1"));
            }

            app.UseHttpsRedirection();

            app.UseMiddleware(typeof(ErroMiddleware));

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
