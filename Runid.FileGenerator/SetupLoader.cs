using System.Text.Json;

namespace Runid.FileGenerator;

public class SetupLoader
{
    public static TemplateSetup? LoadSetup(string setupFilePath)
    {
        string json = File.ReadAllText(setupFilePath);
        return JsonSerializer.Deserialize<TemplateSetup>(json);
    }
}
