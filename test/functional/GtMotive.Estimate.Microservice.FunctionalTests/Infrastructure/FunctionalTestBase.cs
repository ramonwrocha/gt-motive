using System.Threading.Tasks;
using Xunit;

namespace GtMotive.Estimate.Microservice.FunctionalTests.Infrastructure
{
    [Collection(TestCollections.Functional)]
    public abstract class FunctionalTestBase(CompositionRootTestFixture fixture) : IAsyncLifetime
    {
        protected CompositionRootTestFixture Fixture { get; } = fixture;

        public virtual Task InitializeAsync() => Task.CompletedTask;

        public virtual Task DisposeAsync() => Task.CompletedTask;
    }
}
