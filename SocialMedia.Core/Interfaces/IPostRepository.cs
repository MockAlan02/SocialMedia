using SocialMedia.Core.Entities;

namespace SocialMedia.Core.Interfaces
{
    public interface IPostRepository : IRepository<Post>
    {
        public Task<IEnumerable<Post>> GetPostsByUser(int idUser);
       
    }
}

