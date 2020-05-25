using System.ComponentModel.DataAnnotations;

namespace TodoApi.Shared.Models
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Your firstname is required")]
        [StringLength(50, ErrorMessage = "Must be between 2 and 50 characters", MinimumLength = 2)]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Your lastname is required")]
        [StringLength(50, ErrorMessage = "Must be between 2 and 50 characters", MinimumLength = 2)]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Email address is required")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [StringLength(255, ErrorMessage = "Must be between 5 and 255 characters", MinimumLength = 5)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Password confirmation is required")]
        [StringLength(255, ErrorMessage = "Must be between 5 and 255 characters", MinimumLength = 5)]
        [DataType(DataType.Password)]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }

    }
}  