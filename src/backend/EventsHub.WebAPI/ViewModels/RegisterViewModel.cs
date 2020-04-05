using System.ComponentModel.DataAnnotations;

namespace EventsHub.WebAPI.ViewModels
{
    public class RegisterViewModel
    {
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Compare("Password")]
        [Required]
        public string ConfirmPassword { get; set; }
    }
}
