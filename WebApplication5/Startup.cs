

namespace WebApplication4
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using System;

        public class Startup
        {
            public Startup(IConfiguration configuration, IWebHostEnvironment env)
            {
                Configuration = configuration;

                var builder = new ConfigurationBuilder()
                    .SetBasePath(env.ContentRootPath)
                    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                    .AddJsonFile($"appsettings_{env.EnvironmentName}.json", optional: true)
                    .AddJsonFile("appsettings_personal.json", optional: true)
                    .AddEnvironmentVariables();

                Configuration = builder.Build();
            }

            public IConfiguration Configuration { get; }

            public void ConfigureServices(IServiceCollection services)
            {
                services.AddControllers();
                services.AddSingleton<CompanyService>();
            }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            var personalInfoSection = Configuration.GetSection("PersonalInfo");
            if (personalInfoSection == null || !personalInfoSection.Exists())
            {
                Console.WriteLine("PersonalInfo is missing or invalid in configuration.");
            }
            else
            {
                var personalInfo = personalInfoSection.Get<PersonalInfo>();
                if (personalInfo == null)
                {
                    Console.WriteLine("PersonalInfo is null.");
                }
                else
                {
                    Console.WriteLine($"PersonalInfo: {personalInfo.Name}, Age: {personalInfo.Age}, Occupation: {personalInfo.Occupation}");
                }
            }
        }

    }
}













