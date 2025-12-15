using Bloggie.web.Models.Domain;
using Bloggie.web.Models.ViewModels;
using Bloggie.web.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Bloggie.web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogPostLikeController  : ControllerBase
    {
        private readonly IBlogPostLikeRepository blogPostLikeRepository;
        public BlogPostLikeController(IBlogPostLikeRepository blogPostLikeRepository) 
        {
            this.blogPostLikeRepository = blogPostLikeRepository;
        }

        [HttpPost]
        [Route("Add")]
        public async Task<IActionResult> AddLike([FromBody] AddLikeRequest addLikeRequest)
        {
            //domain model(BlogPostLike) for addLikeReguest viewModel
            var model = new BlogPostLike
            {
                BlogPostId = addLikeRequest.BlogPostId,

                UserId = addLikeRequest.UserID
            };
            await blogPostLikeRepository.AddLikeForBlog(model);
            return Ok();
        }

        [HttpGet]
        [Route("{BlogPostId:guid}/totalLikes")]
        public async Task<IActionResult> GetTotalLikesForBlog([FromRoute] Guid blogPostId)
        {
            var totalLikes =  await blogPostLikeRepository.GetTotalLikes(blogPostId);
            return Ok(totalLikes);
        }
    }
}
 