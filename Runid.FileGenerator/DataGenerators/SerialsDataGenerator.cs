namespace Runid.FileGenerator.DataGenerators;

public class SerialsDataGenerator : DataGenerator
{
    public override void GenerateData(string filePath, int numberOfItems, bool appendItemsToFileContent = false)
    {
        // List to hold generated material data
        var data = new List<string>();

        for (int i = 0; i < numberOfItems; i++)
        {
            string serial = RandomDataGenerator.GenerateRandomSerialNumber();
            data.Add($"{serial}");
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
