using System.Text.RegularExpressions;

namespace Runid.FileGenerator;

public class FileGenerator
{
    public static void GenerateFiles()
    {
        string? templatePath = GetTemplatePath();
        if (string.IsNullOrWhiteSpace(templatePath))
        {
            Message.ShowError("Template file not found.");
            Console.WriteLine("Press any key to continue...");
            return;
        }

        string setupPath = "setup.json";
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), DateTime.Now.ToString("yyyyMMdd_HHmm"));

        Console.WriteLine("Enter the number of files to generate:");
        Console.Write(": ");
        var numFiles = Console.ReadLine();
        if (!int.TryParse(numFiles, out int numberOfFiles))
        {
            Message.ShowError("Invalid number of files.");
            return;
        }

        GenerateFiles(templatePath, setupPath, outputPath, numberOfFiles);
    }

    private static string? GetTemplatePath()
    {
        var files = Directory.GetFiles(Directory.GetCurrentDirectory(), "template.*");
        if (files.Length == 0)
        {
            Message.ShowError("Template file not found.");
            return null;
        }

        if (files.Length == 1)
        {
            return files[0];
        }

        Console.WriteLine("Multiple template files found. Please select one:");
        for (int i = 0; i < files.Length; i++)
        {
            Console.WriteLine($"{i + 1}. {files[i]}");
        }

        Console.Write(": ");
        var input = Console.ReadLine();
        if (!int.TryParse(input, out int index) || index < 1 || index > files.Length)
        {
            Message.ShowError("Invalid selection.");
            return null;
        }

        return files[index - 1];
    }

    private static void GenerateFiles(string templatePath, string setupPath, string outputPath, int numberOfFiles)
    {
        string template = File.ReadAllText(templatePath);
        var setup = SetupLoader.LoadSetup(setupPath);

        if (!Directory.Exists(outputPath))
            Directory.CreateDirectory(outputPath);

        // Load data files
        var dataFiles = new Dictionary<string, List<string[]>>();

        if (setup == null)
        {
            Message.ShowError("Invalid setup file.");
            return;
        }

        if (setup.Rules == null)
        {
            Message.ShowError("Invalid setup file.");
            return;
        }

        foreach (var rule in setup.Rules)
        {
            if (rule.Value.StartsWith("file"))
            {
                var match = Regex.Match(rule.Value, @"file\[(.+):(\d+)\]");
                if (match.Success)
                {
                    string fileKey = match.Groups[1].Value;
                    if (!dataFiles.ContainsKey(fileKey))
                    {
                        dataFiles[fileKey] = DataLoader.LoadData($"{fileKey}.txt");
                    }
                }
            }
        }

        // Generate files
        for (int i = 0; i < numberOfFiles; i++)
        {
            string filledTemplate = TemplateReplacer.ReplaceKeys(template, setup, dataFiles);
            string filename = GenerateFileName(setup.NameConvention ?? "file-{date}_{time}_{sequence}.xml", i + 1);
            File.WriteAllText(Path.Combine(outputPath, filename), filledTemplate);

            Console.WriteLine($"File {i + 1} generated: {filename}");
        }
    }

    private static string GenerateFileName(string nameConvention, int sequence)
    {
        string currentDate = DateTime.Now.ToString("yyyyMMdd");
        string currentTime = DateTime.Now.ToString("HHmmssffffff");

        string filename = nameConvention
            .Replace("{date}", currentDate)
            .Replace("{time}", currentTime)
            .Replace("{sequence}", sequence.ToString());

        return filename;
    }
}
