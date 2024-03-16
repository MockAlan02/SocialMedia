using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SocialMedia.Core.Entities;
[Table("Usuario")]
public partial class User
{
    [Key]
    [Column("IdUsuario")]
    public int IdUser { get; set; }
    [Column("Nombres")]
    public string? FirstName { get; set; }
    [Column("Apellidos")]
    public string? LastName { get; set; }
    public string? Email { get; set; }
    [Column("FechaNacimiento")]
    public DateOnly? DateofBirth { get; set; }
    [Column("Telefono")]
    public string? Phone { get; set; }
    [Column("Activo")]
    public bool? IsActive { get; set; }

    public virtual ICollection<Comments>? Comments { get; set; }

    public virtual ICollection<Post>? Posts { get; set; }
}
