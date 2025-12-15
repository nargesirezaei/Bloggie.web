
using Bloggie.web.Data;
using Bloggie.web.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace Bloggie.web.Repository
{
    public class BlogPostLikeRepository : IBlogPostLikeRepository
    {
        private readonly BloggieDbContext bloggieDbCtx;
        public BlogPostLikeRepository(BloggieDbContext bloggieDbCtx)
        {
            this.bloggieDbCtx = bloggieDbCtx;
        }

        public async Task<BlogPostLike> AddLikeForBlog(BlogPostLike blogPostLike)
        {
            await bloggieDbCtx.BlogPostLike.AddAsync(blogPostLike);
            await bloggieDbCtx.SaveChangesAsync();
            return blogPostLike;
        }

        public async Task<IEnumerable<BlogPostLike>> GetLikesForBlog(Guid blogPostId)
        {
            return await bloggieDbCtx.BlogPostLike.Where(x=> x.BlogPostId == blogPostId).ToListAsync();
        }

        public async Task<int> GetTotalLikes(Guid blogPostId)
        {
            return await bloggieDbCtx.BlogPostLike
                .CountAsync(x => x.BlogPostId == blogPostId);
        }
    }
}
