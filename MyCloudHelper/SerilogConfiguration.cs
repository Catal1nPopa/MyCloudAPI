using Microsoft.Extensions.Hosting;
using Serilog;

namespace MyCloudHelper
{
    public static class SerilogConfiguration
    {
        public static void ConfigureSerilog(this IHostBuilder hostBuilder)
        {
            var logPath = Path.Combine(AppContext.BaseDirectory, "Logs", "log-mycloud-.txt");

            hostBuilder.UseSerilog((context, services, configuration) =>
            {
                configuration.ReadFrom.Configuration(context.Configuration);
            });
        }
    }
}
