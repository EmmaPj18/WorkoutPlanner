using System.Text.Json;

namespace WorkoutPlanner.Common.Extensions;

public static class JsonExtensions
{
    public static string? ToJson(this object? value, bool indent = false)
    {
        JsonSerializerOptions options = new();

        if (indent)
        {
            options.WriteIndented = true;
        }

        return value is null
            ? null
            : JsonSerializer.Serialize(value, options);
    }

    public static T? FromJson<T>(this string? value)
    {

        return string.IsNullOrWhiteSpace(value) ?
            default :
            JsonSerializer.Deserialize<T>(value);
    }
}
