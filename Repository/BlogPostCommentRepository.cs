using Bloggie.web.Data;
using Bloggie.web.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace Bloggie.web.Repository
{
    
    public class BlogPostCommentRepository : IBlogPostCommentRepository
    {

        private readonly BloggieDbContext bloggieDbContext;
        public BlogPostCommentRepository(BloggieDbContext bloggieDbContext)
        {
            this.bloggieDbContext = bloggieDbContext;
            
        }

        public async Task<BlogPostComment> AddAsync(BlogPostComment comment)
        {
            await bloggieDbContext.BlogPostComment.AddAsync(comment);
            await bloggieDbContext.SaveChangesAsync();
            return comment;
        }

        public async Task<IEnumerable<BlogPostComment>> GetCommentByBlogId(Guid blogPostId)
        {
            return await bloggieDbContext.BlogPostComment.Where(x => x.BlogPostId == blogPostId)
                .ToListAsync();
        }
    }
}
