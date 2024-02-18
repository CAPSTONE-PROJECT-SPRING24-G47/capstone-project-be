using capstone_project_be.Application.DTOs.Auths;
using MediatR;

namespace capstone_project_be.Application.Features.Auths.Requests
{
    public class SignInRequest(UserSignInDTO userSignInData) : IRequest<object>
    {
        public UserSignInDTO UserSignInData { get; set; } = userSignInData;

    }
}
