
using Bloggie.web.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Bloggie.web.Controllers
{
    //Htttp resoponese , sucsess 200, bad 400,something went wrong 500
    /// <summary>
    /// GET => https://localhost:****/api/images
    /// POST => https://localhost:****/api/images
    /// **** => port number
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly IImageRepository imageRepository;
        public ImagesController(IImageRepository imageRepository)
        {
            this.imageRepository = imageRepository;
        }
        // we will call the third party API service(Cloudinary)fra repository. 
        //that service will upload our image to the cloud and we will get a URL in return.
        [HttpPost]
        public async Task<IActionResult> UploadAsync(IFormFile file)
        {
            var imageUrl = await imageRepository.UploadAsync(file);
            if (imageUrl == null)
            {
                return Problem("something went wrong" , null , (int)HttpStatusCode.InternalServerError);
            }
            //JsonResult er en http response.
            //“Lag et lite midlertidig objekt som har én egenskap — link — med verdien fra imageUrl.”
            return new JsonResult(new { link = imageUrl });
        }

    }
}
