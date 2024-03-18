using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SocialMedia.Api.Responses;
using SocialMedia.Core.CustomEntities;
using SocialMedia.Core.Dtos;
using SocialMedia.Core.Entities;
using SocialMedia.Core.Interfaces;
using SocialMedia.Core.QueryFilters;
using SocialMedia.Infrastructure.Interfaces;

namespace SocialMedia.Api.Controllers
{
    [ApiController]
    [Route("Api/[Controller]")]

    public class PostController : ControllerBase
    {
        private readonly IPostService _postService;
        private readonly IMapper _mapper;
        private readonly IUriService _uriService;
        public PostController(IPostService postService, IMapper mapper, IUriService uriService)
        {
            _postService = postService;
            _mapper = mapper;
            _uriService = uriService;
        }
        [HttpGet(Name = nameof(GetPosts))]
        public IActionResult GetPosts([FromQuery]PostQueryFilter filter)
        {

            var post = _postService.GetPosts(filter);
            var posDto = _mapper.Map<IEnumerable<PostDto>>(post);

            //Data return to the user
            var metadata = new Metadata
            {
                HasNextPage = post.NextPage,
                PageSize = post.PageSize,
                HasPreviousPage = post.HasPreviousPage,
                TotalCount = post.TotalCount,
                TotalPage = post.TotalPages,
                NextPageUrl = _uriService.GetPostPaginationUri(filter, Url.RouteUrl(nameof(GetPosts))!).ToString(),
                PreviousPageUrl = _uriService.GetPostPaginationUri(filter, Url.RouteUrl(nameof(GetPosts))!).ToString()
            };
            var response = new ApiResponse<IEnumerable<PostDto>>(posDto)
            {
                Meta = metadata
            };
            Response.Headers.Append("X-Pagination",JsonConvert.SerializeObject(metadata));
            return Ok(response);
        
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPost(int id)
        {
            var post = await _postService.GetPost(id);
            var postDto = _mapper.Map<PostDto>(post);
            var response = new ApiResponse<PostDto>(postDto);
            return Ok(response);
        }
        [HttpPost]
        public async Task<IActionResult> InsertPost(PostDto postDto)
        {
            var post = _mapper.Map<Post>(postDto);
            await _postService?.InsertPost(post)!;
            postDto = _mapper.Map<PostDto>(post);
            var response  = new ApiResponse<PostDto>(postDto);
            return Ok(response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutPost(int id, PostDto postDto)
        {
            var post = _mapper.Map<Post>(postDto);
            post.Id = id;
            var response = new ApiResponse<bool>(await _postService.UpdatePost(post));
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _postService.DeletePost(id);
            var response = new ApiResponse<bool>(result);
            return Ok(response);

        }
    }
}
