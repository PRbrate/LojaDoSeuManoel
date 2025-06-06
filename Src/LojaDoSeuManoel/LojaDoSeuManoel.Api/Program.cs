using System.Globalization;
using Asp.Versioning.ApiExplorer;
using LojaDoSeuManoel.Api.ApiConfig;
using LojaDoSeuManoel.Api.Entities;
using LojaDoSeuManoel.Api.Repositories;
using LojaDoSeuManoel.Api.Repositories.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();


var cultureInfo = new CultureInfo("en-US");
CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;

builder.Services.AddEndpointsApiExplorer();

builder.Services.Configure<JwtSettings>(
    builder.Configuration.GetSection("JwtSettings"));


builder.Services.AddDbContext<LojaDoSeuManoelContext>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("Connection")));

builder.Services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<LojaDoSeuManoelContext>()
                .AddDefaultTokenProviders();

builder.AddVersioningConfig()
    .AddSwaggerConfig()
    .AddIdentityConfig();

builder.Services.RegisterServices();



var app = builder.Build();

try
{
    using (var scope = app.Services.CreateScope()) { 
        var services = scope.ServiceProvider;
        var context = services.GetRequiredService<LojaDoSeuManoelContext>();
        context.Database.Migrate();
        await DbSender.SeedAsync(app.Services);
    }
}
catch (Exception ex)
{
    Console.WriteLine("Erro ao executar SeedAsync: " + ex.Message);
    throw;
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    var apiVersionDescriptionProvider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();

    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        foreach (var description in apiVersionDescriptionProvider.ApiVersionDescriptions)
        {
            options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json",
                description.GroupName.ToUpperInvariant());
        }
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
