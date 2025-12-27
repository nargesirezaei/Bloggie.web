using Bloggie.web.Data;
using Bloggie.web.Models.Domain;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace Bloggie.web.Repository
{
    public class TagRepository : ITagRepository
    {
        private readonly BloggieDbContext dbContext;
        public TagRepository(BloggieDbContext dbContext)
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

        public async Task<IEnumerable<Category>> GetAllAsync(string? searchQuery)
        {
            //ved bruk av AsQueryable har vi en liste som vi kan query på det!
            var query = dbContext.Categories.AsQueryable();

            //Filtering 
            if(string.IsNullOrWhiteSpace(searchQuery)==false)
            {
                query = query.Where(x => x.Name.Contains(searchQuery) || 
                                    x.DisplayName.Contains(searchQuery));
            }

            //sorting
            //pagination
            return await query.ToListAsync();
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
