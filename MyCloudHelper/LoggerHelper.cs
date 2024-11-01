using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCloudHelper
{
    public static class LoggerHelper
    {
        public static void LogInformation(string message)
        {
            Log.Information(message);
        }

        public static void LogWarning(string message)
        {
            Log.Warning(message);
        }

        public static void LogError(Exception ex, string message)
        {
            Log.Error(ex, message);
        }

        public static void LogDebug(string message)
        {
            Log.Debug(message);
        }
    }

}
