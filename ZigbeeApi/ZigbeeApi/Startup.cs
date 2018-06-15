using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using Infrastructure.Services;
using Swashbuckle.AspNetCore.Swagger;

namespace ZigbeeApi
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

            services.AddMvc();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "My API", Version = "v1" });
            });


            services.AddEntityFrameworkSqlServer()
                             .AddDbContext<ApplicationContext>();

            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddTransient<IUpdateService, UpdateService>();
            services.AddTransient<IHumiditySensorsService, HumiditySensorsService>();
            services.AddTransient<ITemperatureSensorsService, TemperatureSensorsService>();
            services.AddTransient<IRoomsService, RoomsService>();
            services.AddTransient<ITemperatureService, TemperatureService>();
            services.AddTransient<IHumidityService, HumidityService>();
            services.AddTransient<IEmailService, EmailService>();
            services.AddTransient<ISettingsService, SettingsService>();

            services.AddTransient<IHumidityRepository, HumidityRepository>();
            services.AddTransient<ITemperaturesRepository, TemperaturesRepository>();
            services.AddTransient<ITemperatureSensorsRepository, TemperatureSensorsRepository>();
            services.AddTransient<IHumiditySensorsRepository, HumiditySensorsRepository>();
            services.AddTransient<IRoomsService, RoomsService>();
            services.AddTransient<ISettingsRepository, SettingsRepository>();    

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseSwagger();     

            app.UseMvc();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });


            JsonConvert.DefaultSettings = () => new JsonSerializerSettings
            {               
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };
        }
    }

  
}
