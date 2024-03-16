
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SocialMedia.Core.Dtos
{
    public class PostDto
    {
      
        public int Id { get; set; }
        public int IdUser { get; set; } 
        public DateTime? Date { get; set; }
        public string? Description { get; set; }
        public string? Image { get; set; }
    }
}
