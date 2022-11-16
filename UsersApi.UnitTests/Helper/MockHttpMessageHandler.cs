using Moq;
using Moq.Protected;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using UsersApi.API.Model;

namespace UsersApi.UnitTests.Helper
{
    internal static class MockHttpMessageHandler<T>
    {
        internal static Mock<HttpMessageHandler> SetupBasicGetResources(List<T> expectedResponse)
        {
            var mockResponse = new HttpResponseMessage(System.Net.HttpStatusCode.OK)
            {
                Content = new StringContent(JsonConvert.SerializeObject(expectedResponse))
            };

            mockResponse.Content.Headers.ContentType =
                new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");


            var handlerMock = new Mock<HttpMessageHandler>();

            handlerMock.Protected()
                .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(mockResponse);

            return handlerMock;
        }

        internal static Mock<HttpMessageHandler> SetupBasicGetResources(List<User> expectedResponse, string endpoint)
        {
            var mockResponse = new HttpResponseMessage(System.Net.HttpStatusCode.OK)
            {
                Content = new StringContent(JsonConvert.SerializeObject(expectedResponse))
            };

            mockResponse.Content.Headers.ContentType =
                new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            var handlerMock = new Mock<HttpMessageHandler>();

            //var httpRequestMessage = new HttpRequestMessage
            //{
            //    RequestUri = new Uri(endpoint),
            //    Method = HttpMethod.Get,
            //};

            handlerMock.Protected()
                .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(mockResponse);

            return handlerMock;
        }

        internal static Mock<HttpMessageHandler> SetupReturnNotFound()
        {
            var mockResponse = new HttpResponseMessage(System.Net.HttpStatusCode.NotFound)
            {
                Content = new StringContent(JsonConvert.SerializeObject(string.Empty))
            };

            mockResponse.Content.Headers.ContentType =
                new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");


            var handlerMock = new Mock<HttpMessageHandler>();

            handlerMock.Protected()
                .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(mockResponse);

            return handlerMock;
        }
    }
}
