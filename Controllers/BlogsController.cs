using Bloggie.web.Models.Domain;
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
        private readonly IBlogPostCommentRepository blogPostCommentRepository;
        private readonly ITagRepository tagRepository;
        public BlogsController(IBlogPostRepository blogPostRepository,
                               IBlogPostLikeRepository blogLikeRepository, 
                               SignInManager<IdentityUser> signInManager,
                               UserManager<IdentityUser> userManager,
                               IBlogPostCommentRepository blogPostCommentRepository,
                               ITagRepository tagRepository)
        {
            this.blogPostRepository = blogPostRepository;
            this.blogLikeRepository = blogLikeRepository;
            this.signInManager = signInManager;
            this.userManager = userManager;
            this.blogPostCommentRepository = blogPostCommentRepository;
            this.tagRepository = tagRepository;
        }
        [HttpGet]
        public async Task<IActionResult> Index(string urlHandler, string? selectedCategory)
        {
            var liked = false;
            var blogPost = await blogPostRepository.GetByUrlHandler(urlHandler);

            var blogDetailsViewModel = new BlogDetailsViewModel();



            //if (!string.IsNullOrWhiteSpace(selectedCategory))
            //{
            //    blogPost = await blogPost.Where(p => p.Categories.Any(c => c.Name == selectedCategory))
            //    .ToList();
            //}

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
                        liked = likeForUser != null;
                        //if (likeForUser != null)
                        //{
                        //    liked = true; // ✅ bare hvis bruker faktisk har likt
                        //}
                    }
                }
                //get comment for blog post 
                var blogCommentsDomainModel = await blogPostCommentRepository.GetCommentByBlogId(blogPost.Id);
                var blogCommentsForView = new List<BlogComment>();

                foreach (var blogComment in blogCommentsDomainModel)
                {
                    blogCommentsForView.Add(new BlogComment
                    {
                        Description = blogComment.Description,
                        DateAdded = blogComment.DateAdded,
                        UserName = (await userManager.FindByIdAsync(blogComment.UserId.ToString())).UserName
                    });
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
                    Liked = liked,
                    Comments = blogCommentsForView
                };
            }
       
            return View(blogDetailsViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Index(BlogDetailsViewModel blogDetailsViewModel)
        {

            if(signInManager.IsSignedIn(User))
            {
                //mapping viewModel to domain 
                var commentDomainModel = new BlogPostComment
                {
                    BlogPostId = blogDetailsViewModel.Id,
                    Description = blogDetailsViewModel.CommentDescription,
                    UserId = Guid.Parse(userManager.GetUserId(User)),
                    DateAdded = DateTime.UtcNow

                };
                await blogPostCommentRepository.AddAsync(commentDomainModel);
                return RedirectToAction("Index", "Blogs",
                    new {urlHandle = blogDetailsViewModel.UrlHandle});
            }
            else
            {
                return View();
            }
            
        }
        


        //next fetch comments


    }
}
