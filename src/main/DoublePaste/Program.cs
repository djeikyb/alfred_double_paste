using System.CommandLine;
using System.CommandLine.Builder;
using System.CommandLine.Hosting;
using System.CommandLine.NamingConventionBinder;
using System.CommandLine.Parsing;
using DoublePaste.Core;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace DoublePaste;

public class Program
{
    static async Task<int> Main(string[] args) => await BuildCommandLine()
        .UseHost(
            Host.CreateDefaultBuilder,
            host => host
                .ConfigureServices(services => services.AddClipboard())
                .ConfigureLogging((_, lb) => lb.SetMinimumLevel(LogLevel.Warning))
        )
        .UseDefaults()
        .Build()
        .InvokeAsync(args);

    private static CommandLineBuilder BuildCommandLine()
    {
        var root = new RootCommand();
        root.Handler = CommandHandler.Create<IHost, IConsole, CancellationToken>(LastTwoForTwitterThreads);
        return new CommandLineBuilder(root);
    }

    private static async Task LastTwoForTwitterThreads(IHost host, IConsole console, CancellationToken ct)
    {
        var clips = host.Services.GetRequiredService<IClipboardService>();
        var scriptFilterDto = await clips.LastTwoForTwitterThreads(ct);
        console.WriteLine(scriptFilterDto.Items[0].Text.Copy);
        // var json = JsonSerializer.Serialize(scriptFilterDto);
        // console.WriteLine(json);
    }
}
