using System.Text.Json.Serialization;

namespace Runid.FileGenerator;

public class TemplateSetup
{
    [JsonPropertyName("rules")]
    public Dictionary<string, string> Rules { get; set; } = new Dictionary<string, string>();

    [JsonPropertyName("name_convention")]
    public string? NameConvention { get; set; }

    [JsonPropertyName("date_format")]
    public string? DateFormat { get; set; }

    [JsonPropertyName("time_format")]
    public string? TimeFormat { get; set; }
}
