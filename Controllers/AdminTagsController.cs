using Bloggie.web.Data;
using Bloggie.web.Models.Domain;
using Bloggie.web.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Bloggie.web.Controllers
{
    public class AdminTagsController : Controller
    {
        private BloggieDbXontext _dbContext;
        public AdminTagsController(BloggieDbXontext dbContext)
        {
            _dbContext = dbContext;
        }
        //these two add methods doing the Create functionaliity
        [HttpGet]
        public IActionResult Add()
        {
            //this is the add page show
            return View();
        }

        [HttpPost]
        [ActionName("Add")]
        public IActionResult Add(AddTagRequest addTagRequest)
        {
            var tag = new Tag
            {
                Name = addTagRequest.Name,
                DisplayName = addTagRequest.DisplayName
            };
            _dbContext.Tags.Add(tag);
            _dbContext.SaveChanges();

            return RedirectToAction("List");
        }
        //here we will have the show functionallity.
        [HttpGet]
        [ActionName("List")]
        public IActionResult List()
        {
            //need to use dbContext to read 
            var items = _dbContext.Tags.ToList();
            return View(items);
        }


        [HttpGet]
        public IActionResult Update(Guid id)
         {
            //1st method 
            //var tag = _dbContext.Tags.Find(id);
            //
            //2nd method
            var item = _dbContext.Tags.FirstOrDefault(x => x.Id == id);
            if(item!=null)
            {
                var updateTagRequest = new UpdateTagRequest
                {
                    Id = id,
                    Name = item.Name,
                    DisplayName = item.DisplayName
                };
                return View(updateTagRequest);
            }
            return View(null);
        }

        [HttpPost]
        public IActionResult Update(UpdateTagRequest tag)
        {
            var item = new Tag
            {
                Id = tag.Id,
                Name = tag.Name,
                DisplayName = tag.DisplayName,
            };
            var exixting = _dbContext.Tags.Find(item.Id);
            if(exixting != null)
            {
                exixting.Name = item.Name;
                exixting.DisplayName = item.DisplayName;
                _dbContext.SaveChanges();
                // show success notif
                return RedirectToAction("List");
            }
            //show failure notif
            return RedirectToAction("Update",new {Id = tag.Id});
        }

        [HttpDelete]
        public IActionResult Remove(UpdateTagRequest tagRequest)
        {
            var tag1 = _dbContext.Tags.Find(tagRequest.Id);
            if(tag1 != null)
            {
                _dbContext.Remove(tag1);
                _dbContext.SaveChanges();
                return RedirectToAction("List");
            }
           

            
            return RedirectToAction("Update", new {id = tagRequest.Id});
        }


    }
}
