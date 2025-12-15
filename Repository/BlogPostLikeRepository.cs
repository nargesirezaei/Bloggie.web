
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

            // Sjekk om brukeren allerede har likt dette innlegget
            var existingLike = await bloggieDbCtx.BlogPostLike
                .FirstOrDefaultAsync(x =>
                    x.BlogPostId == blogPostLike.BlogPostId &&
                    x.UserId == blogPostLike.UserId);


            if (existingLike != null)
            {
                // Returner null for å indikere at brukeren allerede har likt
                return null;
            }

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
