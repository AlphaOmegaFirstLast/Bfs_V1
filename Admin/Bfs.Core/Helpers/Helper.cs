using System.Text.Json;

namespace Bfs.Core.Helpers;

public static class SerializationHelper
{
    private static string SerializeContract<TContract>(TContract contract)
    {
        return JsonSerializer.Serialize(contract);
    }

    private static TEntity? DeserializeToEntity<TEntity>(string json)
    {
        return JsonSerializer.Deserialize<TEntity>(json);
    }

    /// <summary>
    /// Helper to get a strongly-typed parsed value from jsonProperty.
    /// Returns default(T) when not present or cannot be cast.
    /// attempts to deserialize to T.
    /// </summary>
    public static Tout? GetParsed<Tin, Tout>(Tin item, string jsonProperty, JsonSerializerOptions? options = null) 
            where Tin : class
            where Tout : class
    {
        if (item == null) return default;

        var prop = item.GetType().GetProperty(jsonProperty);
        if (prop == null) return default;

        var value = prop.GetValue(item) as string; ;
        if (string.IsNullOrWhiteSpace(value)) return default;

        try
        {
            return JsonSerializer.Deserialize<Tout>(value);
        }
        catch
        {
            return default;
        }
    }

    // Convenience method to transform from TContract to TEntity
    public static TEntity? DoMapping<TContract, TEntity>(TContract contract)
    {
        var json = SerializeContract(contract);
        return DeserializeToEntity<TEntity>(json);
    }
}
