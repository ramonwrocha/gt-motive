using GtMotive.Estimate.Microservice.Domain.Rentals.Entities;
using Xunit;

namespace GtMotive.Estimate.Microservice.UnitTests.Domain.Tests
{
    /// <summary>
    /// Contains unit tests for the <see cref="Rental"/> entity.
    /// </summary>
    public static class RentalTests
    {
        /// <summary>
        /// Verifies that a newly created rental has a null EndDate.
        /// </summary>
        [Fact]
        public static void NewRentalHasNullEndDate()
        {
            // Arrange
            var rental = Rental.Create("vehicle-1", "person-1");

            // Assert
            Assert.Null(rental.EndDate);
        }

        /// <summary>
        /// Verifies that calling <see cref="Rental.End"/> on an active rental sets the end date and marks the rental as inactive.
        /// </summary>
        [Fact]
        public static void EndSetsEndDateWhenRentalIsActive()
        {
            // Arrange
            var rental = Rental.Create("vehicle-1", "person-1");

            // Act
            rental.End();

            // Assert
            Assert.False(rental.IsActive);
            Assert.NotNull(rental.EndDate);
        }
    }
}
