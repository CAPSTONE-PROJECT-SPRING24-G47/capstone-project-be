using AutoMapper;
using capstone_project_be.Application.DTOs;
using capstone_project_be.Application.Features.Auths.Requests;
using capstone_project_be.Application.Interfaces;
using capstone_project_be.Application.Responses;
using MediatR;

namespace capstone_project_be.Application.Features.Auths.Handles
{
    public class ResetPasswordCodeHandler : IRequestHandler<ResetPasswordCodeRequest, object>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ResetPasswordCodeHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<object> Handle(ResetPasswordCodeRequest request, CancellationToken cancellationToken)
        {
            var data = request.ResetPasswordCodeData;
            var userList = await _unitOfWork.UserRepository.Find(user => user.Email == data.Email);
            var userToUpdate = userList.First();
            var codeList = await _unitOfWork.VerificationCodeRepository.Find(code => code.UserId == userToUpdate.UserId);
            var verificationCode = codeList.First();

            if (verificationCode.Code == data.VerificationCode)
            {
                if (verificationCode.VerificationCodeExpireTime <= DateTime.Now)
                {
                    await _unitOfWork.VerificationCodeRepository.Delete(verificationCode);
                    await _unitOfWork.Save();
                    return new CodeExpiredResponse<UserDTO>()
                    {
                        IsSuccess = false,
                        Message = "Mã xác minh đã hết hạn",
                        IsExpired = true
                    };
                }

                else return new BaseResponse<UserDTO>()
                {
                    IsSuccess = true,
                    Message = "Mã xác minh đúng !",
                };
            }
            else return new BaseResponse<UserDTO>()
            {
                IsSuccess = false,
                Message = "Mã xác minh không đúng !",
            };
        }
    }
}
