using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.DependencyInjection;
using SocialMedia.Core.Entities;
using SocialMedia.Core.Enumerations;


namespace SocialMedia.Infrastructure.Data.Configurations
{
    internal class SecurityConfiguration : IEntityTypeConfiguration<Security>
    {
        public void Configure(EntityTypeBuilder<Security> builder)
        {
            builder.ToTable("Seguridad");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id)
                .HasColumnName("id");
            builder.Property(e => e.UserName)
               .HasColumnName("NombreUsuario")
               .HasMaxLength(50)
               .IsUnicode(false);
            builder.Property(e => e.User)
              .HasColumnName("Usuario")
              .HasMaxLength(50)
              .IsUnicode(false);
            builder.Property(e => e.Password)
             .HasColumnName("Contrasena")
             .HasMaxLength(200)
             .IsUnicode(false);
            builder.Property(e => e.Rol)
            .HasColumnName("Rol")
            .IsRequired()
            .HasMaxLength(40)
            .HasConversion(x => x.ToString()
            , x => (RoleType)Enum.Parse(typeof(RoleType),x));
        }
    }
}
