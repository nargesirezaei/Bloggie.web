using Microsoft.AspNetCore.Identity;

namespace Bloggie.web.Repository
{
    public interface IUserRepository
    {
        Task<IEnumerable<IdentityUser>> GetAll();
    }
}
