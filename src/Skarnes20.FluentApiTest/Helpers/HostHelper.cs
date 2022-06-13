namespace Skarnes20.FluentApiTest.Helpers;

public static class HostHelper
{
    private static IHost? _host;

    public static IHost CreateHost(IConfiguration configuration)
    {
        return _host ??= new HostBuilder()
            .ConfigureServices(services =>
            {
                services.AddHttpClient("Api",
                    client =>
                    {
                        client.BaseAddress = new Uri(configuration.GetValue<string>("ApiUrl"));
                    });
                services.AddHttpClient("Token");
            }).Build();
    }
}