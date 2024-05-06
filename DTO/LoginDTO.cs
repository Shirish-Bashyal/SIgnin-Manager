﻿using System.ComponentModel.DataAnnotations;

namespace SIgnin_Manager.DTO
{
    public class LoginDTO
    {
        [Required]
        [StringLength(50)]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 5)]
        public string Password { get; set; }
    }
}
