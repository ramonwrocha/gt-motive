using System;
using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Vehicle.CreateVehicle;
using GtMotive.Estimate.Microservice.Domain.Vehicles.Entities;
using GtMotive.Estimate.Microservice.Domain.Vehicles.Interfaces;
using Moq;
using Xunit;

namespace GtMotive.Estimate.Microservice.UnitTests.ApplicationCore.Tests
{
    /// <summary>
    /// CreateVehicleUseCaseTests.
    /// </summary>
    public class CreateVehicleUseCaseTests
    {
        private readonly CreateVehicleUseCase _sut;
        private readonly Mock<IVehicleRepository> _vehicleRepositoryMock;
        private readonly Mock<ICreateVehicleOutputPort> _outputPortMock;

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateVehicleUseCaseTests"/> class.
        /// </summary>
        public CreateVehicleUseCaseTests()
        {
            _vehicleRepositoryMock = new Mock<IVehicleRepository>();
            _outputPortMock = new Mock<ICreateVehicleOutputPort>();
            _sut = new CreateVehicleUseCase(
                outputPort: _outputPortMock.Object,
                vehicleRepository: _vehicleRepositoryMock.Object);
        }

        /// <summary>
        /// Should create a vehicle when the provided data is valid.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous unit test.</returns>
        [Fact]
        public async Task HandleShouldCreateVehicleWhenDataIsValid()
        {
            // Arrange
            var input = new CreateVehicleInput(manufactureYear: DateTime.UtcNow.Year - 1);

            // Act
            await _sut.Execute(input);

            // Assert
            _vehicleRepositoryMock.Verify(v => v.AddAsync(It.IsAny<Vehicle>()), Times.Once);
        }
    }
}
