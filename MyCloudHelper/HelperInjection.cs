using Microsoft.Extensions.DependencyInjection;

namespace MyCloudHelper
{
    public static class HelperInjection
    {
        public static IServiceCollection AddHelperServices(this IServiceCollection Services)
        {
            Services.AddSingleton<FileEncryptionService>();
            return Services;
        }
    }
}
