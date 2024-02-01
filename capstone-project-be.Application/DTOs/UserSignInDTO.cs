﻿namespace capstone_project_be.Application.DTOs
{
    public class UserSignInDTO
    {
        public required string Email { get; set; }
        public required string Password { get; set; }
        public string? GoogleToken { get; set; }
    }
}
