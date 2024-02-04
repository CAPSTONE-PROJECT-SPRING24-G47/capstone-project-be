using capstone_project_be.Application.DTOs;
using MediatR;

namespace capstone_project_be.Application.Features.Users.Requests
{
    public class SignInRequest(UserSignInDTO userSignInData) : IRequest
    {
        public UserSignInDTO UserSignInData { get; set; } = userSignInData;

    }
}
