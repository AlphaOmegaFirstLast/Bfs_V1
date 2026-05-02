using Bfs.Core.Config;
using Bfs.Core.TenantManagement;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

public class CacheWarmingService : BackgroundService
{
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly IMemoryCache _cache;
    private readonly ILogger<CacheWarmingService> _logger;

    // How often to refresh the cache
    private readonly TimeSpan _refreshInterval = TimeSpan.FromMinutes(5);

    public CacheWarmingService(
        IServiceScopeFactory scopeFactory,
        IMemoryCache cache,
        ILogger<CacheWarmingService> logger)
    {
        _scopeFactory = scopeFactory;
        _cache = cache;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        // Warm immediately on startup — don't wait for first timer tick
        await LoadTenantsIntoCache(stoppingToken);

        using var timer = new PeriodicTimer(_refreshInterval);

        while (!stoppingToken.IsCancellationRequested
               && await timer.WaitForNextTickAsync(stoppingToken))
        {
            await LoadTenantsIntoCache(stoppingToken);
        }
    }

    private async Task LoadTenantsIntoCache(CancellationToken ct)
    {
        try
        {
            // Use a scope because your data service is likely scoped/transient
            using var scope = _scopeFactory.CreateScope();
            var dataService = scope.ServiceProvider.GetRequiredService<ITenantManager>();

            var data = await dataService.FetchDataAsync(ct);

            var options = new MemoryCacheEntryOptions
            {
                // Safety net: if background service dies, cache expires
                // rather than serving stale data forever
                AbsoluteExpirationRelativeToNow = _refreshInterval * 3,
                Priority = CacheItemPriority.NeverRemove
            };

            var cacheKey = dataService.GetCacheKey(); 
            _cache.Set(cacheKey, data, options);

            _logger.LogInformation("Cache refreshed at {Time}", DateTimeOffset.UtcNow);
        }
        catch (Exception ex)
        {
            // Don't crash the service — log and keep old cached value
            _logger.LogError(ex, "Failed to refresh cache. Serving stale data.");
        }
    }
}