using PassCodeManager.Classified.Registration.Abstract;
using PassCodeManager.Services;
using PassCodeManager.Services.Abstract;

namespace PassCodeManager.Classified.Registration
{
    public class RegisterServices : IServiceRegistration
    {
        public void RegisterAppServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IPassCodeService, PassCodeService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ILoggingService, LoggingService>();
        }

    }
}
