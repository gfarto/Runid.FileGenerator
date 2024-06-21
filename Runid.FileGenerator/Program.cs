using Runid.FileGenerator;
using Runid.FileGenerator.DataGenerators;

static void ShowMenu()
{
    Console.ResetColor();
    Message.ShowMessageOnGreen("RUNID - Data & Files Generator");
    Console.WriteLine();

    Console.WriteLine("Options: ");
    Console.WriteLine("*****************************");
    Console.WriteLine(" 1. Navigate to a directory");
    Console.WriteLine(" 2. Display current directory");
    Console.WriteLine(" 3. List directories");
    Console.WriteLine(" 4. Create a subdirectory");
    Console.WriteLine(" 5. List files");
    Console.WriteLine(" 6. Open current directory in Explorer");

    Message.ShowMessageOnGreen("Generation of Random DATA files: ");
    Console.WriteLine(" DM. Material Master");
    Console.WriteLine(" DB. Batch Master");
    Console.WriteLine(" DS. Serials");
    Console.WriteLine(" DD. Delivery Numbers");

    Message.ShowMessageOnGreen("Generation of Files based on Templates");
    Console.WriteLine(" R. Run Template");
    Console.WriteLine(" T. Create Template Example File");
    Console.WriteLine(" S. Create 'setup.json' Example File");

    Message.ShowMessageOnGreen("---");
    Console.WriteLine("C. Clear");
    Console.WriteLine("M. Show Menu");
    Console.WriteLine("Q. Exit");

    Console.WriteLine("");
    DirectoryUtils.ShowCurrentDirectory();

Start:
    Console.WriteLine("");
    Console.Write(": ");

    var option = Console.ReadLine() ?? "";
    switch (option.ToLower())
    {
        case "1":
            DirectoryUtils.NavigateAndSelectFolder();
            break;
        case "2":
            DirectoryUtils.ShowCurrentDirectory();
            goto Start;
        case "3":
            DirectoryUtils.ListDirectories();
            goto Start;
        case "4":
            DirectoryUtils.CreateSubdirectory();
            break;
        case "5":
            DirectoryUtils.ListFiles();
            goto Start;
        case "6":
            DirectoryUtils.OpenDirectoryInExplorer();
            goto Start;

        case "dm":
            DataFileGenerator.CreateFile(DataFileGenerator.DataFileType.Material);
            goto Start;
        case "db":
            DataFileGenerator.CreateFile(DataFileGenerator.DataFileType.Batch);
            goto Start;
        case "ds":
            DataFileGenerator.CreateFile(DataFileGenerator.DataFileType.Serial);
            goto Start;
        case "dd":
            DataFileGenerator.CreateFile(DataFileGenerator.DataFileType.Delivery);
            goto Start;

        case "r":
            FileGenerator.GenerateFiles();
            goto Start;
        case "t":
            ExampleFileCreator.CreateTemplateExampleFile();
            goto Start;
        case "s":
            ExampleFileCreator.CreateSetupExampleFile();
            goto Start;

        case "q":
            Environment.Exit(0);
            break;
        case "c":
            Console.Clear();
            break;
        case "m":
            break;
        default:
            Console.WriteLine("Invalid option. Press any key to continue...");
            Console.ReadKey();
            break;
    }
}

do
{
    ShowMenu();
} while (true);
