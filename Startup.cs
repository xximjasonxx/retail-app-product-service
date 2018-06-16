using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProductApi.Data;
using ProductApi.Middleware;

namespace ProductApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            string connectionString = Configuration.GetConnectionString("DefaultConnectionString");
            if (!string.IsNullOrEmpty(Environment.GetEnvironmentVariable("ProductConnectionString")))
            {
                connectionString = Environment.GetEnvironmentVariable("ProductConnectionString");
            }

            if (!string.IsNullOrEmpty(Environment.GetEnvironmentVariable("DB_HOST")))
            {
                connectionString = string.Format("server={0};database=Products;uid={1};pwd=Password01!",
                    Environment.GetEnvironmentVariable("DB_HOST"),
                    Environment.GetEnvironmentVariable("DB_USER"),
                    Environment.GetEnvironmentVariable("DB_PASSWORD")
                );
                Console.WriteLine(connectionString);
            }
            
            services.AddDbContext<ProductDbContext>(
                options => options.UseMySql(connectionString)
            );

            // scoped services
            services.AddScoped<IUserDataContext, UserDataContext>();

            services.AddCors(options => {
                options.AddPolicy("AllowAll",
                    builder => builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials());
            });

            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMiddleware<SetUserContextMiddleware>();
            app.UseCors("AllowAll");
            app.UseMvc();
        }
    }
}
