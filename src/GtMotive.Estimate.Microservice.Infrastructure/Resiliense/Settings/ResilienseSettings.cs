namespace GtMotive.Estimate.Microservice.Infrastructure.Resiliense.Settings
{
    public class ResilienseSettings
    {
        public const string SectionName = "ResilienceStrategy";

        public int RetryCount { get; set; }

        public int RetryDelayMilliseconds { get; set; }

        public double CircuitBreakerFailureThreshold { get; set; }

        public int CircuitBreakerDurationSeconds { get; set; }

        public int CircuitBreakerSamplingDurationSeconds { get; set; }

        public int CircuitBreakerMinimumThroughput { get; set; }
    }
}
