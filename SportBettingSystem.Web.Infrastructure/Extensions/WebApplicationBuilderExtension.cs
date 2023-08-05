namespace SportsBettingSystem.Web.Infrastructure.Extensions
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.DependencyInjection;
    using SportsBettingSystem.Data.Models;
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

        public static IApplicationBuilder SeedAdministrator(this IApplicationBuilder app, string email)
        {
            using IServiceScope scopedServcie = app.ApplicationServices.CreateScope();

            IServiceProvider serviceProvider = scopedServcie.ServiceProvider;

            UserManager<ApplicationUser> userManager = 
                serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            RoleManager<IdentityRole<Guid>> roleManager = 
                serviceProvider.GetRequiredService<RoleManager<IdentityRole<Guid>>>();

            Task.
                Run(async () =>
                {
                    if (await roleManager.RoleExistsAsync("Administrator"))
                    {
                        return;
                    }

                    IdentityRole<Guid> role =
                        new IdentityRole<Guid>("Administrator");

                    await roleManager.CreateAsync(role);

                    ApplicationUser adminUser =
                        await userManager.FindByEmailAsync(email);

                    await userManager.AddToRoleAsync(adminUser, "Administrator");
                })
            .GetAwaiter()
            .GetResult();

            return app;
        }
    }
}
