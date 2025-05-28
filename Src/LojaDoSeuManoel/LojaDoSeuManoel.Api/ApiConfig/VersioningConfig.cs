using Asp.Versioning;
using Asp.Versioning.Conventions;

namespace LojaDoSeuManoel.Api.ApiConfig
{
    public static class VersioningConfig
    {
        public static WebApplicationBuilder AddVersioningConfig(this WebApplicationBuilder builder)
        {
            builder.Services.AddApiVersioning(
                    options =>
                    {
                        options.DefaultApiVersion = new ApiVersion(1);
                        options.AssumeDefaultVersionWhenUnspecified = true;
                        options.ReportApiVersions = true;
                    })
                .AddMvc(options =>
                {
                    options.Conventions.Add(new VersionByNamespaceConvention());
                })
                .AddApiExplorer(options =>
                {
                    options.GroupNameFormat = "'v'VVV";
                    options.SubstituteApiVersionInUrl = true;
                    options.DefaultApiVersion = new ApiVersion(1);
                    options.AssumeDefaultVersionWhenUnspecified = true;
                });

            return builder;
        }
    }
}
