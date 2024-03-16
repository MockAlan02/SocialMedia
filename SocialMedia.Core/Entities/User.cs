using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SocialMedia.Core.Entities;
public partial class User : BaseEntity
{
    public User()
    {
        Comments = new HashSet<Comments>();
        Posts = new HashSet<Post>();
    }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Email { get; set; }
    public DateOnly? DateofBirth { get; set; }
    public string? Phone { get; set; }
    public bool? IsActive { get; set; }

    public virtual ICollection<Comments>? Comments { get; set; }

    public virtual ICollection<Post>? Posts { get; set; }
}
