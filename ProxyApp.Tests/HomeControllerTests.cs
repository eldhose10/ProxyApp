using Newtonsoft.Json.Linq;
using Moq;
using ProxyApp.Controllers;
using ProxyApp.Services; 
using System.Net;
using Xunit;

namespace ProxyApp.Tests
{
    public class HomeControllerTests
    {
        private readonly HomeController _systemUnderTest;
        private readonly Mock<IExternalApiService> _apiServiceMock;

        public HomeControllerTests()
        {
            _apiServiceMock = new Mock<IExternalApiService>();
            _systemUnderTest = new HomeController(_apiServiceMock.Object);
        }

        [Fact]
        public void GetResource_ReturnsExpectedResult()
        {
            // Arrange
            var resource = "people";
            string resourceName = "Luke Skywalker";
            var expectedResponse = "{\"results\":[{\"name\":\"Luke Skywalker\"}]}";

            _apiServiceMock.Setup(service => service.GetResource(resource)).Returns(expectedResponse);

            // Act 
            var response = _systemUnderTest.GetResource(resource);

            // Assert
            JObject jsonResponse = JObject.Parse(response);
            Assert.Equal(resourceName, jsonResponse["results"][0]["name"].ToString());
        }
    }
}
