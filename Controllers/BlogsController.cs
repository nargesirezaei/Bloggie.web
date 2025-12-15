using Bloggie.web.Models.ViewModels;
using Bloggie.web.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Bloggie.web.Controllers
{
    public class BlogsController : Controller
    {
        private readonly IBlogPostRepository blogPostRepository;
        private readonly IBlogPostLikeRepository blogLikeRepository;
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly UserManager<IdentityUser> userManager;
        public BlogsController(IBlogPostRepository blogPostRepository,
                               IBlogPostLikeRepository blogLikeRepository, 
                               SignInManager<IdentityUser> signInManager,
                               UserManager<IdentityUser> userManager)
        {
            this.blogPostRepository = blogPostRepository;
            this.blogLikeRepository = blogLikeRepository;
            this.signInManager = signInManager;
            this.userManager = userManager;
        }
        [HttpGet]
        public async Task<IActionResult> Index(string urlHandler)
        {
            var liked = false;
            var blogPost = await blogPostRepository.GetByUrlHandler(urlHandler);
            var blogDetailsViewModel = new BlogDetailsViewModel();


            if (blogPost != null)
            {
                var totalLikes = await blogLikeRepository.GetTotalLikes(blogPost.Id);

                if (signInManager.IsSignedIn(User))
                {
                    var allLikesForBlog = await blogLikeRepository.GetLikesForBlog(blogPost.Id);
                    var userId = userManager.GetUserId(User);

                    if (userId != null)
                    {
                        var likeForUser = allLikesForBlog.FirstOrDefault(x => x.UserId == Guid.Parse(userId));
                        if (likeForUser != null)
                        {
                            liked = true; // ✅ bare hvis bruker faktisk har likt
                        }
                    }
                }

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
                    TotalLikes = totalLikes,
                    Liked = liked
                };
            }
       
            return View(blogDetailsViewModel);
        }
    }
}
