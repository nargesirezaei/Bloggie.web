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
        public async Task<Tag?> AddAsync(Tag tag)
        {
            await dbContext.Tags.AddAsync(tag);
            await dbContext.SaveChangesAsync();
            return tag;
        }

        public async Task<Tag> DeleteAsync(Guid id)
        {
            var exixtingTag = await dbContext.Tags.FindAsync(id);
            if (exixtingTag != null)
            {
                dbContext.Tags.Remove(exixtingTag);
                await dbContext.SaveChangesAsync();
                return exixtingTag;
            }
            return null;
        }

        public async Task<IEnumerable<Tag>> GetAllAsync()
        {
            return await dbContext.Tags.ToListAsync();
        }

        public Task<Tag?> GetAsync(Guid id)
        {
            return dbContext.Tags.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Tag?> UpdateAsync(Tag tag)
        {
            var existingTag = await dbContext.Tags.FindAsync(tag.Id);
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
