using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SocialMedia.Core.Entities;

namespace SocialMedia.Infrastructure.Data.Configurations
{
    internal class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(e => e.IdUser);

            builder.ToTable("Usuario");

            builder.Property(e => e.LastName)
                .HasMaxLength(50)
                .IsUnicode(false);
            builder.Property(e => e.Email)
                .HasMaxLength(30)
                .IsUnicode(false);
            builder.Property(e => e.FirstName)
                .HasMaxLength(50)
                .IsUnicode(false);
            builder.Property(e => e.Phone)
                .HasMaxLength(10)
                .IsUnicode(false);
        }
    }
}
