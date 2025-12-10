using Bloggie.web.Data;
using Bloggie.web.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace Bloggie.web.Repository
{
    public class BlogPostRepository : IBlogPostRepository
    {

        private readonly BloggieDbContext dbContext;

        public BlogPostRepository(BloggieDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<BlogPost> AddAsync(BlogPost post)
        {
            await dbContext.AddAsync(post);
            await dbContext.SaveChangesAsync();

            return post;
        }

        public async Task<BlogPost?> DeleteAsync(Guid id)
        {
            var blogpost = await dbContext.BloggPosts.FindAsync(id);
            if (blogpost != null)
            {
                 dbContext.BloggPosts.Remove(blogpost);
                await dbContext.SaveChangesAsync();
                return blogpost;
            }
            return null;
        }

        public async Task<IEnumerable<BlogPost>> GetAllAsync()
        {
            return await dbContext.BloggPosts.Include(x => x.Categories).ToListAsync();
        }

        public async Task<BlogPost?> GetAsync(Guid id)
        {
            return await dbContext.BloggPosts.Include(x=>x.Categories).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<BlogPost?> GetByUrlHandler(string urlHandler)
        {
            return await dbContext.BloggPosts.Include(x =>x.Categories).FirstOrDefaultAsync(x=> x.UrlHandle == urlHandler);
           
        }

        public async Task<BlogPost?> UpdateAsync(BlogPost post)
        {
           var exixtingBlog =  await dbContext.BloggPosts.Include(x => x.Categories).FirstOrDefaultAsync(x => x.Id == post.Id);
            if (exixtingBlog != null)
            {
                exixtingBlog.Id = post.Id;
                exixtingBlog.Heading = post.Heading;
                exixtingBlog.PublishedDate = post.PublishedDate;
                exixtingBlog.ShortDescription = post.ShortDescription;
                exixtingBlog.PageTitle = post.PageTitle;
                exixtingBlog.FeaturedImageUrl = post.FeaturedImageUrl;
                exixtingBlog.UrlHandle = post.UrlHandle;
                exixtingBlog.Author = post.Author;
                exixtingBlog.Content = post.Content;
                exixtingBlog.Visible = post.Visible;    
                exixtingBlog.Categories = post.Categories;

                
                await dbContext.SaveChangesAsync();
                return exixtingBlog;

            }

            return null;
        }

    }
}
