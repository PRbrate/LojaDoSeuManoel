using System.Reflection.Emit;
using LojaDoSeuManoel.Api.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LojaDoSeuManoel.Api.Repositories.Context
{
    public class LojaDoSeuManoelContext : IdentityDbContext<User>
    {
        public LojaDoSeuManoelContext(DbContextOptions<LojaDoSeuManoelContext> options) : base(options) { }


        public DbSet<User> User { get; set; }
        public DbSet<Requested> Requested { get; set; }
        public DbSet<Product> Products { get; set; }

        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            configurationBuilder
                .Properties<string>()
                .AreUnicode(false)
                .HaveMaxLength(100);

            configurationBuilder
                .Properties<decimal>()
                .HavePrecision(18, 2);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.HasDefaultSchema("dbo");

            builder.ApplyConfigurationsFromAssembly(typeof(LojaDoSeuManoelContext).Assembly);

            builder.Entity<IdentityUserLogin<string>>()
                .HasKey(login => login.UserId);

            builder.Entity<User>()
                .HasMany(u => u.Requesteds)
                .WithOne(s => s.User)
                .HasForeignKey(u => u.UserId);

            builder.Entity<Requested>()
                .HasMany(p => p.Products)
                .WithOne(p => p.Requested)
                .HasForeignKey(p => p.RequestedId)
                .IsRequired(false);

            builder.Entity<Requested>()
                .Property(p => p.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .ValueGeneratedOnAdd();

            builder.Entity<Product>()
                .Property(p => p.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .ValueGeneratedOnAdd();


        }

    }
}
