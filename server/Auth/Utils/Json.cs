using System.Text.Json;

namespace Auth.Utils
{
    public static class Json
    {
        public static async Task<string> Write<T>(T obj)
        {
            await using MemoryStream stream = new MemoryStream();
            using var reader = new StreamReader(stream);
            await JsonSerializer.SerializeAsync(stream, obj);
            stream.Position = 0;
            return await reader.ReadToEndAsync();
        }
    }
}
