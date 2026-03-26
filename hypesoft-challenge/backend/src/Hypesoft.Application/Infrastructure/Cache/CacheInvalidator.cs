using Microsoft.Extensions.Caching.Distributed;

namespace Hypesoft.Application.Infrastructure.Cache;


public interface ICacheInvalidator
{

    Task InvalidateProductCache(Guid? productId = null, CancellationToken cancellationToken = default);
    

    Task InvalidateCategoryProductsCache(Guid categoryId, CancellationToken cancellationToken = default);
    

    Task InvalidateAllProductCaches(CancellationToken cancellationToken = default);
}

public class CacheInvalidator : ICacheInvalidator
{
    private readonly IDistributedCache _cache;

    public CacheInvalidator(IDistributedCache cache)
    {
        _cache = cache;
    }

    public async Task InvalidateProductCache(Guid? productId = null, CancellationToken cancellationToken = default)
    {
        if (productId.HasValue)
        {
            // Invalida apenas este produto específico
            await _cache.RemoveAsync(CacheKeys.ProductById(productId.Value), cancellationToken);
        }
        else
        {
            for (int page = 1; page <= 100; page++)  
            {
                for (int pageSize = 5; pageSize <= 100; pageSize += 5)
                {
                    await _cache.RemoveAsync(CacheKeys.ProductsPaged(page, pageSize), cancellationToken);
                }
            }
        }
    }


    public async Task InvalidateCategoryProductsCache(Guid categoryId, CancellationToken cancellationToken = default)
    {
        await _cache.RemoveAsync(CacheKeys.ProductsByCategory(categoryId), cancellationToken);
    }


    public async Task InvalidateAllProductCaches(CancellationToken cancellationToken = default)
    {
                await InvalidateProductCache(null, cancellationToken);

    }
}
