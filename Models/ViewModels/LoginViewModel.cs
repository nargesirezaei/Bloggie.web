using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Bloggie.web.Models.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        public string Username { get; set; }

        [Required]
        [MinLength(6)]
        public string Password { get; set; }

        public string? ReturnUrl { get; set; }
    }
}
