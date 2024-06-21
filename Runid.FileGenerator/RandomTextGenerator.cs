namespace Runid.FileGenerator;

public static class RandomTextGenerator
{
    private static Random _random = new Random();

    private static List<string> adjectives = new List<string>
    {
        "Innovative", "Durable", "High-quality", "Compact", "Portable", "Versatile",
        "Reliable", "Efficient", "Stylish", "Affordable"
    };

    private static List<string> materials = new List<string>
    {
        "plastic", "metal", "wood", "glass", "fabric", "ceramic",
        "leather", "rubber", "carbon fiber", "steel"
    };

    private static List<string> productTypes = new List<string>
    {
        "gadget", "device", "tool", "accessory", "appliance", "instrument",
        "equipment", "item", "product", "unit"
    };

    private static List<string> features = new List<string>
    {
        "with advanced technology", "with a sleek design", "with multiple functions",
        "with long battery life", "with high precision", "with user-friendly interface",
        "with enhanced performance", "with great portability", "with easy maintenance", "with customizable settings"
    };

    public static string GenerateRandomDescription()
    {
        string adjective = adjectives[_random.Next(adjectives.Count)];
        string material = materials[_random.Next(materials.Count)];
        string productType = productTypes[_random.Next(productTypes.Count)];
        string feature = features[_random.Next(features.Count)];

        return $"{adjective} {material} {productType} {feature}.";
    }
}
