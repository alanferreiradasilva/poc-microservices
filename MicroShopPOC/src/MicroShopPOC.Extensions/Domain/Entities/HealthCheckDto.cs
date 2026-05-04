namespace MicroShopPOC.Extensions.Domain.Entities
{
    public class HealthCheckDto
    {
        public string Status { get; set; } = string.Empty;

        public HealthCheckDto() { }

        public static HealthCheckDto GetHealthy() =>
            new HealthCheckDto { Status = HealthCheckStatus.Healthy };

        public static HealthCheckDto GetDegraded() =>
            new HealthCheckDto { Status = HealthCheckStatus.Degraded };

        public static HealthCheckDto GetUnhealthy() =>
            new HealthCheckDto { Status = HealthCheckStatus.Unhealthy };
    }
}
