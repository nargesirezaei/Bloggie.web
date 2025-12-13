using Bloggie.web.Data;
using Bloggie.web.Models.Domain;
using Bloggie.web.Models.ViewModels;
using Bloggie.web.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Bloggie.web.Controllers
{
    //with authorize, only logged inn user can use these functionality 
    [Authorize(Roles = "Admin")]
    public class AdminTagsController : Controller
    {
        private ITagRepository tagRepository;

        public AdminTagsController(ITagRepository tagRepository)
        {
            this.tagRepository = tagRepository;
        }
     
    
        [HttpGet]
        public IActionResult Add()
        {
            //this is the add page show
            return View();
        }

        [HttpPost]
        [ActionName("Add")]
        public async Task<IActionResult> Add(AddTagRequest addTagRequest)
        {
            //mapping 
            var tag = new Category
            {
                Name = addTagRequest.Name,
                DisplayName = addTagRequest.DisplayName
            };

           await tagRepository.AddAsync(tag);
           return RedirectToAction("List");
        }


        [HttpGet]
        [ActionName("List")]
        public async  Task<IActionResult> List()
        {
            var tags = await tagRepository.GetAllAsync(); 
            return View(tags);
        }

        [HttpGet]
        public async Task<IActionResult> Update(Guid id)
         {
            var tag = await tagRepository.GetAsync(id);

            if(tag!=null)
            {
                var updateTagRequest = new UpdateTagRequest
                {
                    Id = tag.Id,
                    Name = tag.Name,
                    DisplayName = tag.DisplayName
                };
                return View(updateTagRequest);
            }
            return View(null);
        }
    
        [HttpPost]
        public async Task<IActionResult> Update(UpdateTagRequest tag)
        {
            var item = new Category
            {
                Id = tag.Id,
                Name = tag.Name,
                DisplayName = tag.DisplayName,
            };
            var updatedTag = await tagRepository.UpdateAsync(item);
            if(updatedTag != null)
            {
                return RedirectToAction("List");
            }
            else
            {
                //show error.
            }

                return RedirectToAction("Update", new { Id = tag.Id });
        }

        [HttpPost]
        public async Task<IActionResult> Delete(UpdateTagRequest tagRequest)
        {
            var deletedTag =  await tagRepository.DeleteAsync(tagRequest.Id);
            if(deletedTag != null)
            {
                //success
                return RedirectToAction("List");
            }
            return RedirectToAction("Update", new {id = tagRequest.Id});
        }
    }
}
