using Bloggie.web.Models.Domain;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Bloggie.web.Models.ViewModels
{
    public class AddBlogPostRequest
    {
       
        public string Heading { get; set; }
        public string PageTitle { get; set; }
        public string Content { get; set; }
        public string ShortDescription { get; set; }
        public string FeaturedImageUrl { get; set; }
        public string UrlHandle { get; set; }
        public DateTime PublishedDate { get; set; }
        public string Author { get; set; }
        public bool Visible { get; set; }
        //SelectListItem er en innebygd klasse i ASP.NET Core MVC som brukes for <select>-elementer.

       

        public IEnumerable<SelectListItem> Categories { get; set; }

        //collect tag
        public string[] SelectedTags { get; set; } = Array.Empty<string>();
    }
    }

