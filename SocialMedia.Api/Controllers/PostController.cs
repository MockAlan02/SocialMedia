using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SocialMedia.Core.Dtos;
using SocialMedia.Core.Entities;
using SocialMedia.Core.Interfaces;
using SocialMedia.Infrastructure.Repositories;

namespace SocialMedia.Api.Controllers
{
    [ApiController]
    [Route("Api/[Controller]")]
  
    public class PostController : ControllerBase
    {
        private readonly IPostRepository _postRepository;
        private readonly IMapper _mapper;
        public PostController(IPostRepository post, IMapper mapper)
        {
            _postRepository = post;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> GetPosts()
        {
            var post = await _postRepository.GetPosts();
            var posDto = _mapper.Map<List<PostDto>>(post);
            return Ok(posDto);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPost(int id)
        {
            var post = await _postRepository.GetPost(id);
           
            return Ok(_mapper.Map<PostDto>(post));
        }
        [HttpPost]
        public async Task<IActionResult> InsertPost(PostDto post)
        {
            var poste = _mapper.Map<Post>(post);
            return Ok(await _postRepository?.Insert(poste)!);
        }
    }
}
