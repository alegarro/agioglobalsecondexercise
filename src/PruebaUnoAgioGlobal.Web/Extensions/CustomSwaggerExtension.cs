using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.PlatformAbstractions;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.IO;

namespace PruebaUnoAgioGlobal.Web.Extensions
{
    public static class CustomSwaggerExtension
    {
        #region Private Methods

        private static Info GetSwaggerInfo(IConfigurationSection section)
        {
            string title = section.GetSection("Title").Value;
            string description = section.GetSection("Description").Value;
            string termsOfService = section.GetSection("TermsOfService").Value;
            string servicePath = section.GetSection("ServicePath").Value;
            var info = new Info()
            {
                Title = title,
                Description = description,
                TermsOfService = termsOfService
            };
            return info;
        }

        private static string GetSwaggerEndPointPath(string swaggerVersion)
        {
            return Path.Combine($"/swagger/{swaggerVersion}/swagger.json");
        }

        #endregion

        #region Public Methods

        public static void AddCustomSwagger(this IServiceCollection services, IConfiguration configuration)
        {
            var section = configuration.GetSection("Swagger");
            var version = section.GetSection("Version") == null ? "v1" : string.IsNullOrEmpty(section.GetSection("Version").Value) ? "v1" : section.GetSection("Version").Value;
            var document = section.GetSection("Document").Value;
            var info = GetSwaggerInfo(section);
            services.AddSwaggerGen(options =>
            {
                var path = Path.Combine(PlatformServices.Default.Application.ApplicationBasePath, document);

                options.IncludeXmlComments(path);
                options.SwaggerDoc(version, info);
                options.DescribeAllEnumsAsStrings();
                options.CustomSchemaIds(type => type.FriendlyId(true));
            });
        }

        public static void UseCustomSwagger(this IApplicationBuilder builder, IConfiguration configuration)
        {
            var section = configuration.GetSection("Swagger");
            var swaggerVersion = section.GetSection("Version") == null ? "v1" : string.IsNullOrEmpty(section.GetSection("Version").Value) ? "v1" : section.GetSection("Version").Value;

            builder.UseSwagger();

            var swaggerEndPointPath = GetSwaggerEndPointPath(swaggerVersion);
            builder.UseSwaggerUI(options => options.SwaggerEndpoint(swaggerEndPointPath, "My Custom Swagger EndPoint"));
        }

        #endregion
    }
}
