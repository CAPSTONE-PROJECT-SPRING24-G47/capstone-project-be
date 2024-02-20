using AutoMapper;
using capstone_project_be.Application.DTOs;
using capstone_project_be.Application.Features.Auths.Handles;
using capstone_project_be.Application.Features.Auths.Requests;
using capstone_project_be.Application.Interfaces;
using capstone_project_be.Application.Responses;
using capstone_project_be.Domain.Entities;
using Moq;
using System.Linq.Expressions;

namespace capstone_project_be.Test.AuthTests.HandlesTests
{
    public class SignInHandlerTests
    {
        [Fact]
        public async void SignInHandler_Handle_ReturnSuccessResponse()
        {
            //Arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var mockMapper = new Mock<IMapper>();

            var userSignInData = new UserSignInDTO()
            {
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

            var handler = new SignInHandler(mockUnitOfWork.Object, mockMapper.Object);
            var request = new SignInRequest(userSignInData);

            //Act
            var response = await handler.Handle(request, CancellationToken.None) as BaseResponse<UserDTO>;

            //Assert
            Assert.NotNull(response);
            Assert.True(response.IsSuccess);
            Assert.Equal("Đăng nhập thành công!", response.Message);
        }

        [Fact]
        public async void SignInHandler_Handle_ReturnFailResponse()
        {
            //Arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var mockMapper = new Mock<IMapper>();

            var userSignInData = new UserSignInDTO()
            {
                Email = "test@example.com",
                Password = "password123",
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

            var handler = new SignInHandler(mockUnitOfWork.Object, mockMapper.Object);
            var request = new SignInRequest(userSignInData);

            //Act
            var response = await handler.Handle(request, CancellationToken.None) as BaseResponse<UserDTO>;

            //Assert
            Assert.NotNull(response);
            Assert.False(response.IsSuccess);
            Assert.Equal("Tài khoản hoặc mật khẩu không đúng!", response.Message);
        }
    }
}
