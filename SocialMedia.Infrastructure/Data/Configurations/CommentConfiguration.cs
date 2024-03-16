using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SocialMedia.Core.Entities;

namespace SocialMedia.Infrastructure.Data.Configurations
{
    internal class CommentConfiguration : IEntityTypeConfiguration<Comments>
    {
        public void Configure(EntityTypeBuilder<Comments> builder)
        {
           builder.HasKey(e => e.Id);



           builder.Property(e => e.Id)
                .HasColumnName("IdComentario")
                .ValueGeneratedNever();

           builder.Property(e => e.Description)
                .HasColumnName("Descripcion")
                .HasMaxLength(500)
                .IsUnicode(false);

           builder.Property(e => e.Date)
                .HasColumnType("datetime")
                .HasColumnName("Fecha");

           builder.HasOne(d => d.IdPostNavigation).WithMany(p => p.Comments)
                .HasForeignKey(d => d.IdPost)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Comentario_Publicacion");

           builder.HasOne(d => d.IdUserNavigation)
                .WithMany(p => p.Comments)
                .HasForeignKey(d => d.IdUser)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Comentario_Usuario");
        }
    }
}
