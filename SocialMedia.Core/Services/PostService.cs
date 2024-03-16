﻿using SocialMedia.Core.Entities;
using SocialMedia.Core.Interfaces;

namespace SocialMedia.Core.Services
{
    public class PostService : IPostService
    {
        private readonly IUnitOfWork _unitOfWork;
       

        public PostService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Post>> GetPosts()
        {
            return await _unitOfWork.PostRepository.GetAll();
        }

        public async Task<Post> GetPost(int id)
        {
            return await _unitOfWork.PostRepository.GetById(id);
        }

        public async Task<Post> InsertPost(Post post)
        {
            var user = await _unitOfWork.UserRepository.GetById(post.IdUser);
            if(user == null)
            {
                throw new Exception("User doesn´t Exist");
            }

            if (post.Description!.ToLower().Contains("sexo"))
            {
                throw new Exception("Content not Allowed");
            }
             
            await _unitOfWork.PostRepository.Add(post);
            return post;
        }

        public async Task<bool> UpdatePost(Post post)
        {
            await _unitOfWork.PostRepository.Update(post);
            return true;
        }

        public async Task<bool> DeletePost(int id)
        {
           await _unitOfWork.PostRepository.Delete(id);
            return true;
        }

       
    }
}