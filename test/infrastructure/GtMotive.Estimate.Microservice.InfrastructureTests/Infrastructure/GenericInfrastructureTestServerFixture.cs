using System;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Mongo2Go;

[assembly: CLSCompliant(false)]

namespace GtMotive.Estimate.Microservice.InfrastructureTests.Infrastructure
{
    public class GenericInfrastructureTestServerFixture : IDisposable
    {
        private readonly MongoDbRunner _mongoRunner;

        public GenericInfrastructureTestServerFixture()
        {
            _mongoRunner = MongoDbRunner.Start();
            MongoConnectionString = _mongoRunner.ConnectionString;

            var hostBuilder = new WebHostBuilder()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseEnvironment("IntegrationTest")
                .UseStartup<Startup>();

            Server = new TestServer(hostBuilder);
        }

        public static string MongoConnectionString { get; private set; }

        public TestServer Server { get; }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                Server.Dispose();
                _mongoRunner.Dispose();
            }
        }
    }
}
