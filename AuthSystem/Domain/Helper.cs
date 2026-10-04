using System.Text.Json;

namespace [TemplateSln].Domain;

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

    // Convenience method to transform from TContract to TEntity
    public static TEntity? DoMapping<TContract, TEntity>(TContract contract)
    {
        var json = SerializeContract(contract);
        return DeserializeToEntity<TEntity>(json);
    }
}
