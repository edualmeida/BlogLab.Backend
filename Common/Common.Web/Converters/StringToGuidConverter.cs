using System.Text.Json;
using System.Text.Json.Serialization;

namespace Common.Web.Converters;

public class StringToGuidConverter : JsonConverter<Guid>
{
    public override Guid Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        // Handle if the value is a valid GUID string
        if (reader.TokenType == JsonTokenType.String)
        {
            var stringValue = reader.GetString()?.ToUpperInvariant();
            if (Guid.TryParse(stringValue, out Guid result))
            {
                return result;
            }
            else
            {
                // Handle invalid GUID format (you could throw an exception or return a default GUID)
                throw new JsonException($"Invalid GUID format: {stringValue}");
            }
        }
        
        // If not a string, throw an exception (or handle differently based on needs)
        throw new JsonException("Expected string for GUID conversion.");
    }

    public override void Write(Utf8JsonWriter writer, Guid value, JsonSerializerOptions options)
    {
        // Write the GUID as a string
        writer.WriteStringValue(value.ToString());
    }
}