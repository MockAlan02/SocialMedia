﻿using SocialMedia.Core.Entities;

namespace SocialMedia.Core.Interfaces
{
    public interface IPostRepository
    {
        public Task<IEnumerable<Post>> GetPosts();
        public Task<Post> GetPost(int id);
        public Task<Post> Insert(Post post);
        public Task<bool> Update(Post post);
        public Task<bool> Delete(int id);
    }
}

