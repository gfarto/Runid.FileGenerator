namespace Runid.FileGenerator.DataGenerators;

public interface IDataGenerator
{
    void GenerateData(string filePath, int numberOfItems, bool appendItemsToFileContent = false);
}
