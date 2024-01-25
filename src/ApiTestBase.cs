namespace Skarnes20.Test.FluentApi;

public class ApiTestBase<T> : Fluent<T> where T : Fluent<T>
{
    public HttpResponseMessage ResponseMessage { get; set; }

    public IConfiguration Configuration { get; }

    public HttpClient HttpClient { get; set; }

    public ApiTestBase()
    {
        ResponseMessage = new HttpResponseMessage();
        Configuration = ConfigHelper.GetConfiguration();

        var host = HostHelper.CreateHost(Configuration);

        var clientFactory = host.Services.GetService<IHttpClientFactory>()!;
        HttpClient = clientFactory.CreateClient("Api");
    }

    public async Task ApiGetAsync(string url)
    {
        ResponseMessage = await HttpClient.GetAsync(url);
    }

    public async Task<TContent?> ApiGetContentAsync<TContent>()
    {
        var content = await ResponseMessage.Content.ReadAsStringAsync();
        if (ResponseMessage.IsSuccessStatusCode)
        {
            return JsonConvert.DeserializeObject<TContent>(content);
        }

        throw new Exception($"Invalid content {content}, returned response {ResponseMessage.StatusCode}");
    }

    public async Task IsLoggedIn()
    {
        await HttpClient.SetBearerToken();
    }

    public void IsNotLoggedIn()
    {
        HttpClient.DefaultRequestHeaders.Clear();
    }

    public void VerifyResponseCode(HttpStatusCode statusCode)
    {
        if (ResponseMessage.StatusCode != statusCode)
        {
            throw new Exception($"Invalid response, expected {statusCode}, got {ResponseMessage.StatusCode}");
        }
    }
}