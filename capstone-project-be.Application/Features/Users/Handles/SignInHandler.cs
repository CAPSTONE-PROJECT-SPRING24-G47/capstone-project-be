using AutoMapper;
using capstone_project_be.Application.Features.Users.Requests;
using capstone_project_be.Application.Interfaces;
using MediatR;

namespace capstone_project_be.Application.Features.Users.Handles
{
    public class SignInHandler : IRequestHandler<SignInRequest, string>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public SignInHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<string> Handle(SignInRequest request, CancellationToken cancellationToken)
        {
            var data = request.UserSignInData;
            bool userExists = await _unitOfWork.UserRepository.UserExists(data.Email, data.Password);
            if (userExists)
                return "Đăng nhập thành công";
            else
                return "Tài khoản hoặc mật khẩu không đúng!";
        }
    }
}
