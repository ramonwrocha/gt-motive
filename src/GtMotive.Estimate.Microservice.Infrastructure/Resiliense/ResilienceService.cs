using System;
using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.Domain.Interfaces;
using GtMotive.Estimate.Microservice.Infrastructure.Resiliense.Settings;
using Microsoft.Extensions.Options;
using Polly;
using Polly.Wrap;

namespace GtMotive.Estimate.Microservice.Infrastructure.Resiliense
{
    public class ResilienceService : IResilienceService
    {
        private readonly AsyncPolicyWrap _policy;

        public ResilienceService(IOptions<ResilienseSettings> resilienseSettings)
        {
            ArgumentNullException.ThrowIfNull(resilienseSettings);

            var settings = resilienseSettings.Value;

            var retryPolicy = Policy
                .Handle<Exception>()
                .WaitAndRetryAsync(
                    retryCount: settings.RetryCount,
                    sleepDurationProvider: retryAttempt => TimeSpan.FromSeconds(retryAttempt));

            var circuitBreakerPolicy = Policy
                .Handle<Exception>()
                .AdvancedCircuitBreakerAsync(
                    failureThreshold: settings.CircuitBreakerFailureThreshold, // Break if some percent of requests fail (0.5 => 50%)
                    samplingDuration: TimeSpan.FromSeconds(settings.CircuitBreakerSamplingDurationSeconds), // over what period to sample
                    minimumThroughput: settings.CircuitBreakerMinimumThroughput, // minimum number of requests in sampling duration
                    durationOfBreak: TimeSpan.FromSeconds(settings.CircuitBreakerDurationSeconds)); // How long to break

            _policy = Policy.WrapAsync(retryPolicy, circuitBreakerPolicy);
        }

        public async Task<T> ExecuteAsync<T>(Func<Task<T>> operation)
        {
            return await _policy.ExecuteAsync(operation);
        }

        public async Task ExecuteAsync(Func<Task> operation)
        {
            await _policy.ExecuteAsync(operation);
        }
    }
}
