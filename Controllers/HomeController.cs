using Bloggie.web.Models;
using Bloggie.web.Models.Domain;
using Bloggie.web.Models.ViewModels;
using Bloggie.web.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

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

        public async  Task<IActionResult> Index(string? selectedCategory)
        {
            ViewBag.SelectedCategory = selectedCategory;


            var categories = await tagRepository.GetAllAsync();

            IEnumerable<BlogPost> blogPosts;

            if (!string.IsNullOrWhiteSpace(selectedCategory))
            {
                blogPosts = await blogRepository.GetByCategoryAsync(Guid.Parse(selectedCategory));
            }
            else
            {
                blogPosts = await blogRepository.GetAllAsync();
            }

            var model = new HomeViewModel
            {
                BlogPosts = blogPosts,
                Categories = categories
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
