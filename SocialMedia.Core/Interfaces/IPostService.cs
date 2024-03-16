using SocialMedia.Core.Entities;

namespace SocialMedia.Core.Interfaces
{
    public interface IPostService
    {
        public Task<IEnumerable<Post>> GetPosts();
        public Task<Post> GetPost(int id);
        public Task<Post> InsertPost(Post post);
        public Task<bool> UpdatePost(Post post);
        public Task<bool> DeletePost(int id);
    }
}