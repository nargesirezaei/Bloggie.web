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
            //if(addLikeRequest != null)
            //{

            //}
            var model = new BlogPostLike
            {
                BlogPostId = addLikeRequest.BlogPostId,
                UserId = addLikeRequest.UserID
            };
            await blogPostLikeRepository.AddLikeForBlog(model);
            return Ok();
        }
    }
}
