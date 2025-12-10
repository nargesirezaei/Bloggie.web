using Bloggie.web.Data;
using Bloggie.web.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace Bloggie.web.Repository
{
    public class TagRepository : ITagRepository
    {
        private readonly BloggieDbXontext dbContext;
        public TagRepository(BloggieDbXontext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<Category?> AddAsync(Category tag)
        {
            await dbContext.Categories.AddAsync(tag);
            await dbContext.SaveChangesAsync();
            return tag;
        }

        public async Task<Category> DeleteAsync(Guid id)
        {
            var exixtingTag = await dbContext.Categories.FindAsync(id);
            if (exixtingTag != null)
            {
                dbContext.Categories.Remove(exixtingTag);
                await dbContext.SaveChangesAsync();
                return exixtingTag;
            }
            return null;
        }

        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            return await dbContext.Categories.ToListAsync();
        }

        public Task<Category?> GetAsync(Guid id)
        {
            return dbContext.Categories.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Category?> UpdateAsync(Category tag)
        {
            var existingTag = await dbContext.Categories.FindAsync(tag.Id);
            if (existingTag != null)
            {
                existingTag.Name = tag.Name;
                existingTag.DisplayName = tag.DisplayName;

                await dbContext.SaveChangesAsync();
                return existingTag;
            }
            return null;
        }
    }
}
