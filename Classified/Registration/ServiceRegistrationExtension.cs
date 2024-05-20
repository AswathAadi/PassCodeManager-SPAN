using Microsoft.AspNetCore.Hosting;
using PassCodeManager.Classified.Registration.Abstract;

namespace PassCodeManager.Classified.Registration
{
    public static class ServiceRegistrationExtension
    {
        public static void AddServicesInAssembly(this IServiceCollection services, IConfiguration configuration)
        {
            var appServices = typeof(Startup).Assembly.DefinedTypes
                            .Where(x => typeof(IServiceRegistration)
                            .IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract)
                            .Select(Activator.CreateInstance)
                            .Cast<IServiceRegistration>().ToList();

            appServices.ForEach(svc => svc.RegisterAppServices(services, configuration));
        }
    }
}
