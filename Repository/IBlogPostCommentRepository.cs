using Bloggie.web.Models.Domain;

namespace Bloggie.web.Repository
{
    public interface IBlogPostCommentRepository
    {
        Task<BlogPostComment> AddAsync(BlogPostComment comment);
        Task<IEnumerable<BlogPostComment>> GetCommentByBlogId(Guid blogId);
    }
}
