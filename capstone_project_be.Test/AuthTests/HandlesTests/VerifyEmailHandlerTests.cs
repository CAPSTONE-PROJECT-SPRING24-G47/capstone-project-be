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
    public class VerifyEmailHandlerTests
    {
        [Fact]
        public async void VerifyEmailHandler_Handle_ReturnCodeNotExistResponse()
        {
            //Arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();

            var signUpVerificationData = new SignUpVerificationDTO()
            {
                Email = "test@example.com",
                VerificationCode = "123",
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
            var userList = new List<User>() { user };
            var codeList = new List<VerificationCode>();

            mockUnitOfWork.Setup(u => u.UserRepository.Find(It.IsAny<Expression<Func<User, bool>>>()))
                .ReturnsAsync(userList);
            mockUnitOfWork.Setup(vc => vc.VerificationCodeRepository.Find(It.IsAny<Expression<Func<VerificationCode, bool>>>()))
                .ReturnsAsync(codeList);

            var handler = new VerifyEmailHandler(mockUnitOfWork.Object);
            var request = new VerifyEmailRequest(signUpVerificationData);

            //Act
            var response = await handler.Handle(request, CancellationToken.None) as BaseResponse<UserDTO>;

            //Assert
            Assert.NotNull(response);
            Assert.False(response.IsSuccess);
            Assert.Equal("Mã xác minh không tồn tại", response.Message);
        }

        [Fact]
        public async void VerifyEmailHandler_Handle_ReturnCodeExpiredResponse()
        {
            //Arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();

            var signUpVerificationData = new SignUpVerificationDTO()
            {
                Email = "test@example.com",
                VerificationCode = "123",
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
                VerificationCodeExpireTime = DateTime.Now.AddMinutes(-1),
            };
            var userList = new List<User>() { user };
            var codeList = new List<VerificationCode>() { verificationCode };

            mockUnitOfWork.Setup(u => u.UserRepository.Find(It.IsAny<Expression<Func<User, bool>>>()))
                .ReturnsAsync(userList);
            mockUnitOfWork.Setup(vc => vc.VerificationCodeRepository.Find(It.IsAny<Expression<Func<VerificationCode, bool>>>()))
                .ReturnsAsync(codeList);

            var handler = new VerifyEmailHandler(mockUnitOfWork.Object);
            var request = new VerifyEmailRequest(signUpVerificationData);

            //Act
            var response = await handler.Handle(request, CancellationToken.None) as CodeExpiredResponse<UserDTO>;

            //Assert
            Assert.NotNull(response);
            Assert.False(response.IsSuccess);
            Assert.Equal("Mã xác minh đã hết hạn", response.Message);
            Assert.True(response.IsExpired);
        }

        [Fact]
        public async void VerifyEmailHandler_Handle_ReturnSuccessResponse()
        {
            //Arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();

            var signUpVerificationData = new SignUpVerificationDTO()
            {
                Email = "test@example.com",
                VerificationCode = "123",
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

            var handler = new VerifyEmailHandler(mockUnitOfWork.Object);
            var request = new VerifyEmailRequest(signUpVerificationData);

            //Act
            var response = await handler.Handle(request, CancellationToken.None) as BaseResponse<UserDTO>;

            //Assert
            Assert.NotNull(response);
            Assert.True(response.IsSuccess);
            Assert.Equal("Tài khoản của bạn đã được xác minh thành công", response.Message);
        }

        [Fact]
        public async void VerifyEmailHandler_Handle_ReturnFailResponse()
        {
            //Arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();

            var signUpVerificationData = new SignUpVerificationDTO()
            {
                Email = "test@example.com",
                VerificationCode = "123",
            };

            var userList = new List<User>();

            mockUnitOfWork.Setup(u => u.UserRepository.Find(It.IsAny<Expression<Func<User, bool>>>()))
                .ReturnsAsync(userList);

            var handler = new VerifyEmailHandler(mockUnitOfWork.Object);
            var request = new VerifyEmailRequest(signUpVerificationData);

            //Act
            var response = await handler.Handle(request, CancellationToken.None) as BaseResponse<UserDTO>;

            //Assert
            Assert.NotNull(response);
            Assert.False(response.IsSuccess);
            Assert.Equal("Không tìm thấy mail", response.Message);
        }
    }
}
