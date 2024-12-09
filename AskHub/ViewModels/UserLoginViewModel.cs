﻿using System.ComponentModel.DataAnnotations;

namespace AskHub.ViewModels
{
    public class UserLoginViewModel
    {
        [Required]
        public string? Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string? Password { get; set; }

        public bool RememberMe { get; set; } = false;

    }
}
