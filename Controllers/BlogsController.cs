using Bloggie.web.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Bloggie.web.Controllers
{
    public class BlogsController : Controller
    {
        private readonly IBlogPostRepository blogPostRepository;
        public BlogsController(IBlogPostRepository blogPostRepository)
        {
            this.blogPostRepository = blogPostRepository;
        }
        [HttpGet]
        public async Task<IActionResult> Index(string urlHandler)
        {
            var blogPost = await blogPostRepository.GetByUrlHandler(urlHandler);
            return View(blogPost);
        }
    }
}
