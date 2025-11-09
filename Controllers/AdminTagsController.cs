using Microsoft.AspNetCore.Mvc;

namespace Bloggie.web.Controllers
{
    public class AdminTagsController : Controller
    {
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [ActionName("Add")]
        public IActionResult SubmittTag()
        {
            var name = Request.Form["name"];
            var displayName = Request.Form["displayName"];
            return View("Add");
        }

    }
}
