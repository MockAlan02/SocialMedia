using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SocialMedia.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Infrastructure.Data.Configurations
{
    internal class PostConfiguration : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {
           builder.HasKey(e => e.IdPost).HasName("PK__Publicac__24F1B7D3D8DC9AF8");
           builder.Property(e => e.Description)
                .HasMaxLength(1000)
                .IsUnicode(false);
           builder.Property(e => e.Date).HasColumnType("datetime");
           builder.Property(e => e.Image)
                .HasMaxLength(500)
                .IsUnicode(false);

           builder.HasOne(d => d.IdUserNavigation).WithMany(p => p.Posts)
                .HasForeignKey(d => d.IdUser)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Fk_UsuarioPublicacion");
        }
    }
}
