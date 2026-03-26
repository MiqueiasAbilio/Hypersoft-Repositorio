namespace Hypesoft.Application.Infrastructure.Cache;

public static class CacheKeys
{
    public const string PRODUCTS_PREFIX = "products";
    public const string PRODUCTS_PAGED = $"{PRODUCTS_PREFIX}:paged";
    public const string PRODUCTS_BY_CATEGORY = $"{PRODUCTS_PREFIX}:category";
    public const string PRODUCT_BY_ID = $"{PRODUCTS_PREFIX}:id";
    
    public static string ProductsPaged(int page, int pageSize) => $"{PRODUCTS_PAGED}:{page}:{pageSize}";
    
    public static string ProductsByCategory(Guid categoryId) => $"{PRODUCTS_BY_CATEGORY}:{categoryId}";
    
    public static string ProductById(Guid productId) => $"{PRODUCT_BY_ID}:{productId}";
}
