namespace Bloggie.web.Repository
{
    public interface IImageRepository
    {
        //Task for at methoden er async, string return type som skal være en url, parametere vi skal laste opp en bildet media.
        Task<string> UploadAsync(IFormFile file);
    }
}
