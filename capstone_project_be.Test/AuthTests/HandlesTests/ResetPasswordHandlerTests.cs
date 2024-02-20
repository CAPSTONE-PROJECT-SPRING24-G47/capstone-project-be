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
    public class ResetPasswordHandlerTests
    {
        [Fact]
        public async void ResetPasswordHandler_Handle_ReturnFailResponse()
        {
            //Arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var mockMapper = new Mock<IMapper>();

            var resetPasswordData = new ResetPasswordDTO()
            {
                Email = "test123@example.com",
                Password = "password",
            };
            
            var userList = new List<User>();

            mockUnitOfWork.Setup(u => u.UserRepository.Find(It.IsAny<Expression<Func<User, bool>>>()))
                .ReturnsAsync(userList);

            var handler = new ResetPasswordHandler(mockUnitOfWork.Object, mockMapper.Object);
            var request = new ResetPasswordRequest(resetPasswordData);

            //Act
            var response = await handler.Handle(request, CancellationToken.None) as BaseResponse<UserDTO>;

            //Assert
            Assert.NotNull(response);
            Assert.False(response.IsSuccess);
            Assert.Equal("Xảy ra lỗi khi cập nhật mật khẩu !", response.Message);
        }

        [Fact]
        public async void ResetPasswordHandler_Handle_ReturnSuccessResponse()
        {
            //Arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var mockMapper = new Mock<IMapper>();

            var resetPasswordData = new ResetPasswordDTO()
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
                IsGoogleAuth = false,
                IsVerified = true,
                Status = false,
                CreatedAt = DateTime.Now
            };
            var userList = new List<User>() { user };

            mockUnitOfWork.Setup(u => u.UserRepository.Find(It.IsAny<Expression<Func<User, bool>>>()))
                .ReturnsAsync(userList);

            var handler = new ResetPasswordHandler(mockUnitOfWork.Object, mockMapper.Object);
            var request = new ResetPasswordRequest(resetPasswordData);

            //Act
            var response = await handler.Handle(request, CancellationToken.None) as BaseResponse<UserDTO>;

            //Assert
            Assert.NotNull(response);
            Assert.True(response.IsSuccess);
            Assert.Equal("Cập nhật mật khẩu thành công !", response.Message);
        }
    }
}
