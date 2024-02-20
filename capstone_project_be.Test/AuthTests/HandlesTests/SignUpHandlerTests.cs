using AutoMapper;
using capstone_project_be.Application.DTOs;
using capstone_project_be.Application.Features.Auths.Handles;
using capstone_project_be.Application.Features.Auths.Requests;
using capstone_project_be.Application.Interfaces;
using capstone_project_be.Application.Responses;
using capstone_project_be.Domain.Entities;
using Moq;
using System.Linq.Expressions;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace capstone_project_be.Test.AuthTests.HandlesTests
{
    public class SignUpHandlerTests
    {
        [Fact]
        public async void SignUpHandler_Handle_ReturnCodeResendResponse()
        {
            //Arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var mockMapper = new Mock<IMapper>();
            var mockEmailSender = new Mock<IEmailSender>();

            var userSignUpData = new UserSignUpDTO()
            {
                LastName = "abc",
                FirstName = "abc",
                Email = "test@example.com",
                Password = "password",
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
                IsVerified = false,
                Status = false,
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

            var handler = new SignUpHandler(mockUnitOfWork.Object, mockMapper.Object, mockEmailSender.Object);
            var request = new SignUpRequest(userSignUpData);

            //Act
            var response = await handler.Handle(request, CancellationToken.None) as BaseResponse<UserDTO>;

            //Assert
            Assert.NotNull(response);
            Assert.Equal($"Mã xác minh đã được gửi lại vào mail {userSignUpData.Email}", response.Message);
        }

        [Fact]
        public async void SignUpHandler_Handle_ReturnFailResponse()
        {
            //Arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var mockMapper = new Mock<IMapper>();
            var mockEmailSender = new Mock<IEmailSender>();

            var userSignUpData = new UserSignUpDTO()
            {
                LastName = "abc",
                FirstName = "abc",
                Email = "test@example.com",
                Password = "password",
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
                Status = false,
                CreatedAt = DateTime.Now
            };
            var userList = new List<User>() { user };

            mockUnitOfWork.Setup(u => u.UserRepository.Find(It.IsAny<Expression<Func<User, bool>>>()))
                .ReturnsAsync(userList);

            var handler = new SignUpHandler(mockUnitOfWork.Object, mockMapper.Object, mockEmailSender.Object);
            var request = new SignUpRequest(userSignUpData);

            //Act
            var response = await handler.Handle(request, CancellationToken.None) as BaseResponse<UserDTO>;

            //Assert
            Assert.NotNull(response);
            Assert.False(response.IsSuccess);
            Assert.Equal("Email này đã được sử dụng ở một tài khoản khác", response.Message);
        }

        [Fact]
        public async void SignUpHandler_Handle_ReturnSuccessResponse()
        {
            //Arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var mockMapper = new Mock<IMapper>();
            var mockEmailSender = new Mock<IEmailSender>();

            var userSignUpData = new UserSignUpDTO()
            {
                LastName = "abc",
                FirstName = "abc",
                Email = "test@example.com",
                Password = "password",
            };
            var userList = new List<User>();

            mockUnitOfWork.Setup(u => u.UserRepository.Find(It.IsAny<Expression<Func<User, bool>>>()))
                .ReturnsAsync(userList);
            mockMapper.Setup(x => x.Map<User>(It.IsAny<UserSignUpDTO>()))
                .Returns((User source) => new User()
                {
                    UserId = 1,
                    RoleId = 4,
                    LastName = userSignUpData.LastName,
                    FirstName = userSignUpData.FirstName,
                    Email = userSignUpData.Email,
                    Password = BCrypt.Net.BCrypt.EnhancedHashPassword(userSignUpData.Password),
                    PictureProfile = null,
                    IsGoogleAuth = false,
                    IsVerified = false,
                    Status = true,
                    CreatedAt = DateTime.Now
                });

            var handler = new SignUpHandler(mockUnitOfWork.Object, mockMapper.Object, mockEmailSender.Object);
            var request = new SignUpRequest(userSignUpData);

            //Act
            var response = await handler.Handle(request, CancellationToken.None) as BaseResponse<UserDTO>;

            //Assert
            Assert.NotNull(response);
            Assert.False(response.IsSuccess);
            Assert.Equal($"Đăng ký thành công, mã xác minh đã được gửi vào {userSignUpData.Email}", response.Message);
        }
    }
}
