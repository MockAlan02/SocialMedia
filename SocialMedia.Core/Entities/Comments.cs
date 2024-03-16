using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SocialMedia.Core.Entities;
[Table("Comentario")]
public partial class Comments : BaseEntity
{
    [ForeignKey("IdPost")]
    public int IdPost { get; set; }
    [ForeignKey("IdUser")]
    [Column("IdUsuario")]
    public int IdUser { get; set; }
    public string? Description { get; set; }
    public DateTime? Date { get; set; }
    public bool? IsActive { get; set; }

    public virtual Post? IdPostNavigation { get; set; }

    public virtual User? IdUserNavigation { get; set; }
}
