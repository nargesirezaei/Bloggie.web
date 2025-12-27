
using Bloggie.web.Models.ViewModels;
using Bloggie.web.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Bloggie.web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminUserController : Controller
    {
        private readonly IUserRepository userRepository;
        private readonly UserManager<IdentityUser> userManager;
        public AdminUserController(IUserRepository userRepository,
                                    UserManager<IdentityUser> userManager)
        {
            this.userRepository = userRepository;
            this.userManager = userManager;

        }

        public async Task<IActionResult> List()
        {
            var users = await 
                userRepository.GetAll();

            var usersViewModel = new UserViewModel();
            usersViewModel.Users = new List<User>();


            foreach (var user in users)
            {
                usersViewModel.Users.Add(new Models.ViewModels.User
                {
                    Id = Guid.Parse(user.Id),
                    Username = user.UserName,
                    Email = user.Email

                });
            }

            return View(usersViewModel);
        }
        [HttpPost]
        public async Task<IActionResult> List(UserViewModel request)
        {
            if (!ModelState.IsValid)
            {
                // Last inn eksisterende brukere på nytt, slik at viewet fungerer
                var users = await userRepository.GetAll();
                request.Users = users.Select(u => new Models.ViewModels.User
                {
                    Id = Guid.Parse(u.Id),
                    Username = u.UserName,
                    Email = u.Email
                }).ToList();

                return View(request);
            }

            var identityUser = new IdentityUser
            {
                UserName = request.Username,
                Email = request.Email
            };

            var identityResult = await userManager.CreateAsync(identityUser, request.Password);

            if (identityResult != null && identityResult.Succeeded)
            {
                var roles = new List<string> { "User" };

                if (request.AdminRoleCheckBox)
                {
                    roles.Add("Admin");
                }

                await userManager.AddToRolesAsync(identityUser, roles);

                return RedirectToAction("List");
            }

            // Hvis noe går galt under oppretting, vis feilmeldinger
            ModelState.AddModelError("", "Failed to create user.");

            var allUsers = await userRepository.GetAll();
            request.Users = allUsers.Select(u => new Models.ViewModels.User
            {
                Id = Guid.Parse(u.Id),
                Username = u.UserName,
                Email = u.Email
            }).ToList();

            return View(request);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
            var user = await userManager.FindByIdAsync(id.ToString());
            if(user != null)
            {
                var identityResult = await userManager.DeleteAsync(user);

                if(identityResult != null && identityResult.Succeeded)
                {
                    return RedirectToAction("List", "AdminUser");
                }
            }
            return View();
        }
    }
}
