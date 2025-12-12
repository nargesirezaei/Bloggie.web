using Bloggie.web.Models.Domain;
using Bloggie.web.Models.ViewModels;
using Bloggie.web.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Bloggie.web.Controllers
{
    [Authorize(Roles ="Admin")]
    public class AdminBlogPostController : Controller
    {
        private readonly ITagRepository tagRepository;
        private readonly IBlogPostRepository blogPostRepository;
        public AdminBlogPostController(ITagRepository tagRepository, IBlogPostRepository blogPostRepository)
        {
            this.tagRepository = tagRepository;
            this.blogPostRepository = blogPostRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            //denne methoden lagrer Add-View
            //get tag from repository
            var tags = await tagRepository.GetAllAsync();
            var model = new AddBlogPostRequest
            {
                Categories = tags.Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() })
            };
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Add(AddBlogPostRequest addBlogPostRequest)
        {
            //we need to map view model to domain model befor sending it to dbcontext
            var blogPost = new BlogPost
            {
                Heading = addBlogPostRequest.Heading,
                PageTitle = addBlogPostRequest.PageTitle,
                Content = addBlogPostRequest.Content,
                ShortDescription = addBlogPostRequest.ShortDescription,
                FeaturedImageUrl = addBlogPostRequest.FeaturedImageUrl,
                UrlHandle = addBlogPostRequest.UrlHandle,
                PublishedDate = addBlogPostRequest.PublishedDate,
                Author = addBlogPostRequest.Author,
                Visible = addBlogPostRequest.Visible 
            };
            // we need to convert tags to domain and includ it to blogPost
            //listen er tøm
            var selectedTag = new List<Category>();
            foreach(var selectedTagId in addBlogPostRequest.SelectedTags)
            {
                var selectedTagIdAsGuid = Guid.Parse(selectedTagId);
                var existingTag = await tagRepository.GetAsync(selectedTagIdAsGuid);
                if (existingTag != null)
                    selectedTag.Add(existingTag);        
            }
            blogPost.Categories = selectedTag;

            await blogPostRepository.AddAsync(blogPost);
            return RedirectToAction("List");
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            var blogPosts = await blogPostRepository.GetAllAsync();
            return View(blogPosts);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var blogPost = await blogPostRepository.GetAsync(id);
            var tagsDomainModel = await tagRepository.GetAllAsync();
            if(blogPost != null)
            {
                //når man trykker på edit , så man har en blogpost-obj som går til editt side, men vi konverterer blogpost til Editblogpostrequest
                var model = new EditBlogPostRequest
                {
                    Id = blogPost.Id,
                    Heading = blogPost.Heading,
                    PageTitle = blogPost.PageTitle,
                    Content = blogPost.Content,
                    Author = blogPost.Author,
                    FeaturedImageUrl = blogPost.FeaturedImageUrl,
                    UrlHandle = blogPost.UrlHandle,
                    Visible = blogPost.Visible,
                    ShortDescription = blogPost.ShortDescription,
                    PublishedDate = blogPost.PublishedDate,
                    Tags = tagsDomainModel.Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() }),
                    SelectedTags = blogPost.Categories.Select(x => x.Id.ToString()).ToArray()
                };
                //denne returen er nå man er i edit side , men har data i input som man kan endre!
                return View(model);
            }
            
             return View(null);
        }
        
        
        [HttpPost]
        public async Task<IActionResult> Edit(EditBlogPostRequest postRequest)
        {
            //nå har man en editBlogPostRequest-obj som trenges å bli mappa til blogpost
            var tagDomainModel = await tagRepository.GetAllAsync();
            //mapping view model to domain
            var blogPost = new BlogPost
            {
                Id = postRequest.Id,
                Heading= postRequest.Heading,
                PageTitle= postRequest.PageTitle,
                Content = postRequest.Content,
                Author = postRequest.Author,
                FeaturedImageUrl= postRequest.FeaturedImageUrl,
                UrlHandle = postRequest.UrlHandle,
                ShortDescription = postRequest.ShortDescription,
                PublishedDate = postRequest.PublishedDate,
                Visible= postRequest.Visible

            };
            //map tag into domain model 
            var selectedTags = new List<Category>();
            foreach(var selectedTagId in postRequest.SelectedTags)
            {
                var selectedTagIdAsGuid = Guid.Parse(selectedTagId);
                var existingTag = await tagRepository.GetAsync(selectedTagIdAsGuid);
                if (existingTag != null)
                    selectedTags.Add(existingTag);

            }
            blogPost.Categories = selectedTags;
            var updatedBlog =  await blogPostRepository.UpdateAsync(blogPost);
            if (updatedBlog != null) { return RedirectToAction("List"); }
            return RedirectToAction("List");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(EditBlogPostRequest postRequest)
        {
           var deletedBlog = await blogPostRepository.DeleteAsync(postRequest.Id);
            if(deletedBlog != null)
            {
                return RedirectToAction("List");
            }
            return RedirectToAction("Edit", new {id= postRequest.Id});
            
        }

    }
}
