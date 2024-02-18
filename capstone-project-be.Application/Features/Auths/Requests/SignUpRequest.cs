using capstone_project_be.Application.DTOs.Auths;
using capstone_project_be.Application.Responses;
using MediatR;

namespace capstone_project_be.Application.Features.Auths.Requests
{
    //Kiểu request mà controller sẽ dùng để gửi đi cho handler xử lý
    public class SignUpRequest(UserSignUpDTO userSignUpData) : IRequest<object>
    {
        public UserSignUpDTO UserSignUpData { get; set; } = userSignUpData;
    }
}
