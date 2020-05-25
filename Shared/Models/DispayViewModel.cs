using System.ComponentModel.DataAnnotations;

namespace TodoApi.Shared.Models
{
    public class DisplayViewModel
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
    }
}  