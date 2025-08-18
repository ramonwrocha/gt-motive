using System;
using System.Net;
using System.Net.Http.Json;
using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.InfrastructureTests.Infrastructure;
using Xunit;

namespace GtMotive.Estimate.Microservice.InfrastructureTests.Specs.Api.Vehicles
{
    public sealed class CreateVehicleTests(GenericInfrastructureTestServerFixture fixture) : InfrastructureTestBase(fixture)
    {
        private readonly string _path = "/api/vehicles";

        [Fact]
        public async Task PostCreateVehicleReturnsBadRequestWhenManufactureYearIsTooOld()
        {
            // Arrange
            var client = Fixture.Server.CreateClient();
            var invalidVehicle = new
            {
                manufactureYear = 2019
            };

            // Act
            var response = await client.PostAsJsonAsync(_path, invalidVehicle);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task PostCreateVehicleReturnsCreatedWhenDataIsValid()
        {
            // Arrange
            var client = Fixture.Server.CreateClient();
            var validVehicle = new
            {
                manufactureYear = DateTime.Now.Year - 1
            };

            // Act
            var response = await client.PostAsJsonAsync(_path, validVehicle);

            // Assert
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        }
    }
}
