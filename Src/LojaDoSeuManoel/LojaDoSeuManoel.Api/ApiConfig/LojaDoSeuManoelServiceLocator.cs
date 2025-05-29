using LojaDoSeuManoel.Api.Entities;
using LojaDoSeuManoel.Api.Repositories;
using LojaDoSeuManoel.Api.Repositories.Context;
using LojaDoSeuManoel.Api.Repositories.Interfaces;
using LojaDoSeuManoel.Api.Services;
using LojaDoSeuManoel.Api.Services.Interfaces;
using LojaDoSeuManoel.Core;

namespace LojaDoSeuManoel.Api.ApiConfig
{
    public static class LojaDoSeuManoelServiceLocator
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<INotifier, Notifier>();
            services.AddScoped<IUser, AspNetUser>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IJwtService, JwtService>();
            services.AddSingleton<BoxService>();
            services.AddScoped<IProductService, ProductService>();
            




            services.AddScoped<LojaDoSeuManoelContext>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();

        }
    }
}
