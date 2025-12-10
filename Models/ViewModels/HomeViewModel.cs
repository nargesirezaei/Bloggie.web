using Bloggie.web.Models.Domain;

namespace Bloggie.web.Models.ViewModels
{
    public class HomeViewModel
    {
        public IEnumerable<BlogPost> BlogPosts { get; set; }
        public IEnumerable<Category> categories { get; set; }

    }
}
