namespace Client.seo;

public class MetadataProvider
{
    public Dictionary<string, MetadataValue> RouteDetailMapping { get; set; } = new()
    {
        {
            "/",
            new()
            {
                Title = "Banks and Apps On UPI in Nepal",
                Description = "List of banks and apps supporting UPI in Nepal"
            }
        },
        {
            "/apps",
            new()
            {
                Title = "Banks Supporting UPI",
                Description = "List of apps supporting UPI in Nepal"
            }
        },
        {
            "/banks",
            new()
            {
                Title = "Apps Supporting UPI",
                Description = "List of Banks supporting UPI in Nepal"
            }
        }
    };
}