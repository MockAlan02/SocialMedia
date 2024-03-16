using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SocialMedia.Core.Entities;
[Table("Comentario")]
public partial class Comments
{
    [Key]
    [Column("IdComentario")]
    public int IdComment { get; set; }
    [ForeignKey("IdPost")]
    [Column("IdPublicacion")]
    public int IdPost { get; set; }
    [ForeignKey("IdUser")]
    [Column("IdUsuario")]
    public int IdUser { get; set; }
    [Column("Descripcion")]
    public string? Description { get; set; }
    [Column("Fecha")]
    public DateTime? Date { get; set; }
    [Column("Activo")]
    public bool? IsActive { get; set; }

    public virtual Post? IdPostNavigation { get; set; }

    public virtual User? IdUserNavigation { get; set; }
}
