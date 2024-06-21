using System.Text.RegularExpressions;

namespace Runid.FileGenerator;

public static class TemplateReplacer
{
    public static string ReplaceKeys(string template, TemplateSetup templateSetup, Dictionary<string, List<string[]>> dataFiles)
    {
        var usedIndices = new Dictionary<string, int>();

        foreach (var rule in templateSetup.Rules)
        {
            string key = rule.Key;
            string value = rule.Value;

            if (value.StartsWith("random"))
            {
                if (value.Contains("[int]"))
                {
                    template = template.Replace($"{{{key}}}", RandomDataGenerator.GenerateRandomQuantity().ToString());
                }
                else if (value.Contains("[date:past]"))
                {
                    template = template.Replace($"{{{key}}}", RandomDataGenerator.GenerateRandomDate(365, RandomDataGenerator.DateType.Past).ToString(templateSetup.DateFormat ?? "yyyy-MM-dd"));
                }
                else if (value.Contains("[date:future]"))
                {
                    template = template.Replace($"{{{key}}}", RandomDataGenerator.GenerateRandomDate(365, RandomDataGenerator.DateType.Future).ToString(templateSetup.DateFormat ?? "yyyy-MM-dd"));
                }
                else if (value.Contains("[date:any]"))
                {
                    template = template.Replace($"{{{key}}}", RandomDataGenerator.GenerateRandomDate(365, RandomDataGenerator.DateType.Any).ToString(templateSetup.DateFormat ?? "yyyy-MM-dd"));
                }
                else if (value.Contains("[number]"))
                {
                    template = template.Replace($"{{{key}}}", RandomDataGenerator.GenerateRandomNumber().ToString());
                }
                else if (value.Contains("[text]"))
                {
                    template = template.Replace($"{{{key}}}", RandomDataGenerator.GenerateRandomText());
                }
            }
            else if (value.StartsWith("file"))
            {
                var match = Regex.Match(value, @"file\[(.+):(\d+)\]");
                if (match.Success)
                {
                    string fileKey = match.Groups[1].Value;
                    int colIndex = int.Parse(match.Groups[2].Value);

                    if (dataFiles.ContainsKey(fileKey))
                    {
                        var data = dataFiles[fileKey];

                        if (data.Count == 0)
                        {
                            template = template.Replace($"{{{key}}}", string.Empty);
                            continue;
                        }

                        // Use the same index if already used, else generate a new one
                        int currentIndex;
                        if (usedIndices.ContainsKey(fileKey))
                        {
                            currentIndex = usedIndices[fileKey];
                        }
                        else
                        {
                            currentIndex = RandomDataGenerator.GenerateRandomNumber() % data.Count;
                            usedIndices[fileKey] = currentIndex;
                        }

                        string randomValue = data[currentIndex][colIndex];
                        template = template.Replace($"{{{key}}}", randomValue);
                    }
                }
            }
        }

        return template;
    }
}
