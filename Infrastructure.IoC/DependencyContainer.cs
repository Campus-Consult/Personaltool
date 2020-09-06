using Microsoft.Extensions.DependencyInjection;
using Personaltool.Application.Interfaces;
using Personaltool.Application.Services;

namespace Personaltool.Infrastructure.IoC
{
    public class DependencyContainer
    {
        public static void RegisterServices(IServiceCollection services)
        {
            services.AddScoped<IPositionService, PositionService>();
        }
    }
}
