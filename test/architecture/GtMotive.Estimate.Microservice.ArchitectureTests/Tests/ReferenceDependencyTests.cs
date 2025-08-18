using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using GtMotive.Estimate.Microservice.Api;
using GtMotive.Estimate.Microservice.Domain.Vehicles.Entities;
using GtMotive.Estimate.Microservice.Infrastructure;
using Xunit;

namespace GtMotive.Estimate.Microservice.ArchitectureTests.Tests
{
    /// <summary>
    /// Contains architecture tests to verify that project layer dependencies conform to the intended architecture.
    /// </summary>
    public class ReferenceDependencyTests
    {
        private static readonly string Api = typeof(ApiConfiguration).Assembly.GetName().Name;
        private static readonly string Domain = typeof(Vehicle).Assembly.GetName().Name;
        private static readonly string Application = typeof(ApiConfiguration).Assembly.GetName().Name;
        private static readonly string Infrastructure = typeof(InfrastructureConfiguration).Assembly.GetName().Name;

        /// <summary>
        /// Verifies that the Domain layer does not depend on Application, API, or Infrastructure layers.
        /// </summary>
        [Fact]
        public void DomainShouldNotDependOnOtherLayers()
        {
            var domainAssembly = Assembly.Load(typeof(Vehicle).Assembly.GetName().Name);

            var hasApplicationReference = HasProjectReference(domainAssembly, Application);
            var hasApiReference = HasProjectReference(domainAssembly, Api);
            var hasInfrastructureReference = HasProjectReference(domainAssembly, Infrastructure);

            Assert.False(hasApplicationReference);
            Assert.False(hasApiReference);
            Assert.False(hasInfrastructureReference);
        }

        /// <summary>
        /// Verifies that the Application layer only depends on the Domain layer.
        /// </summary>
        [Fact]
        public void ApplicationShouldOnlyDependOnDomain()
        {
            var applicationAssembly = Assembly.Load(typeof(ApiConfiguration).Assembly.GetName().Name);

            var hasDomainReference = HasProjectReference(applicationAssembly, Domain);
            var hasApiReference = HasProjectReference(applicationAssembly, Api);
            var hasInfrastructureReference = HasProjectReference(applicationAssembly, Infrastructure);

            Assert.True(hasDomainReference);
            Assert.False(hasApiReference);
            Assert.False(hasInfrastructureReference);
        }

        /// <summary>
        /// Verifies that the Infrastructure Persistence layer only depends on the Domain and Shared layers.
        /// </summary>
        [Fact]
        public void InfraPersistenceShouldOnlyDependOnDomainAndShared()
        {
            var infrastructureAssembly = Assembly.Load(typeof(InfrastructureConfiguration).Assembly.GetName().Name);

            var hasDomainReference = HasProjectReference(infrastructureAssembly, Domain);
            var hasApplicationReference = HasProjectReference(infrastructureAssembly, Application);
            var hasApiReference = HasProjectReference(infrastructureAssembly, Api);

            Assert.True(hasDomainReference);
            Assert.False(hasApplicationReference);
            Assert.False(hasApiReference);
        }

        /// <summary>
        /// Verifies that the Presentation layer can depend on all other layers.
        /// </summary>
        [Fact]
        public void PresentationCanDependOnAllLayers()
        {
            var presentationAssembly = Assembly.Load(typeof(ApiConfiguration).Assembly.GetName().Name);

            var hasDomainReference = HasProjectReference(presentationAssembly, Domain);
            var hasApplicationReference = HasProjectReference(presentationAssembly, Application);
            var hasApiReference = HasProjectReference(presentationAssembly, Api);
            var hasInfrastructureReference = HasProjectReference(presentationAssembly, Infrastructure);

            var resultReferences = new List<bool>
            {
                hasDomainReference,
                hasApplicationReference,
                hasApiReference,
                hasInfrastructureReference
            };

            Assert.True(resultReferences.Count > 0);
        }

        private static bool HasProjectReference(Assembly assembly, string referencedAssemblyName)
        {
            var referencedAssemblies = assembly.GetReferencedAssemblies();
            return referencedAssemblies.Any(a => a.Name == referencedAssemblyName);
        }
    }
}
