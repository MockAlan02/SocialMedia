﻿using SocialMedia.Core.Entities;


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Core.Interfaces
{
    public interface IPostRepository
    {
       public Task<IEnumerable<Post>> GetPosts();
       public Task<Post> GetPost(int id);
        public Task<Post> Insert(Post post);
    }
}

