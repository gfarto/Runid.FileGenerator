namespace Runid.FileGenerator;

public static class RandomDataGenerator
{
    private static Random _random = new Random();

    public static int GenerateRandomNumber(int minValue = 100000, int maxValue = 999999)
    {
        return _random.Next(minValue, maxValue);
    }

    public static string GenerateRandomNumber(string prefix, int minValue = 100000, int maxValue = 999999)
    {
        if (!string.IsNullOrWhiteSpace(prefix))
            return $"{prefix}{GenerateRandomNumber(minValue, maxValue)}";

        return GenerateRandomNumber(minValue, maxValue).ToString();
    }

    public static int GenerateRandomQuantity()
    {
        return GenerateRandomNumber(1, 1000);
    }

    public static string GenerateRandomDeliveryNumber(string? prefix = "D")
    {
        return GenerateRandomNumber(prefix ?? "", 1000000, 9999999);
    }

    public static string GenerateRandomBatchNumber(string? prefix = "B")
    {
        return GenerateRandomNumber(prefix ?? "", 1000, 9999);
    }

    public static string GenerateRandomMaterialNumber(string? prefix = "MAT")
    {
        return GenerateRandomNumber(prefix ?? "", 100, 999);
    }

    public static string GenerateRandomSerialNumber(string? prefix = "S")
    {
        return GenerateRandomNumber(prefix ?? "", 100000000, 999999999);
    }

    public static string GenerateRandomText()
    {
        return "Random Description " + _random.Next(1, 1000);
    }

    public static DateTime GenerateRandomDate(int maxRangeOfDays = 365, DateType dateType = DateType.Past)
    {
        if (dateType == DateType.Past)
            return DateTime.Now.AddDays(_random.Next(maxRangeOfDays * -1, 0));

        if (dateType == DateType.Future)
            return DateTime.Now.AddDays(_random.Next(0, maxRangeOfDays));

        return DateTime.Now.AddDays(_random.Next(maxRangeOfDays * -1, maxRangeOfDays));
    }

    public enum DateType
    {
        Past,
        Future,
        Any
    }
}
