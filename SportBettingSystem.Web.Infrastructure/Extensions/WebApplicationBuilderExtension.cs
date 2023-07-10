namespace SportsBettingSystem.Web.Infrastructure.Extensions
{
    using Microsoft.Extensions.DependencyInjection;
    using System.Reflection;

    public static class WebApplicationBuilderExtension
    {
        /// <summary>
        /// This method registers every service interface and implementation from an assembly of a given *random* service
        /// </summary>
        /// <param name="serviceType">Type of random service</param>
        /// <exception cref="InvalidOperationException"></exception>
        public static void AddApplicationServices(this IServiceCollection services, Type serviceType)
        {
            Assembly? serviceAssembly = Assembly.GetAssembly(serviceType);
            if (serviceAssembly == null)
                throw new InvalidOperationException("Invalid service type provided");

            Type[] serviceTypes = serviceAssembly
                .GetTypes()
                .Where(t => t.Name.EndsWith("Service") && !t.IsInterface)
                .ToArray();

            foreach (var implementationType in serviceTypes)
            {
                Type? interfaceType = implementationType
                    .GetInterface($"I{implementationType.Name}");
                if(interfaceType == null)
                    throw new InvalidOperationException($"No interface is provided for the service with name: {implementationType.Name}");

                services.AddScoped(interfaceType,implementationType);
            }
        }
    }
}
