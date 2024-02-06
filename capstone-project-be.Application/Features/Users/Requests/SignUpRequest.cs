﻿using capstone_project_be.Application.DTOs;
using MediatR;

namespace capstone_project_be.Application.Features.Users.Requests
{
    //Kiểu request mà controller sẽ dùng để gửi đi cho handler xử lý
    public class SignUpRequest(UserSignUpDTO userSignUpData) : IRequest<string>
    {
        public UserSignUpDTO UserSignUpData { get; set; } = userSignUpData;
    }
}
