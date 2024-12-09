using System.ComponentModel.DataAnnotations;

namespace AskHub.ViewModels
{
    public class UserRegisterViewModel
    {
        [Required]
        public string? Email { get; set; }

        [Required]
        public string? Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string? Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("Password")]
        public string? ConfirmedPassword { get; set; }

        public bool RememberMe { get; set; } = false;
    }
}
