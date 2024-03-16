using Microsoft.EntityFrameworkCore;
using SocialMedia.Core.Entities;
using SocialMedia.Core.Interfaces;
using SocialMedia.Infrastructure.Data;

namespace SocialMedia.Infrastructure.Repositories
{
    public class PostRepository(SocialMediaContext socialMediaContext, IUserRepository userRepository) : IPostRepository
    {
        private readonly SocialMediaContext _context = socialMediaContext;
        private readonly IUserRepository _userRepository = userRepository;

        public async Task<IEnumerable<Post>> GetPosts() => await _context.Posts.ToListAsync();

        public async Task<Post> GetPost(int id) => await _context.Posts.FirstOrDefaultAsync(x => x.IdPost == id)!;

        public async Task<Post> Insert(Post post)
        {
            var user = await _userRepository.GetUser(post.IdUser);
            //Confirm the user exist
            if (user == null)
                throw new Exception("User doesn´t Exist");
            //User can´t not make description about sex
            if (post.Description!.ToLower().Contains("sexo"))
                throw new Exception("Don´t refer anything about sex");

            //Insert data to the database
            _context.Posts.Add(post);
            await _context.SaveChangesAsync();
            
            return post;
        }
        public async Task<bool> Update(Post post)
        {
            //Get Post by Id
            var currentPost = await GetPost(post.IdPost);
            //Changes in each column
            currentPost.Description = post.Description;
            currentPost.Date = post.Date;
            currentPost.Image = post.Image;
            //Get Each Column was Updated
            int rows = await _context.SaveChangesAsync();
            //Confirm if Post was Update
            return rows > 0;
        }
        public async Task<bool> Delete(int id)
        {
            // Get Post by id
            var currentPost = await GetPost(id);

            // Check if post exists
            if (currentPost == null)
            {
                return false; // Post not found
            }

            // Remove associated comments
            var comment = _context.Comments.Where(comment => comment.IdPost == currentPost.IdPost);
            _context.Comments.RemoveRange(comment);

            // Remove Post from Database and Save Changes
            _context.Posts.Remove(currentPost);
            int rowsAffected = await _context.SaveChangesAsync();

            // Confirm Post was Deleted
            return rowsAffected > 0;
        }


    }
}
