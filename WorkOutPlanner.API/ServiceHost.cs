namespace WorkoutPlanner.API;

public static class ServiceHost
{
    public static ConfigurationManager AddSecretsJson(this ConfigurationManager configuration)
    {
        configuration.AddJsonFile("appsettings.Secrets.json", optional: true, reloadOnChange: true);

        return configuration;
    }
}