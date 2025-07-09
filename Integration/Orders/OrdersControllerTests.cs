using Application.Orders.Commands;
using Application.Orders.Dtos;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using SharedKernel.Dto;
using System.Net;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Xunit;

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

        [Fact]
        public async Task CreateOrder_Should_Return_OrderId_With_ValidInput()
        {
            var command = new CreateOrderCommand()
            {
                CustomerId = Guid.NewGuid(),
                Items = new List<OrderItemDto>()
                {
                    new OrderItemDto()
                    {
                        UnitPrice = 100,
                        ProductId = Guid.NewGuid(),
                        Quantity = 1
                    },
                    new OrderItemDto()
                    {
                        ProductId = Guid.NewGuid(),
                        Quantity = 2,
                        UnitPrice = 80
                    }
                }
            };

            var response = await _client.PostAsJsonAsync("/api/orders", command);

            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var result = await response.Content.ReadFromJsonAsync<ApiResult<Guid>>();

            result.Should().NotBeNull();
            result.Success.Should().BeTrue();

            result.Data.Should().NotBeEmpty();
        }
    }
}