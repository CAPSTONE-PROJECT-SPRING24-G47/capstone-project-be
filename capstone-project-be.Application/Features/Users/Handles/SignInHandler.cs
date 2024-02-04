using AutoMapper;
using capstone_project_be.Application.Features.Users.Requests;
using capstone_project_be.Application.Interfaces;
using MediatR;

namespace capstone_project_be.Application.Features.Users.Handles
{
    public class SignInHandler : IRequestHandler<SignInRequest>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public SignInHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task Handle(SignInRequest request, CancellationToken cancellationToken)
        {
            var data = request.UserSignInData;
            bool userExists = await _unitOfWork.UserRepository.UserExists(data.Email, data.Password);
            if (userExists)
                Console.WriteLine("Đăng nhập thành công");
            else
                Console.WriteLine("Tài khoản hoặc mật khẩu không đúng!");
        }
    }
}
