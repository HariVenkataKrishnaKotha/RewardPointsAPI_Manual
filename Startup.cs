using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog.Extensions.Logging.File;
using Microsoft.OpenApi.Models;
using RewardPointsAPI_Manual.Filters;
using RewardPointsAPI_Manual.Middleware;

namespace RewardPointsAPI_Manual
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
            services.AddControllers();
            services.AddMvc(options => options.Filters.Add(typeof(ExceptionFilter)));
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Rewards API", Version = "v1" });
            });
            services.AddLogging(configure => configure.AddFile().SetMinimumLevel(Configuration.GetValue<LogLevel>("Logging:File:LogLevel:Default")));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseMiddleware<RequestLoggingMiddleware>();
            app.UseRouting();
            app.UseAuthorization();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "Rewards API V1");
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
