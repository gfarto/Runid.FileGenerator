namespace Runid.FileGenerator;

public class ExampleFileCreator
{
    private static void CreateExampleFile(string content, string filename)
    {
        var path = Path.Combine(Directory.GetCurrentDirectory(), filename);
        File.WriteAllText(path, content);

        Message.ShowMessageOnGreen($"Example file '{filename}' created.");
    }

    public static void CreateTemplateExampleFile()
    {
        string content = @"<InboundDelivery>
            <DeliveryNumber>{DeliveryNumber}</DeliveryNumber>
            <Material>{Material}</Material>
            <Quantity>{Quantity}</Quantity>
            <Description>{Description}</Description>
            <DeliveryDate>{DeliveryDate}</DeliveryDate>
        </InboundDelivery>";

        CreateExampleFile(content, "template.xml");
    }

    public static void CreateSetupExampleFile()
    {
        string content = @"{
            ""rules"": {
            ""DeliveryNumber"": ""random[int]"",
            ""DeliveryDate"": ""random[date:past]"",
            ""Quantity"": ""random[number]"",
            ""Description"": ""random[text]"",
            ""Material"": ""file[materials:0]"",
            ""MaterialDescription"": ""file[materials:1]"",
            ""BatchNumber"": ""file[batches:0]"",
            ""BatchCreationDate"": ""random[date:past]"",
            ""BatchExpiryDate"": ""random[date:future]""
          },
          ""name_convention"": ""biec_to_ilmsxp-IDB-{date}_{time}_{sequence}.xml"",
          ""date_format"": ""yyyyMMdd"",
          ""time_format"": ""HHmmss""
        }";

        CreateExampleFile(content, "setup.json");
    }
}
