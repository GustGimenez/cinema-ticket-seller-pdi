using System.Text.Json;

namespace Commons.Extensions;

public static class JsonExtensions
{
    private static readonly JsonSerializerOptions JsonSerializerOptions =
        new()
        {
            PropertyNameCaseInsensitive = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

    public static async Task<T?> DeserializeJsonAsync<T>(this Stream stream)
        where T : class =>
        await JsonSerializer.DeserializeAsync<T>(stream, JsonSerializerOptions);
}