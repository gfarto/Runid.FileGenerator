namespace Runid.FileGenerator.DataGenerators;

public class BatchDataGenerator : DataGenerator
{
    public override void GenerateData(string filePath, int numberOfItems, bool appendItemsToFileContent = false)
    {
        var dateFormats = new Dictionary<string, string>
    {
        { "1", "dd/MM/yyyy" },
        { "2", "MM/dd/yyyy" },
        { "3", "yyyy-MM-dd" },
        { "4", "yyyy.MM.dd" },
        { "5", "yyyyMMdd" },
    };
        Console.WriteLine("Chose the date format: ");
        foreach (var (key, value) in dateFormats)
        {
            Console.WriteLine($" {key}. {value}");
        }
        Console.Write(": ");
        var dateFormatSelection = Console.ReadLine() ?? "1";
        var dateFormat = dateFormats.ContainsKey(dateFormatSelection) ? dateFormats[dateFormatSelection] : "dd/MM/yyyy";


        // List to hold generated material data
        var data = new List<string>();

        for (int i = 0; i < numberOfItems; i++)
        {
            string batchNumber = RandomDataGenerator.GenerateRandomBatchNumber("B");
            string manufactureDate = RandomDataGenerator.GenerateRandomDate(365, RandomDataGenerator.DateType.Past).ToString(dateFormat);
            string expiryDate = RandomDataGenerator.GenerateRandomDate(365 * 3, RandomDataGenerator.DateType.Future).ToString(dateFormat);

            data.Add($"{batchNumber}|{manufactureDate}|{expiryDate}");
        }

        if (appendItemsToFileContent && File.Exists(filePath))
        {
            var existingData = File.ReadAllLines(filePath).ToList();
            existingData.AddRange(data);
            data = existingData;
        }

        // Write the material data to the file
        File.WriteAllLines(filePath, data);

        Message.ShowMessageOnGreen($"File {(appendItemsToFileContent ? "updated" : "created")} at: {filePath}.");
    }
}
