﻿using System.ComponentModel.DataAnnotations;

namespace CorpEstate.DAL.DTO
{
    public class UserLoginDTO
    {
        [Required]
        public string Name { get; set; } = string.Empty;
        [Required]
        public string Password { get; set; } = string.Empty;
    }
}
