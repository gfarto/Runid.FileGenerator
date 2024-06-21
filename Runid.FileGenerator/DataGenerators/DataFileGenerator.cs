namespace Runid.FileGenerator.DataGenerators;

public static class DataFileGenerator
{
    public enum DataFileType
    {
        Material,
        Delivery,
        Batch,
        Serial
    }

    private static string GetFileTypeName(DataFileType fileType)
    {
        return fileType switch
        {
            DataFileType.Material => "materials",
            DataFileType.Delivery => "deliveries",
            DataFileType.Batch => "batches",
            DataFileType.Serial => "serials",
            _ => string.Empty
        };
    }

    public static void CreateFile(DataFileType dataFileType)
    {
        string fileTypeName = GetFileTypeName(dataFileType);
        if (string.IsNullOrWhiteSpace(fileTypeName))
        {
            Message.ShowError("Invalid file type.");
            return;
        }

        Console.WriteLine("Enter the number of items to generate: ");
        int numberOfItems = int.Parse(Console.ReadLine() ?? "0");

        var currentDirectory = Directory.GetCurrentDirectory();
        var dataDirectory = Path.Combine(currentDirectory, "data");
        var filePath = Path.Combine(dataDirectory, $"{fileTypeName}.txt");
        bool appendData = false;

        if (File.Exists(filePath))
        {
            Message.ShowWarning("File already exists.", true);
            Console.WriteLine("What do you want to do?");
            Console.WriteLine(" W - Overwrite the file");
            Console.WriteLine(" A - Append to the file");
            Console.WriteLine(" R - Rename previous file");
            Console.WriteLine(" N - Cancel");
            Console.Write(": ");
            var response = Console.ReadLine()?.ToLower() ?? "n";

            switch (response)
            {
                case "w":
                    File.Delete(filePath);
                    break;
                case "a":
                    appendData = true;
                    break;
                case "r":
                    string newFileName = Path.Combine(dataDirectory, $"{fileTypeName}_{DateTime.Now:yyyyMMddHHmmss}.txt");
                    File.Move(filePath, newFileName);
                    break;
                case "n":
                    return;
                default:
                    Console.WriteLine("Invalid response. Press any key to return main menu...");
                    Console.ReadKey();
                    return;
            }
        }

        if (!Directory.Exists(dataDirectory))
            Directory.CreateDirectory(dataDirectory);

        IDataGenerator dataGenerator = dataFileType switch
        {
            DataFileType.Material => new MaterialDataGenerator(),
            DataFileType.Delivery => new DeliveryNumbersGenerator(),
            DataFileType.Batch => new BatchDataGenerator(),
            DataFileType.Serial => new SerialsDataGenerator(),
            _ => throw new ArgumentOutOfRangeException()
        };

        dataGenerator.GenerateData(filePath, numberOfItems, appendData);
    }
}
