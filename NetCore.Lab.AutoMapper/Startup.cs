using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NetCore.Lab.AutoMapper.Data;
using AutoMapper;
using AutoMapper.EquivalencyExpression;

namespace NetCore.Lab.AutoMapper
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

            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen();
            services.ConfigureSwaggerGen(options =>
            {
                //解決重名導致無法產生swagger文件的問題
                options.CustomSchemaIds(x => x.FullName);
            });

            //資料庫存放在記憶體
            services
                .AddDbContext<NetCoreLabAutoMapperContext>(options =>
                           //options.UseSqlServer(Configuration.GetConnectionString("NetCoreLabAutoMapperContext"))
                           options.UseInMemoryDatabase(databaseName: "Test")
                    );

            //使用Add AutoMapper DI
            services.AddAutoMapper((serviceProvider, automapper) =>
            {
                //using AutoMapper.EquivalencyExpression;
                automapper.AddCollectionMappers();
                automapper.UseEntityFrameworkCoreModel<NetCoreLabAutoMapperContext>(serviceProvider);
            },
                //全專案
                AppDomain.CurrentDomain.GetAssemblies()
            );

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                c.RoutePrefix = string.Empty;
            });

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
