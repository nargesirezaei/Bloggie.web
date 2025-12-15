using Bloggie.web.Models.ViewModels;
using Bloggie.web.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Bloggie.web.Controllers
{
    public class BlogsController : Controller
    {
        private readonly IBlogPostRepository blogPostRepository;
        private readonly IBlogPostLikeRepository blogLikeRepository;
        public BlogsController(IBlogPostRepository blogPostRepository, IBlogPostLikeRepository blogLikeRepository)
        {
            this.blogPostRepository = blogPostRepository;
            this.blogLikeRepository = blogLikeRepository;
        }
        [HttpGet]
        public async Task<IActionResult> Index(string urlHandler)
        {
            var blogPost = await blogPostRepository.GetByUrlHandler(urlHandler);
            var blogDetailsViewModel = new BlogDetailsViewModel();


            if (blogPost != null)
            {
                var totalLikes = await blogLikeRepository.GetTotalLikes(blogPost.Id);

                //vi have to convert domain model , blogpost to viewmodel, blogDetailviewmodel.
                blogDetailsViewModel = new BlogDetailsViewModel
                {
                    Id = blogPost.Id,
                    Heading = blogPost.Heading,
                    PageTitle = blogPost.PageTitle,
                    Content = blogPost.Content,
                    ShortDescription = blogPost.ShortDescription,
                    FeaturedImageUrl = blogPost.FeaturedImageUrl,
                    UrlHandle = blogPost.UrlHandle,
                    PublishedDate = blogPost.PublishedDate,
                    Author = blogPost.Author,
                    Visible = blogPost.Visible,
                    Categories = blogPost.Categories,
                    TotalLikes = totalLikes
                };
            }
       
            return View(blogDetailsViewModel);
        }
    }
}
