using Fukicycle.Tool.AppBase.Store;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fukicycle.Tool.AppBase
{
    public static class AppBaseWebAssemblyExtension
    {
        public static IServiceCollection AddAppBase(this IServiceCollection services)
        {
            services.AddScoped<IStateContainer, StateContainer>();
            return services;
        }
    }
}
