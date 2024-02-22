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
    public class GoogleAuthHandlerTests
    {
        [Fact]
        public async void GoogleAuthHandler_Handle_ReturnFailResponse()
        {
            //Arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var mockMapper = new Mock<IMapper>();

            var googleAuthData = new GoogleAuthDTO()
            {
                LastName = "abc",
                FirstName = "abc",
                Email = "test@example.com",
                PictureProfile = "",
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

            var handler = new GoogleAuthHandler(mockUnitOfWork.Object, mockMapper.Object);
            var request = new GoogleAuthRequest(googleAuthData);

            //Act
            var response = await handler.Handle(request, CancellationToken.None) as BaseResponse<UserDTO>;

            //Assert
            Assert.NotNull(response);
            Assert.False(response.IsSuccess);
            Assert.Equal("Email đã được sử dụng ở tài khoản khác", response.Message);
        }

        [Fact]
        public async void GoogleAuthHandler_Handle_ReturnSuccessSignUpResponse()
        {
            //Arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var mockMapper = new Mock<IMapper>();

            var googleAuthData = new GoogleAuthDTO()
            {
                LastName = "abc",
                FirstName = "abc",
                Email = "test@example.com",
                PictureProfile = "",
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

            var handler = new GoogleAuthHandler(mockUnitOfWork.Object, mockMapper.Object);
            var request = new GoogleAuthRequest(googleAuthData);

            //Act
            var response = await handler.Handle(request, CancellationToken.None) as BaseResponse<UserDTO>;

            //Assert
            Assert.NotNull(response);
            Assert.True(response.IsSuccess);
            Assert.Equal("Đăng nhập thành công", response.Message);
        }

        //[Fact]
        //public async void GoogleAuthHandler_Handle_ReturnSuccessSignInResponse()
        //{
        //    //Arrange
        //    var mockUnitOfWork = new Mock<IUnitOfWork>();
        //    var mockMapper = new Mock<IMapper>();

        //    var googleAuthData = new GoogleAuthDTO()
        //    {
        //        LastName = "abc",
        //        FirstName = "abc",
        //        Email = "test@example.com",
        //        PictureProfile = "",
        //    };
        //    var userList = new List<User>();

        //    mockUnitOfWork.Setup(u => u.UserRepository.Find(It.IsAny<Expression<Func<User, bool>>>()))
        //        .ReturnsAsync(userList);
        //    mockMapper.Setup(m => m.Map<UserDTO>(It.IsAny<User>()))
        //        .Returns((User user) => new UserDTO 
        //        {
        //            UserId = user.UserId,
        //            RoleId = user.RoleId,
        //            LastName = user.LastName,
        //            FirstName = user.FirstName,
        //            Email = user.Email,
        //            PictureProfile = user.PictureProfile,
        //            IsGoogleAuth = user.IsGoogleAuth,
        //            Status = user.Status,
        //            CreatedAt = user.CreatedAt
        //        });

        //    var handler = new GoogleAuthHandler(mockUnitOfWork.Object, mockMapper.Object);
        //    var request = new GoogleAuthRequest(googleAuthData);

        //    //Act
        //    var response = await handler.Handle(request, CancellationToken.None) as BaseResponse<UserDTO>;

        //    //Assert
        //    Assert.NotNull(response);
        //    Assert.True(response.IsSuccess);
        //    Assert.Equal("Đăng ký thành công", response.Message);
        //}
    }
}
