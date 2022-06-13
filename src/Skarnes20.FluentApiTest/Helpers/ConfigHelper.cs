namespace Skarnes20.FluentApiTest.Helpers;

public static class ConfigHelper
{
    private static IConfigurationRoot? _configuration;

    public static IConfigurationRoot GetConfiguration(string configFile = "testconfig.json")
    {
        if (_configuration == null)
        {
            var jsonConfig = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile(configFile, optional: true, reloadOnChange: true)
                .Build();

            var config = new ConfigurationBuilder()
                .AddConfiguration(jsonConfig)
                .AddAzureKeyVault(new Uri(jsonConfig["AzureKeyVaultUrl"]),
                    new DefaultAzureCredential());

            _configuration = config.Build();
        }

        return _configuration;
    }
}