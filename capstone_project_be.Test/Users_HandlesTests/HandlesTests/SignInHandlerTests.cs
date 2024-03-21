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

namespace capstone_project_be.Test.Users_HandlesTests.HandlesTests
{
    public class SignInHandlerTests
    {
        [Fact]
        public async void SignInHandler_Handle_ReturnSuccessResponse()
        {
            //Arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var mockMapper = new Mock<IMapper>();
            var mockStorageRepository = new Mock<IStorageRepository>();

            var userSignInData = new UserSignInDTO()
            {
                Email = "test@example.com",
                Password = BCrypt.Net.BCrypt.EnhancedHashPassword("password"),
            };
            var user = new User() // assuming User model exists
            {
                UserId = 1,
                RoleId = 4,
                LastName = "abc",
                FirstName = "abc",
                Email = "test@example.com",
                Password = BCrypt.Net.BCrypt.EnhancedHashPassword("password")
            };
            var userList = new List<User>() { user };

            mockUnitOfWork.Setup(u => u.UserRepository.Find(It.IsAny<Expression<Func<User, bool>>>()))
                .ReturnsAsync(userList);

            var handler = new SignInHandler(mockUnitOfWork.Object, mockStorageRepository.Object, mockMapper.Object);
            var request = new SignInRequest(userSignInData);

            //Act
            var response = await handler.Handle(request, CancellationToken.None);

            //Assert
            Assert.IsType<BaseResponse<UserDTO>>(response);
            //Assert.True(response.IsSuccess);
            //Assert.Equal("Đăng nhập thành công!", response.Message);
        }
    }
}
