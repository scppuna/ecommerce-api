using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using IEcommerceAPI.Data;
using System;
using Microsoft.EntityFrameworkCore;
using IEcommerceAPI.Service;
using System.Data;
using MySql.Data.MySqlClient;
using IEcommerceAPI.Repository;
using IEcommerceAPI.Services;
using IEcommerceAPI.Middleware;
using EcommerceAPI.Data.Repository;
using EcommerceAPI.Services;

namespace IEcommerceAPI
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
            services.AddTransient<IDbConnection>((sp) => new MySqlConnection(Configuration.GetConnectionString("CategoriaConnection")));
            services.AddDbContext<AppDbContext>(opts => opts.UseLazyLoadingProxies().UseMySQL(Configuration.GetConnectionString("CategoriaConnection")));

            services.AddScoped<CategoriaRepository>();
            services.AddScoped<CategoriaService>();
            services.AddScoped<SubcategoriaService>();
            services.AddScoped<SubcategoriaRepository>();
            services.AddScoped<ProdutoRepository>();
            services.AddScoped<ProdutoService>();
            services.AddScoped<CDRepository>();
            services.AddScoped<CentroService>();
            
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "IEcommerceAPI", Version = "v1" });
            });
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "IEcommerceAPI v1"));
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
