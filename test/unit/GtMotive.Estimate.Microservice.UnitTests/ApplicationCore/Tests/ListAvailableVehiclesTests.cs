using System;
using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Vehicle.ListAvailableVehicles;
using GtMotive.Estimate.Microservice.Domain.Vehicles.Entities;
using GtMotive.Estimate.Microservice.Domain.Vehicles.Interfaces;
using Moq;
using Xunit;

namespace GtMotive.Estimate.Microservice.UnitTests.ApplicationCore.Tests
{
    /// <summary>
    /// ListAvailableVehicles.
    /// </summary>
    public class ListAvailableVehiclesTests
    {
        private readonly ListAvailableVehiclesUseCase _sut;
        private readonly Mock<IVehicleRepository> _vehicleRepositoryMock;
        private readonly Mock<IListAvailableVehiclesOutputPort> _outputPortMock;

        /// <summary>
        /// Initializes a new instance of the <see cref="ListAvailableVehiclesTests"/> class.
        /// </summary>
        public ListAvailableVehiclesTests()
        {
            _vehicleRepositoryMock = new Mock<IVehicleRepository>();
            _outputPortMock = new Mock<IListAvailableVehiclesOutputPort>();
            _sut = new ListAvailableVehiclesUseCase(
                outputPort: _outputPortMock.Object,
                vehicleRepository: _vehicleRepositoryMock.Object);
        }

        /// <summary>
        /// Tests that the use case returns NotFound when no available vehicles are present in the repository.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous unit test.</returns>
        [Fact]
        public async Task HandleShouldReturnNotFoundWhenNoAvailableVehicles()
        {
            // Arrange
            _vehicleRepositoryMock
                .Setup(v => v.GetAvailableVehiclesAsync())
                .ReturnsAsync([]);

            // Act
            await _sut.Execute(new ListAvailableVehiclesInput());

            // Assert
            _outputPortMock.Verify(o => o.NotFoundHandle(It.IsAny<string>()), Times.Once);
        }

        /// <summary>
        /// Tests that the use case returns available vehicles when vehicles are present in the repository.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous unit test.</returns>
        [Fact]
        public async Task HandleShouldReturnAvailableVehicles()
        {
            // Arrange
            var vehicles = new[]
            {
                Vehicle.Create(manufactureYear: DateTime.UtcNow.Year - 1),
                Vehicle.Create(manufactureYear: DateTime.UtcNow.Year - 2),
                Vehicle.Create(manufactureYear: DateTime.UtcNow.Year - 3)
            };

            _vehicleRepositoryMock
                .Setup(v => v.GetAvailableVehiclesAsync())
                .ReturnsAsync(vehicles);

            // Act
            await _sut.Execute(new ListAvailableVehiclesInput());

            // Assert
            _outputPortMock.Verify(o => o.StandardHandle(It.Is<ListAvailableVehiclesOutput>(r => r.Vehicles == vehicles)), Times.Once);
        }
    }
}
