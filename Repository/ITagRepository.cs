using Bloggie.web.Models.Domain;

namespace Bloggie.web.Repository
{
    public interface ITagRepository
    {
        Task<IEnumerable<Category>> GetAllAsync(string? searchQuery = null);
        Task<Category?> GetAsync(Guid id);
        Task<Category?> AddAsync(Category tag);
        Task<Category?> UpdateAsync(Category tag);
        Task<Category> DeleteAsync(Guid id);

    }
}
