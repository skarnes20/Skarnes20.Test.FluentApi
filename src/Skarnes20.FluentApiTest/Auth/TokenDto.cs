namespace Skarnes20.FluentApiTest.Auth;

public class TokenDto
{
    [JsonProperty("expires_on")]
    [JsonConverter(typeof(UnixDateTimeConverter))]
    public DateTime ExpiresOn { get; set; }

    [JsonProperty("access_token")]
    public string AccessToken { get; set; } = string.Empty;
}