using System.Reflection;
using Microsoft.AspNetCore.Builder;
using MicroShopPOC.Extensions.Endpoints.Abstractions;

namespace MicroShopPOC.Extensions.Endpoints
{
    public static class EndpointsRegister
    {
        public static void RegisterEndpoints(this WebApplication app, Assembly assembly)
        {
            var mappers = assembly
                            .GetTypes()
                            .Where(t => typeof(IEndpointMapper).IsAssignableFrom(t) && !t.IsInterface)
                            .Select(Activator.CreateInstance)
                            .Cast<IEndpointMapper>();

            foreach (var mapper in mappers)
                mapper.Map(app);
        }
    }
}
