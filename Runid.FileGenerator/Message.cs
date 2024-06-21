namespace Runid.FileGenerator;

public static class Message
{
    private static void ShowMessage(string message, int beeps = 0, ConsoleColor? foregroundColor = null, ConsoleColor? backgroundColor = null)
    {
        if (foregroundColor != null)
            Console.ForegroundColor = foregroundColor.Value;

        if (backgroundColor != null)
            Console.BackgroundColor = backgroundColor.Value;

        Console.WriteLine(message);

        for (int i = 0; i < beeps; i++)
            Console.Beep();

        Console.ResetColor();
    }

    public static void ShowMessageOnBlue(string message)
    {
        ShowMessage(message, 0, ConsoleColor.Blue, ConsoleColor.Black);
    }

    public static void ShowMessageOnGreen(string message)
    {
        ShowMessage(message, 0, ConsoleColor.Green, ConsoleColor.Black);
    }

    public static void ShowWarning(string message, bool beep = true)
    {
        ShowMessage(message, beep ? 1 : 0, ConsoleColor.Yellow, ConsoleColor.Black);
    }

    public static void ShowError(string message)
    {
        ShowMessage(message, 2, ConsoleColor.Black, ConsoleColor.Red);
    }

}
