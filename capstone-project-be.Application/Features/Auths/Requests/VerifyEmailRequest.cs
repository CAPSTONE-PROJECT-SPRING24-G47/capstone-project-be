﻿using capstone_project_be.Application.DTOs.Auths;
using MediatR;

namespace capstone_project_be.Application.Features.Auths.Requests
{
    public class VerifyEmailRequest(SignUpVerificationDTO signUpVerificationData) : IRequest<object>
    {
        public SignUpVerificationDTO SignUpVerificationData { get; set; } = signUpVerificationData;
    }
}
