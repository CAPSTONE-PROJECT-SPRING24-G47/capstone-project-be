using AutoMapper;
using capstone_project_be.Application.DTOs.Auths;
using capstone_project_be.Application.DTOs.Users;
using capstone_project_be.Application.Features.Auths.Handles;
using capstone_project_be.Application.Features.Auths.Requests;
using capstone_project_be.Application.Interfaces;
using capstone_project_be.Application.Responses;
using capstone_project_be.Domain.Entities;
using Moq;
using System.Linq.Expressions;

namespace capstone_project_be.Test.AuthTests.HandlesTests
{
    public class VerifyResetPasswordHandlerTests
    {
        [Fact]
        public async void VerifyResetPasswordHandler_Handle_ReturnSuccessResponse()
        {
            //Arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var mockMapper = new Mock<IMapper>();
            var mockEmailSender = new Mock<IEmailSender>();

            var verifyResetPasswordData = new ResetPasswordVerificationDTO()
            {
                Email = "test@example.com",
            };
            var user = new User()
            {
                UserId = 1,
                RoleId = 4,
                LastName = "abc",
                FirstName = "abc",
                Email = "test@example.com",
                Password = BCrypt.Net.BCrypt.EnhancedHashPassword("password"),
                PictureProfile = "",
                IsGoogleAuth = true,
                IsVerified = true,
                IsBanned = false,
                CreatedAt = DateTime.Now
            };
            var verificationCode = new VerificationCode()
            {
                Id = 1,
                UserId = 1,
                Code = "123",
                VerificationCodeExpireTime = DateTime.Now.AddMinutes(5),
            };
            var userList = new List<User>() { user };
            var codeList = new List<VerificationCode>() { verificationCode };

            mockUnitOfWork.Setup(u => u.UserRepository.Find(It.IsAny<Expression<Func<User, bool>>>()))
                .ReturnsAsync(userList);
            mockUnitOfWork.Setup(vc => vc.VerificationCodeRepository.Find(It.IsAny<Expression<Func<VerificationCode, bool>>>()))
                .ReturnsAsync(codeList);

            var handler = new VerifyResetPasswordHandler(mockUnitOfWork.Object, mockEmailSender.Object);
            var request = new VerifyResetPasswordRequest(verifyResetPasswordData);

            //Act
            var response = await handler.Handle(request, CancellationToken.None) as BaseResponse<UserDTO>;

            //Assert
            Assert.NotNull(response);
            Assert.True(response.IsSuccess);
            Assert.Equal($"Mã xác minh đã được gửi lại vào mail {verifyResetPasswordData.Email}", response.Message);
        }

        [Fact]
        public async void VerifyResetPasswordHandler_Handle_ReturnFailResponse()
        {
            //Arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var mockMapper = new Mock<IMapper>();
            var mockEmailSender = new Mock<IEmailSender>();

            var verifyResetPasswordData = new ResetPasswordVerificationDTO()
            {
                Email = "test@example.com",
            };
            var userList = new List<User>();

            mockUnitOfWork.Setup(u => u.UserRepository.Find(It.IsAny<Expression<Func<User, bool>>>()))
                .ReturnsAsync(userList);

            var handler = new VerifyResetPasswordHandler(mockUnitOfWork.Object, mockEmailSender.Object);
            var request = new VerifyResetPasswordRequest(verifyResetPasswordData);

            //Act
            var response = await handler.Handle(request, CancellationToken.None) as BaseResponse<UserDTO>;

            //Assert
            Assert.NotNull(response);
            Assert.False(response.IsSuccess);
            Assert.Equal("Email không tồn tại!", response.Message);
        }
    }
}
