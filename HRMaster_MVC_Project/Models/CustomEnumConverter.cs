using System.Text.Json;
using System.Text.Json.Serialization;

namespace HRMaster_MVC_Project.Models
{
    public class CustomEnumConverter<T> : JsonConverter<T> where T : struct, Enum
    {
        public override T Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var value = reader.GetString();
            if (Enum.TryParse<T>(value, out var enumValue))
            {
                return enumValue;
            }

            throw new JsonException($"Unknown value {value} for enum {typeof(T)}");
        }

        public override void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString());
        }
    }
}
