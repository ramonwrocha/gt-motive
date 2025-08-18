using Xunit;

namespace GtMotive.Estimate.Microservice.InfrastructureTests.Infrastructure
{
    [Collection(TestCollections.TestServer)]
    public abstract class InfrastructureTestBase(GenericInfrastructureTestServerFixture fixture) : IClassFixture<GenericInfrastructureTestServerFixture>
    {
        protected GenericInfrastructureTestServerFixture Fixture { get; } = fixture;
    }
}
