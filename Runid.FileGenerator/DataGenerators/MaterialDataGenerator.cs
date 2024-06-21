namespace Runid.FileGenerator.DataGenerators;

public class MaterialDataGenerator : DataGenerator
{
    public override void GenerateData(string filePath, int numberOfItems, bool appendItemsToFileContent = false)
    {
        // List to hold generated material data
        var data = new List<string>();

        for (int i = 0; i < numberOfItems; i++)
        {
            string materialCode = RandomDataGenerator.GenerateRandomMaterialNumber("M");
            string materialDescription = RandomTextGenerator.GenerateRandomDescription();
            data.Add($"{materialCode}|{materialDescription}");
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
