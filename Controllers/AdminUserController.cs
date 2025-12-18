
using Bloggie.web.Models.ViewModels;
using Bloggie.web.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Bloggie.web.Controllers
{
    [Authorize(Roles ="Admin")]
    public class AdminUserController : Controller
    {
        private readonly IUserRepository userRepository;
        public AdminUserController(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public async Task<IActionResult> List()
        {
            var users = await userRepository.GetAll();

            var usersViewModel = new UserViewModel();
            usersViewModel.Users = new List<User>();


            foreach(var user in users)
            {
                usersViewModel.Users.Add(new Models.ViewModels.User
                {
                    Id = Guid.Parse(user.Id),
                    Username =user.UserName,
                    Email = user.Email

                });
            }
            
            return View(usersViewModel);
        }
    }
}
