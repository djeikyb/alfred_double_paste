using Microsoft.Extensions.DependencyInjection;

namespace DoublePaste.Core;

public static class StartupExtensions
{
    public static IServiceCollection AddClipboard(this IServiceCollection services)
    {
        return services
            .AddScoped(typeof(IRepository<>), typeof(ClipboardRepository<>))
            .AddDbContext<ReadOnlyClipboardContext>()
            .AddScoped<IClipboardService, ClipboardService>()
            ;
    }
}
