using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechyGirlsTrivia.WebAPI.Storage;

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
