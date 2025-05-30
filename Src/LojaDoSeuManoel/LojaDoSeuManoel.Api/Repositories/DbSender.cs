using LojaDoSeuManoel.Api.Dtos;
using LojaDoSeuManoel.Api.Entities;
using LojaDoSeuManoel.Api.Entities.Enums;
using LojaDoSeuManoel.Api.Repositories.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace LojaDoSeuManoel.Api.Repositories
{
    public static class DbSender
    {
        public static async Task SeedAsync(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<LojaDoSeuManoelContext>();
            var useManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();

            await context.Database.MigrateAsync();

            var adminEmail = "seumanoel@admin.com";
            var adminUser = await useManager.FindByEmailAsync(adminEmail);
            if(adminUser == null)
            {
                adminUser = new User
                {
                    UserName = adminEmail.Split('@')[0],
                    Name = "Seu Manoel",
                    Email = adminEmail,
                    Address = "Endereço Default",
                    EmailConfirmed = true,
                    Role = UserRole.Admin

                };
                await useManager.CreateAsync(adminUser, "Admin123!");
            }

            if (!await context.Products.AnyAsync()) 
            {
                context.Products.AddRange(
                    new Product
                    {
                        Name = "Play 5",
                        Height = 10.4m,
                        Width = 39m,
                        Length = 26
                    },
                    new Product
                    {
                        Name = "X box series S",
                        Height = 27.5m,
                        Width = 15.1m,
                        Length = 6.5m
                    },
                    new Product
                    {
                        Name = "GTA VI",
                        Height = 1.9m,
                        Width = 1.35m,
                        Length = 14m
                    },
                    new Product
                    {
                        Name = "Expedition 33",
                        Height = 1.9m,
                        Width = 1.35m,
                        Length = 14m
                    },
                    new Product
                    {
                        Name = "Controle Xbox",
                        Height = 7.0m,
                        Width = 18m,
                        Length = 18m
                    },
                    new Product
                    {
                        Name = "Controle PS5",
                        Height = 7.0m,
                        Width = 12m,
                        Length = 12m
                    });

                await context.SaveChangesAsync();

            }
        }
    }
}
