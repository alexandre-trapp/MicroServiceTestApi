using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WeatherDB.Models;
using WeatherDB.Services;

namespace WeatherDB
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
            services.Configure<WeatherDatabaseSettings>(
                Configuration.GetSection(nameof(WeatherDatabaseSettings)));

            services.AddSingleton<IWeatherDatabaseSettings>(sp =>
                sp.GetRequiredService<IOptions<WeatherDatabaseSettings>>().Value);

            services.AddSingleton<WeatherService>();

            services.AddControllers()
                .AddNewtonsoftJson(options => options.UseMemberCasing());

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Weather Api DB Test", Version = "v1" });
            });
        }

        ///<summary>
        ///This method gets called by the runtime.Use this method to configure the HTTP request pipeline.
        ///</summary>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            EnableMiddlewareGenerateToSwaggerAsJsonEndpoint(app);
            EnableMiddlewareToSwaggerUISpecifyingJsonEndpoint(app);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private static void EnableMiddlewareGenerateToSwaggerAsJsonEndpoint(IApplicationBuilder app)
        {
            app.UseSwagger();
        }

        private static void EnableMiddlewareToSwaggerUISpecifyingJsonEndpoint(IApplicationBuilder app)
        {
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });
        }
    }
}
