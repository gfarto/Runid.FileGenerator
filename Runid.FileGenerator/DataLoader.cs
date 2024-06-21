namespace Runid.FileGenerator;

public static class DataLoader
{
    public static List<string[]> LoadData(string filePath)
    {
        var currentDirectory = Directory.GetCurrentDirectory();
        var dataDirectory = Path.Combine(currentDirectory, "data");
        var fileFullPath = Path.Combine(dataDirectory, $"{filePath}");

        if (!File.Exists(fileFullPath))
        {
            Message.ShowError("File not found.");
            return [];
        }

        var data = new List<string[]>();
        foreach (var line in File.ReadLines(fileFullPath))
        {
            data.Add(line.Split('|'));
        }
        return data;
    }
}
