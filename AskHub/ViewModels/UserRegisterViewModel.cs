﻿using System.ComponentModel.DataAnnotations;

namespace AskHub.ViewModels
{
    public class UserRegisterViewModel
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        [MaxLength(30)]
        public string? Email { get; set; }

        [Required]
        [MinLength(1)]
        [MaxLength(20)]
        public string? Name { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [MaxLength(30)]
        public string? Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [MaxLength(30)]
        [Compare("Password")]
        [Display(Name = "Confirm Password")]
        public string? ConfirmedPassword { get; set; }

        public bool RememberMe { get; set; } = false;
    }
}