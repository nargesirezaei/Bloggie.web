using System.Diagnostics;
using Bloggie.web.Models;
using Bloggie.web.Models.ViewModels;
using Bloggie.web.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Bloggie.web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IBlogPostRepository blogRepository;
        private readonly ILogger<HomeController> _logger;
        private readonly ITagRepository tagRepository;


        public HomeController(ILogger<HomeController> logger, IBlogPostRepository blogRepository, ITagRepository tagRepository)
        {
            _logger = logger;
            this.blogRepository = blogRepository;
            this.tagRepository = tagRepository;

        }

        public async  Task<IActionResult> Index()
        {
            //getting all post
            var blogPosts = await blogRepository.GetAllAsync();
            //getting all Categories
            var categories = await tagRepository.GetAllAsync();
            var model = new HomeViewModel
            {
                BlogPosts = blogPosts,
                categories = categories
            };

            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
