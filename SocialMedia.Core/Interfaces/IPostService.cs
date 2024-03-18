using SocialMedia.Core.CustomEntities;
using SocialMedia.Core.Entities;
using SocialMedia.Core.QueryFilters;

namespace SocialMedia.Core.Interfaces
{
    public interface IPostService
    {
        public PageList<Post> GetPosts(PostQueryFilter filter);
        public Task<Post> GetPost(int id);
        public Task<Post> InsertPost(Post post);
        public Task<bool> UpdatePost(Post post);
        public Task<bool> DeletePost(int id);
    }
}