using Microsoft.Extensions.DependencyInjection;
using TechyGirlsTrivia.Models.Storage;

namespace TechyGirlsTrivia.WebAPI
{
    public static class Injector
    {
        public static void Register(this IServiceCollection services)
        {
            services.AddTransient<IDataAccess, DataAccess>();
            services.AddTransient<IStorageManager, StorageManager>();
        }
    }
}
