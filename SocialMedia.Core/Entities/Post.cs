using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net.Http.Headers;

namespace SocialMedia.Core.Entities;
[Table("Publicacion")]
public partial class Post : BaseEntity
{
    public Post()
    {
        Comments = new HashSet<Comments>();
    }

    public int IdUser { get; set; }
    public DateTime? Date { get; set; }
    public string? Description { get; set; }
    public string? Image { get; set; }
    public virtual ICollection<Comments>? Comments { get; set; }

    public virtual User? IdUserNavigation { get; set; }
}
