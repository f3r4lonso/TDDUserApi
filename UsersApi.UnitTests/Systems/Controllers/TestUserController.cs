using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using NewWebApi.UnitTests.Fixtures;
using UsersApi.API.Controllers;
using UsersApi.API.Model;
using UsersApi.API.Services;

namespace UsersApi.UnitTests.Systems.Controllers;

public class TestUserController
{
    [Fact]
    public async void GetUssers_OnSuccess_OKCode()
    {
        //Arrange
        var mockUserService = new Mock<IUserService>();
        var mockLogguer = new Mock<ILogger<UserController>>();

        mockUserService.Setup(s => s.GetUsers()).ReturnsAsync(UsersFixture.GetTestUsers());

        var userService = new UserController(mockUserService.Object, mockLogguer.Object);

        //Act
        var result = (OkObjectResult)await userService.GetAllUsers();

        //Assert
        result.StatusCode.Should().Be(200);
    }

    [Fact]
    public async void GetAllUsers_OnSuccess_NotFoundCode()
    {
        //Arrange
        var mockUserService = new Mock<IUserService>();
        var mockLogguer = new Mock<ILogger<UserController>>();

        mockUserService.Setup(s => s.GetUsers()).ReturnsAsync(new List<User>());

        var userService = new UserController(mockUserService.Object, mockLogguer.Object);

        //Act
        var result = (NotFoundObjectResult)await userService.GetAllUsers();

        //Assert
        result.StatusCode.Should().Be(404);
    }

    [Fact]
    public async void GetAllUsers_OnSuccess_InvokersUserServiceOnceTime()
    {
        //Arrange
        var mockUserService = new Mock<IUserService>();
        var mockLogguer = new Mock<ILogger<UserController>>();

        mockUserService.Setup(s => s.GetUsers()).ReturnsAsync(new List<User>());

        var userService = new UserController(mockUserService.Object, mockLogguer.Object);

        //Act
        var result = await userService.GetAllUsers();

        //Assert
        mockUserService.Verify(s => s.GetUsers(), Times.Once);
    }

    [Fact]
    public async void GetAllUsers_OnSuccess_InvokersLoggerMessageOK()
    {
        //Arrange
        var mockUserService = new Mock<IUserService>();
        var mockLogguer = new Mock<ILogger<UserController>>();

        mockUserService.Setup(s => s.GetUsers()).ReturnsAsync(UsersFixture.GetTestUsers());

        var userService = new UserController(mockUserService.Object, mockLogguer.Object);

        //Act
        var result = await userService.GetAllUsers();

        //Assert
        mockLogguer.Verify(
                        x => x.Log(
                        It.Is<LogLevel>(l => l == LogLevel.Information),
                        It.IsAny<EventId>(),
                        It.Is<It.IsAnyType>((v, t) => v.ToString() == "OK"),
                        It.IsAny<Exception>(),
                        It.Is<Func<It.IsAnyType, Exception, string>>((v, t) => true))
                        );
    }

    [Fact]
    public async void GetAllUsers_OnSuccess_InvokersLoggerMessageNotFound()
    {
        //Arrange
        var mockUserService = new Mock<IUserService>();
        var mockLogguer = new Mock<ILogger<UserController>>();

        mockUserService.Setup(s => s.GetUsers()).ReturnsAsync(new List<User>());

        var userService = new UserController(mockUserService.Object, mockLogguer.Object);

        //Act
        var result = await userService.GetAllUsers();

        //Assert
        mockLogguer.Verify(
                    x => x.Log(
                    It.Is<LogLevel>(l => l == LogLevel.Information),
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((v, t) => v.ToString() == "Not Found"),
                    It.IsAny<Exception>(),
                    It.Is<Func<It.IsAnyType, Exception, string>>((v, t) => true))
                    );
    }


    [Fact]
    public async void GetAllUsers_OnSuccess_LIstOfUsers()
    {
        //Arrange
        var mockUserService = new Mock<IUserService>();
        var mockLogguer = new Mock<ILogger<UserController>>();

        mockUserService.Setup(s => s.GetUsers()).ReturnsAsync(UsersFixture.GetTestUsers());

        var userService = new UserController(mockUserService.Object, mockLogguer.Object);

        //Act
        var result = await userService.GetAllUsers();

        //Assert
        result.Should().BeOfType<OkObjectResult>();
        var objectResult = (OkObjectResult)result;
        objectResult.Value.Should().BeOfType<List<User>>();
    }


    [Fact]
    public async void GetAllUsers_OnNotUsersFound_Returns404()
    {
        //Arrange
        var mockUserService = new Mock<IUserService>();
        var mockLogguer = new Mock<ILogger<UserController>>();

        mockUserService.Setup(s => s.GetUsers()).ReturnsAsync(new List<User>());

        var userService = new UserController(mockUserService.Object, mockLogguer.Object);

        //Act
        var result = await userService.GetAllUsers();

        //Assert
        result.Should().BeOfType<NotFoundObjectResult>();
        
    }
}