using System.Net.Http.Json;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

using Microsoft.VisualStudio.TestPlatform.TestHost;

namespace Integration.Orders
{
    public class OrdersControllerTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;

        public OrdersControllerTests(WebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task CreateOrder_Should_Return_BadRequest_If_Model_Invalid()
        {
            var command = new { ProductId = "", Quantity = 0 };

            var response = await _client.PostAsJsonAsync("/api/orders", command);

            response.StatusCode.Should().Be(System.Net.HttpStatusCode.BadRequest);
        }
    }
}