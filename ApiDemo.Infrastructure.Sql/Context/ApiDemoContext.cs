using ApiDemo.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace ApiDemo.Infrastructure.Sql.Context
{
    public class ApiDemoContext(DbContextOptions<ApiDemoContext> options) : IdentityDbContext<ApplicationUser>(options)
    {
        public DbSet<Order> Orders { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            var typeBuilder = builder.Entity<Order>();

            typeBuilder.HasIndex(x => x.Id).IsUnique();
            typeBuilder.Property(x => x.Id).ValueGeneratedOnAdd();
            typeBuilder.Property(x => x.Date).IsRequired();
            typeBuilder.Property(x => x.IsConfirmed).IsRequired();

            base.OnModelCreating(builder);
        }
    }
}
