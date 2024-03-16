using SocialMedia.Core.Entities;
using SocialMedia.Core.Interfaces;

namespace SocialMedia.Core.Services
{
    public class PostService : IPostService
    {
        private readonly IPostRepository _postRepository;
        public PostService(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }

        public async Task<IEnumerable<Post>> GetPosts()
        {
            return await _postRepository.GetPosts();
        }

        public async Task<Post> GetPost(int id)
        {
            return await _postRepository.GetPost(id);
        }

        public async Task<Post> InsertPost(Post post)
        {
        
            return await _postRepository.Insert(post);
        }

        public async Task<bool> UpdatePost(Post post)
        {
            return await _postRepository.Update(post);
        }

        public async Task<bool> DeletePost(int id)
        {
           return await _postRepository.Delete(id);
        }

       
    }
}
