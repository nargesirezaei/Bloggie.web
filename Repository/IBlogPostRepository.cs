using Bloggie.web.Models.Domain;

namespace Bloggie.web.Repository
{
    public interface IBlogPostRepository
    {
        Task<BlogPost> AddAsync(BlogPost post);
        Task<IEnumerable<BlogPost>> GetAllAsync();
        //these there methods can return null GetAsync, if id is wrong, thats whay we need nullable
        Task<BlogPost?> GetAsync(Guid id);
        Task<BlogPost?> GetByUrlHandler(string urlHandler);
        Task<BlogPost?> UpdateAsync(BlogPost post);
        Task<BlogPost?> DeleteAsync(Guid id);

        Task<IEnumerable<BlogPost>> GetByCategoryAsync(Guid categoryId);





    }
}
