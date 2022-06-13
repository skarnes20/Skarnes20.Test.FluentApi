﻿namespace Skarnes20.FluentApiTest.Auth;

public static class AdTokenProvider
{
    private static TokenDto _tokenDto = new();

    public static async Task SetBearerToken(this HttpClient client)
    {
        var token = await GetAccessTokenAsync();
        client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
    }

    private static async Task<string> GetAccessTokenAsync()
    {
        if (!TokenIsValid())
        {
            var config = ConfigHelper.GetConfiguration().GetSection("AzureAd");
            var tokenUrl = config.GetValue<string>("TokenUrl");

            var form = new Dictionary<string, string>
            {
                { "grant_type", "client_credentials" },
                { "client_id", config.GetValue<string>("ClientId")},
                { "client_secret", config.GetValue<string>("ClientSecret") },
                { "scope", config.GetValue<string>("Scope")}
            };

            var httpClient = new HttpClient();
            var message = await httpClient.PostAsync(tokenUrl, new FormUrlEncodedContent(form));
            if (message.IsSuccessStatusCode)
            {
                var response = await message.Content.ReadAsStringAsync();
                _tokenDto = JsonConvert.DeserializeObject<TokenDto>(response) ?? new TokenDto();
            }
            else
            {
                throw new Exception("Ups, something went wrong when trying to get an access token");
            }
        }

        return _tokenDto.AccessToken;
    }

    private static bool TokenIsValid() => DateTime.UtcNow.CompareTo(_tokenDto.ExpiresOn) < 0;
}