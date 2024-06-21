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
                    template = template.Replace($"{{{key}}}", RandomDataGenerator.GenerateRandomInteger().ToString());
                }
                else if (value.Contains("[deliveryNumber]"))
                {
                    template = template.Replace($"{{{key}}}", RandomDataGenerator.GenerateRandomDeliveryNumber().ToString());
                }
                else if (value.Contains("[quantity]"))
                {
                    template = template.Replace($"{{{key}}}", RandomDataGenerator.GenerateRandomQuantity().ToString());
                }
                else if (value.Contains("[date:past"))
                {
                    var match = Regex.Match(value, @"random\[date:past:(\d+)\]");
                    int maxRangeOfDays = match.Success ? int.Parse(match.Groups[1].Value) : 365;
                    template = template.Replace($"{{{key}}}", RandomDataGenerator.GenerateRandomDate(maxRangeOfDays, RandomDataGenerator.DateType.Past).ToString(templateSetup.DateFormat ?? "yyyy-MM-dd"));
                }
                else if (value.Contains("[date:future"))
                {
                    var match = Regex.Match(value, @"random\[date:future:(\d+)\]");
                    int maxRangeOfDays = match.Success ? int.Parse(match.Groups[1].Value) : 365;
                    template = template.Replace($"{{{key}}}", RandomDataGenerator.GenerateRandomDate(maxRangeOfDays, RandomDataGenerator.DateType.Future).ToString(templateSetup.DateFormat ?? "yyyy-MM-dd"));
                }
                else if (value.Contains("[date:any"))
                {
                    var match = Regex.Match(value, @"random\[date:any:(\d+)\]");
                    int maxRangeOfDays = match.Success ? int.Parse(match.Groups[1].Value) : 365;
                    template = template.Replace($"{{{key}}}", RandomDataGenerator.GenerateRandomDate(maxRangeOfDays, RandomDataGenerator.DateType.Any).ToString(templateSetup.DateFormat ?? "yyyy-MM-dd"));
                }
                else if (value.Contains("[number]"))
                {
                    template = template.Replace($"{{{key}}}", RandomDataGenerator.GenerateRandomNumber().ToString());
                }
                else if (value.Contains("[text]"))
                {
                    template = template.Replace($"{{{key}}}", RandomDataGenerator.GenerateRandomText());
                }
                else if (value.Contains("[decimal"))
                {
                    var match = Regex.Match(value, @"random\[decimal:(\d+),(\d+),(\d+)\]");
                    if (match.Success)
                    {
                        int min = int.Parse(match.Groups[1].Value);
                        int max = int.Parse(match.Groups[2].Value);
                        int decimalPlaces = int.Parse(match.Groups[3].Value);
                        template = template.Replace($"{{{key}}}", RandomDataGenerator.GenerateRandomDecimal(min, max, decimalPlaces).ToString());
                    }
                    else
                    {
                        template = template.Replace($"{{{key}}}", RandomDataGenerator.GenerateRandomDecimal().ToString());
                    }
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
