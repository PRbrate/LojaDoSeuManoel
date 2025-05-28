using LojaDoSeuManoel.Api.Entities;
using LojaDoSeuManoel.Api.Repositories;
using LojaDoSeuManoel.Api.Repositories.Context;
using LojaDoSeuManoel.Api.Services;
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



            services.AddScoped<LojaDoSeuManoelContext>();
            services.AddScoped<IUserRepository, UserRepository>();

        }
    }
}
