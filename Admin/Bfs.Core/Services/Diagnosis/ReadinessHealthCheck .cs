using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Bfs.Core.Services.Diagnosis
{
    public class ReadinessHealthCheck : IHealthCheck
    {
        private static volatile bool _isReady = false;

        public static void MarkReady() => _isReady = true;

        public Task<HealthCheckResult> CheckHealthAsync(
            HealthCheckContext context,
            CancellationToken cancellationToken = default)
        {
            if (_isReady)
                return Task.FromResult(HealthCheckResult.Healthy("Application is ready"));

            return Task.FromResult(HealthCheckResult.Unhealthy("Application is still starting"));
        }
    }

}
