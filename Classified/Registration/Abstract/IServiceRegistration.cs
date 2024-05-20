namespace PassCodeManager.Classified.Registration.Abstract
{
    public interface IServiceRegistration
    {
        void RegisterAppServices(IServiceCollection services, IConfiguration configuration);
    }
}
