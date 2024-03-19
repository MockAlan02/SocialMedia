using Microsoft.Extensions.Options;
using SocialMedia.Core.CustomEntities;
using SocialMedia.Core.Entities;
using SocialMedia.Core.Exceptions;
using SocialMedia.Core.Interfaces;
using SocialMedia.Core.QueryFilters;
using SocialMedia.Infrastructure.Options;

namespace SocialMedia.Core.Services
{
    public class PostService : IPostService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly PaginationOptions _paginationOptions;

        public PostService(IUnitOfWork unitOfWork, IOptions<PaginationOptions> options)
        {
            _unitOfWork = unitOfWork;
            _paginationOptions = options.Value;
        }

        public PageList<Post> GetPosts(PostQueryFilter filters)
        {
            filters.PageNumber = filters.PageNumber == 0? _paginationOptions.DefaultPageNumber : filters.PageNumber;
            filters.PageSize = filters.PageSize == 0 ? _paginationOptions.DefaultPageSize : filters.PageSize;
            var post = _unitOfWork.PostRepository.GetAll();
            if (filters.IdUser != null)
            {
                post = post.Where(post => post.IdUser ==  filters.IdUser);
            }
            else if(filters.Description != null)
            {
                post = post.Where(post => post.Description!.Contains(filters.Description, StringComparison.CurrentCultureIgnoreCase));
            }
            else if(filters.Date != null)
            {
                post = post.Where(post => post.Date ==  filters.Date);
            }
            var pagedPost = PageList<Post>.Create(post,filters.PageNumber,filters.PageSize);
            return pagedPost;
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
                throw new BusinessExceptions("User doesn´t Exist");
            }
            var userPost = await _unitOfWork.PostRepository.GetPostsByUser(post.IdUser);
            if(userPost.Count() < 10)
            {
                var fecha =  userPost.Last().Date;
                if (fecha.HasValue)
                {
                TimeSpan time = (TimeSpan)(DateTime.Now  - fecha);
                if(time.TotalDays < 7)
                    {
                        throw new BusinessExceptions("You are not able to publish the post");
                    }
                }                
            }

            if (post.Description!.ToLower().Contains("sexo"))
            {
                throw new BusinessExceptions("Content not Allowed");
            }
             
             _unitOfWork?.PostRepository.Add(post);
            await _unitOfWork!.SaveChangesAsync();
            return post;
        }

        public async Task<bool> UpdatePost(Post post)
        {
             _unitOfWork.PostRepository.Update(post);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeletePost(int id)
        {
           await _unitOfWork.PostRepository.Delete(id);
            return true;
        }

       
    }
}
