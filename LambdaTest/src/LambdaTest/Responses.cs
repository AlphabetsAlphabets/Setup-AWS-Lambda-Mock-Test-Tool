using Newtonsoft.Json;

public class Responses
{
    [JsonProperty("SuccessStatus")]
    public int status { get; set; }

    [JsonProperty("ReturnMessage")]
    public string? returnMessage { get; set; }

    [JsonProperty("ReturnRequestID")]
    public string? returnRequestID { get; set; }
}