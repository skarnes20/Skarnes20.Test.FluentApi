namespace Skarnes20.Test.FluentApi.Helpers;

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
                        client.BaseAddress = new Uri(configuration.GetValue<string>("ApiUrl")??string.Empty);
                    });
                services.AddHttpClient("Token");
            }).Build();
    }
}