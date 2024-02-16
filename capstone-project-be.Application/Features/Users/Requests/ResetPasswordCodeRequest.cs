﻿using capstone_project_be.Application.DTOs;
using MediatR;

namespace capstone_project_be.Application.Features.Users.Requests
{
    public class ResetPasswordCodeRequest(ResetPasswordCodeDTO resetPasswordCodeData) : IRequest<object>
    {
        public ResetPasswordCodeDTO ResetPasswordCodeData { get; set; } = resetPasswordCodeData;
    }
}
