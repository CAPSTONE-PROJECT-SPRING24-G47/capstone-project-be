﻿namespace capstone_project_be.Application.DTOs.Auths
{
    public class ResetPasswordDTO
    {
        public required string Email { get; set; }
        public required string Password { get; set; }
    }
}
