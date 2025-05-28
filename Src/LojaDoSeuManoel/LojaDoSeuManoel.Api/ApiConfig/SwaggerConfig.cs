using Asp.Versioning.ApiExplorer;
using Microsoft.OpenApi.Models;

namespace LojaDoSeuManoel.Api.ApiConfig
{
    public static class SwaggerConfig
    {
        public static WebApplicationBuilder AddSwaggerConfig(this WebApplicationBuilder builder)
        {
            builder.Services.AddEndpointsApiExplorer();

            builder.Services.AddSwaggerGen(c =>
            {
                c.MapType<DateTime>(() => new OpenApiSchema
                {
                    Type = "string",
                    Format = "date",
                    Example = new Microsoft.OpenApi.Any.OpenApiString(DateTime.Now.ToString("yyyy-MM-dd"))
                });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "Insira o token JWT desta maneira: Bearer {seu token}",
                    Name = "Authorization",
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                },
                Array.Empty<string>()
            }
        });

                var provider = builder.Services.BuildServiceProvider()
                    .GetRequiredService<IApiVersionDescriptionProvider>();

                foreach (var description in provider.ApiVersionDescriptions)
                {
                    c.SwaggerDoc(description.GroupName, new OpenApiInfo
                    {
                        Title = $"BarberShop API - {description.GroupName.ToUpperInvariant()}",
                        Version = description.ApiVersion.ToString(),
                        Description = "API para gerenciamento de barbearias."
                    });
                }

                c.DocInclusionPredicate((docName, apiDesc) =>
                {
                    if (!apiDesc.GroupName.Equals(docName, StringComparison.OrdinalIgnoreCase))
                        return false;

                    return true;
                });
            });

            return builder;
        }

    }
}
