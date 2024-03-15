using Microsoft.EntityFrameworkCore;
using SocialMedia.Core.Entities;
using SocialMedia.Core.Interfaces;
using SocialMedia.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Infrastructure.Repositories
{
    public class PostMongoRepository(SocialMediaContext socialMediaContext) : IPostRepository
    {
        private readonly SocialMediaContext _context = socialMediaContext;

        public async Task<IEnumerable<Post>> GetPosts() => await _context.Posts.ToListAsync();

        public async Task<Post> GetPost(int id) => await _context.Posts.FirstOrDefaultAsync(x => x.IdPost == id)!;

        public async Task<Post> Insert(Post post)
        {
            _context.Posts.Add(post);
            await _context.SaveChangesAsync();
            return post;
        }
    }
}
