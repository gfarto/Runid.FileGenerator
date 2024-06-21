using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Runid.FileGenerator;

public static class DirectoryUtils
{
    public static void NavigateAndSelectFolder()
    {
        try
        {
            string currentPath = Directory.GetCurrentDirectory();
            while (true)
            {
                string[] directories = Directory.GetDirectories(currentPath);
                ListDirectories(currentPath, true, directories);

                Console.WriteLine();
                Console.WriteLine("---");
                Console.WriteLine(" Enter a number to navigate to a subdirectory");
                Console.WriteLine(" '..' to go up");
                Console.WriteLine(" Type the folder name directly or part of the name ");
                Console.WriteLine("---");
                Console.WriteLine(" 's' to select the current directory and return to main menu");


                string input = Console.ReadLine() ?? "";

                if (input == "..")
                {
                    currentPath = Directory.GetParent(currentPath)?.FullName ?? currentPath;
                }
                else if (input == "s")
                {
                    Directory.SetCurrentDirectory(currentPath);
                    return;
                    // return currentPath;
                }
                else if (int.TryParse(input, out int index) && index > 0 && index <= directories.Length)
                {
                    currentPath = directories[index - 1];
                }
                else
                {
                    string? matchedDirectory = FindMatchingDirectory(currentPath, input);
                    if (matchedDirectory != null)
                    {
                        currentPath = matchedDirectory;
                    }
                    else
                    {
                        Message.ShowWarning("Invalid input or directory does not exist. Press any key to continue...");
                        Console.ReadKey();
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Message.ShowError(ex.Message);
        }
    }

    public static void ShowCurrentDirectory(string? currentPath = null)
    {
        Console.WriteLine("Current Directory: ");
        Message.ShowMessageOnBlue(currentPath ?? Directory.GetCurrentDirectory());
    }

    public static void ListDirectories(string? currentPath = null, bool showCurrentDirectory = false, string[]? directories = null)
    {
        if (currentPath == null)
            currentPath = Directory.GetCurrentDirectory();

        if (showCurrentDirectory)
            ShowCurrentDirectory(currentPath);

        if (directories == null)
            directories = Directory.GetDirectories(currentPath);

        Console.WriteLine("Subdirectories:");

        if (directories.Length == 0)
        {
            Message.ShowWarning("No subdirectories found.");
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Green;
            for (int i = 0; i < directories.Length; i++)
            {
                Console.WriteLine($"{i + 1}. {Path.GetFileName(directories[i])}");
            }
            Console.ResetColor();
        }
    }

    public static void ListFiles(string? currentPath = null, bool showCurrentDirectory = false, string[]? files = null)
    {
        if (currentPath == null)
            currentPath = Directory.GetCurrentDirectory();

        if (showCurrentDirectory)
            ShowCurrentDirectory(currentPath);

        if (files == null)
            files = Directory.GetFiles(currentPath);

        Console.WriteLine("Files:");

        if (files.Length == 0)
        {
            Message.ShowWarning("No files found.");
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Green;
            for (int i = 0; i < files.Length; i++)
            {
                Console.WriteLine($"{i + 1}. {Path.GetFileName(files[i])}");
            }
            Console.ResetColor();
        }
    }

    public static void CreateSubdirectory(string? parentDirectory = null)
    {
        if (string.IsNullOrWhiteSpace(parentDirectory))
        {
            parentDirectory = Directory.GetCurrentDirectory();
        }

        Console.WriteLine("Enter the name of the new subdirectory: ");
        string? subdirectoryName = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(subdirectoryName))
        {
            Message.ShowWarning("Subdirectory name cannot be empty. Press any key to continue...");
            Console.ReadKey();
            return;
        }

        string newSubdirectoryPath = Path.Combine(parentDirectory, subdirectoryName);

        try
        {
            Directory.CreateDirectory(newSubdirectoryPath);
            Console.WriteLine($"Subdirectory '{subdirectoryName}' created successfully. Press any key to continue...");
            Console.ReadKey();
        }
        catch (Exception ex)
        {
            Message.ShowError($"Failed to create subdirectory: {ex.Message}");
            Console.WriteLine($"Press any key to continue...");
            Console.ReadKey();
        }
    }

    private static string? FindMatchingDirectory(string currentPath, string input)
    {
        string[] directories = Directory.GetDirectories(currentPath);
        string[] matches = directories
            .Where(d => Path.GetFileName(d).IndexOf(input, StringComparison.OrdinalIgnoreCase) >= 0)
            .ToArray();

        if (matches.Length == 1)
        {
            return matches[0];
        }
        else if (matches.Length > 1)
        {
            Message.ShowWarning("\nDid you mean:");
            for (int i = 0; i < matches.Length; i++)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"{i + 1}. {Path.GetFileName(matches[i])}");
                Console.ResetColor();
            }

            Console.WriteLine("\nEnter the number of the directory to navigate to it:");
            string choice = Console.ReadLine() ?? "";

            if (int.TryParse(choice, out int selectedIndex) && selectedIndex > 0 && selectedIndex <= matches.Length)
            {
                return matches[selectedIndex - 1];
            }
            else
            {
                Message.ShowWarning("Invalid selection. Press any key to continue...");
                Console.ReadKey();
            }
        }

        return null;
    }


    public static void OpenDirectoryInExplorer(string? directoryPath = null)
    {
        if (string.IsNullOrWhiteSpace(directoryPath))
        {
            directoryPath = Directory.GetCurrentDirectory();
        }

        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            Process.Start(new ProcessStartInfo()
            {
                FileName = directoryPath,
                UseShellExecute = true,
                Verb = "open"
            });
        }
        else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
        {
            Process.Start("xdg-open", directoryPath);
        }
        else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
        {
            Process.Start("open", directoryPath);
        }
        else
        {
            Console.WriteLine("Unsupported operating system.");
        }
    }
}
