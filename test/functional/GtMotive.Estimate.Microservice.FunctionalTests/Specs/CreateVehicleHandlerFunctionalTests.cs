using System;
using System.Text.Json;
using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.Abstractions.Interfaces;
using GtMotive.Estimate.Microservice.Api.UseCases.Vehicle.CreateVehicle;
using GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Vehicle.CreateVehicle;
using GtMotive.Estimate.Microservice.FunctionalTests.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace GtMotive.Estimate.Microservice.FunctionalTests.Specs
{
    [Collection(TestCollections.Functional)]
    public class CreateVehicleHandlerFunctionalTests(CompositionRootTestFixture fixture) : FunctionalTestBase(fixture)
    {
        [Fact]
        public async Task ShouldCreateVehicleAndReturnOutput()
        {
            // Arrange
            var currentYear = DateTime.UtcNow.Year;
            var command = new CreateVehicleCommand(ManufactureYear: currentYear);

            // Act & Assert
            await Fixture.UsingHandlerForRequestResponse<CreateVehicleCommand, IWebApiPresenter>(async handler =>
            {
                var presenter = await handler.Handle(command, default);

                var createVehiclePresenter = presenter as CreateVehiclePresenter;
                Assert.NotNull(createVehiclePresenter);

                var createdResult = createVehiclePresenter.ActionResult as CreatedResult;
                Assert.NotNull(createdResult);

                var json = JsonSerializer.Serialize(createdResult.Value);
                var output = JsonSerializer.Deserialize<CreateVehicleOutput>(json);

                Assert.NotNull(output);
                Assert.Equal(currentYear, output.ManufactureYear);
                Assert.False(string.IsNullOrWhiteSpace(output.VehicleId));
            });
        }
    }
}
