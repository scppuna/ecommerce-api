using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using UsuariosAPI.Models;

namespace UsuariosAPI.Data
{
    public class UserDbContext : IdentityDbContext<CustomIdentityUser, IdentityRole<int>, int>
    {
        private IConfiguration _configuration;

        public UserDbContext(DbContextOptions<UserDbContext> opt, IConfiguration configuration) : base(opt)
        {
            _configuration = configuration;
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            CustomIdentityUser admin = new CustomIdentityUser
            {
                UserName = "admin",
                Nome = "nome",
                NormalizedUserName = "ADMIN",
                Email = "admin@admin.com",
                NormalizedEmail = "ADMIN@ADMIN.COM",
                EmailConfirmed = true,
                CPF = "00000000000",
                Cep = "000000000",
                SecurityStamp = Guid.NewGuid().ToString(),
                Id = 9999,
                DataCriacao = DateTime.Now,
                DataNascimento = DateTime.Now,
                Status = true,
                Logradouro = "logradouro",
                Bairro = "bairro",
                Localidade = "localidade",
                Numero = 000,
                Complemento = "complemento",
                UF = "uf"

            };

            PasswordHasher<CustomIdentityUser> hasher = new PasswordHasher<CustomIdentityUser>();

            admin.PasswordHash = hasher.HashPassword(admin, _configuration
                .GetValue<string>("admininfo:password"));

            builder.Entity<CustomIdentityUser>().HasData(admin);

            builder.Entity<IdentityRole<int>>().HasData(new IdentityRole<int>
            {
                Id = 99999,
                Name = "admin",
                NormalizedName = "ADMIN"
            });

            builder.Entity<IdentityRole<int>>().HasData(new IdentityRole<int>
            {
                Id = 99997,
                Name = "regular",
                NormalizedName = "REGULAR"
            });

            builder.Entity<IdentityUserRole<int>>().HasData(new IdentityUserRole<int>
            {
                RoleId = 99999,
                UserId = 9999
            });

        }
    }
}
