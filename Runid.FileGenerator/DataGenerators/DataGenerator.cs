namespace Runid.FileGenerator.DataGenerators;

public abstract class DataGenerator : IDataGenerator
{
    public abstract void GenerateData(string filePath, int numberOfItems, bool appendItemsToFileContent = false);
}
