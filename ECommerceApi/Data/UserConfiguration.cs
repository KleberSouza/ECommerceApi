using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ECommerceApi.Models;
using BC = BCrypt.Net.BCrypt;

namespace ECommerceApi.Data
{
    public class UserConfiguration : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.HasIndex(x => x.Email).IsUnique();
            builder.HasData(
                new Usuario
                {
                    Id = 1,
                    Nome = "Administrator",
                    Email = "admin@email.com.br",
                    Perfil = UsuarioPerfil.Administrador,
                    Senha = BC.HashPassword("pucminas")
                },
                new Usuario
                {
                    Id = 2,
                    Nome = "Cliente",
                    Email = "cliente@email.com.br",
                    Perfil = UsuarioPerfil.Cliente,
                    Senha = BC.HashPassword("pucminas")
                }
            );
        }
    }
}
