using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SocialMedia.Core.Entities;
[Table("Publicacion")]
public partial class Post
{
    [Key]
    [Column("IdPublicacion")]
    public int IdPost { get; set; }
    [ForeignKey("IdUser")]
    [Column("IdUsuario")]
    public int IdUser { get; set; }
    [Column("Fecha")]
    public DateTime? Date { get; set; }
    [Column("Descripcion")]
    public string? Description { get; set; }
    [Column("Imagen")]
    public string? Image { get; set; }
    public virtual ICollection<Comments>? Comments { get; set; }

    public virtual User? IdUserNavigation { get; set; }
}
