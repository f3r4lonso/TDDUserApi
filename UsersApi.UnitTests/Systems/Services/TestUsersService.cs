using FluentAssertions;
using Microsoft.Extensions.Options;
using Moq;
using Moq.Protected;
using NewWebApi.UnitTests.Fixtures;
using System.Threading.Tasks;
using UsersApi.API.Config;
using UsersApi.API.Model;
using UsersApi.API.Services;
using UsersApi.UnitTests.Helper;
using Xunit;

namespace UsersApi.UnitTests.Systems.Services
{
    public class TestUsersService
    {
        [Fact]
        public async Task GetAllUsers_WhenCAlled_InvokesHttpGetRequest()
        {
            //Arrage
            var exampleResponse = UsersFixture.GetTestUsers();
            var handlerMock = MockHttpMessageHandler<User>.SetupBasicGetResources(exampleResponse);

            var httpClient = new HttpClient(handlerMock.Object);
            var endpoint = "https://example.com";
            var config = Options.Create(new UsersApiOptions
            {
                Endpoint = endpoint
            });

            var sut = new UserService(httpClient, config);

            //Act
            await sut.GetUsers();

            //Assert
            handlerMock
                .Protected()
                .Verify(
                "SendAsync",
                Times.Exactly(1),
                ItExpr.Is<HttpRequestMessage>(req => req.Method == HttpMethod.Get),
                ItExpr.IsAny<CancellationToken>()
                );
        }

        [Fact]
        public async Task GetAllUsers_WhenCAlled_ReturnListOfUsers()
        {
            //Arrage
            var exampleResponse = UsersFixture.GetTestUsers();
            var handlerMock = MockHttpMessageHandler<User>.SetupBasicGetResources(exampleResponse);

            var httpClient = new HttpClient(handlerMock.Object);
            var endpoint = "https://example.com";
            var config = Options.Create(new UsersApiOptions
            {
                Endpoint = endpoint
            });

            var sut = new UserService(httpClient, config);

            //Act
            var resul = await sut.GetUsers();


            //Assert
            resul.Should().BeOfType<List<User>>();
        }

        [Fact]
        public async Task GetAllUsers_WhenCAlled_NotFound()
        {
            //Arrage
            var handlerMock = MockHttpMessageHandler<User>.SetupReturnNotFound();
            var httpClient = new HttpClient(handlerMock.Object);
            var endpoint = "https://example.com";
            var config = Options.Create(new UsersApiOptions
            {
                Endpoint = endpoint
            });

            var sut = new UserService(httpClient, config);

            //Act
            var resul = await sut.GetUsers();

            //Assert
            resul.Count.Should().Be(0);
        }

        [Fact]
        public async Task GetAllUsers_WhenCAlled_Get3UsersList()
        {
            //Arrage
            var exampleResponse = UsersFixture.GetTestUsers();
            var handlerMock = MockHttpMessageHandler<User>.SetupBasicGetResources(exampleResponse);
            var httpClient = new HttpClient(handlerMock.Object);

            var endpoint = "https://example.com";
            var config = Options.Create(new UsersApiOptions
            {
                Endpoint = endpoint
            });

            var sut = new UserService(httpClient, config);

            //Act
            var resul = await sut.GetUsers();

            //Assert
            resul.Count.Should().Be(exampleResponse.Count);
        }

        // Configuration testing
        [Fact]
        public async Task GetAllUsers_WhenCAlled_InvokesConfiguresExternalUrl()
        {
            //Arrage
            var exampleResponse = UsersFixture.GetTestUsers();
            
            var endpoint = "https://example.com/users";


            var handlerMock = MockHttpMessageHandler<User>.SetupBasicGetResources(exampleResponse, endpoint);
            var httpClient = new HttpClient(handlerMock.Object);


            var config = Options.Create(
                new UsersApiOptions {
                    Endpoint = endpoint
            });

            var sut = new UserService(httpClient, config);

            var uri = new Uri(endpoint);

            //Act
            var resul = await sut.GetUsers();

            //Assert
            handlerMock
              .Protected()
              .Verify(
              "SendAsync",
              Times.Exactly(1),
              ItExpr.Is<HttpRequestMessage>(req => req.Method == HttpMethod.Get && req.RequestUri == uri),
              ItExpr.IsAny<CancellationToken>()
              );
        }
    }
}
