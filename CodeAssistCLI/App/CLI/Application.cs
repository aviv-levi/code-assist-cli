using CodeAssistCLI.App.CommandHandlers;
using CodeAssistCLI.App.Commands;
using CodeAssistCLI.Core.Services;
using CommandLine;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Yaml;

namespace CodeAssistCLI.App.CLI;

public static class Application
{
    public static void Run(string[] args)
    {
        // Set up dependency injection
        var serviceProvider = ConfigureServices();

        // Resolve handlers
        var analyzeCommandHandler = serviceProvider.GetRequiredService<AnalyzeCommandHandler>();
        // var debugCommandHandler = serviceProvider.GetRequiredService<DebugCommandHandler>();
        //
        // Parser.Default.ParseArguments<AnalyzeCommand, DebugCommand>(args).MapResult(
        //     (AnalyzeCommand opts) => analyzeCommandHandler.HandleCommand(opts).Result,
        //     (DebugCommand opts) => debugCommandHandler.HandleCommand(opts),
        //     errs => 1
        // );
    }

    private static ServiceProvider ConfigureServices()
    {
        var services = new ServiceCollection();

        var configurationPath = Path.Join(Directory.GetCurrentDirectory(), "Configuration", "Configuration.yaml");

        // Load configuration
        var configuration = new ConfigurationBuilder()
            .AddYamlFile(configurationPath)
            .AddEnvironmentVariables()
            .Build();

        services.AddSingleton<IConfiguration>(configuration);

        // Register services
        services.AddSingleton<GptService>(sp =>
        {
            var config = sp.GetRequiredService<IConfiguration>();
            var apiKey = config["OpenAI:ApiKey"] ?? throw new ArgumentException("API Key is missing.");
            return new GptService(new HttpClient(), apiKey);
        });
        services.AddSingleton<CodeAnalysisService>();

        // Register command handlers
        services.AddSingleton<AnalyzeCommandHandler>();
        // services.AddSingleton<DebugCommandHandler>();

        // Build service provider
        return services.BuildServiceProvider();
    }
}
