using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PassCodeManager.Classified.DBcontext;
using System.Text;
using PassCodeManager.Classified.Registration;
using PassCodeManager.Infrastructure.Middlewares;

namespace PassCodeManager
{
    public class Startup
    {
        private readonly IConfiguration _configuration;
        public Startup(IConfiguration configuration)
        {
            _configuration = configuration
                ?? throw new ArgumentNullException(nameof(configuration));
        }

        public void ConfigureServices(IServiceCollection services, IWebHostEnvironment environment) 
        {
            services.AddAutoMapper(typeof(Startup));
            services.AddControllers();
            services.AddEndpointsApiExplorer();

            services.AddServicesInAssembly(_configuration);

            services.AddDbContext<Context>(options =>
                    {
                        var configuration = new ConfigurationBuilder()
                            .SetBasePath(environment.ContentRootPath)
                            .AddJsonFile("appsettings.json")
                            .Build();

                        options.UseMySql(configuration.GetConnectionString("DefaultConnection"), new MySqlServerVersion(new Version(8, 0, 22))); // Use Pomelo's MySQL provider
                    });

            services.AddSwaggerGen();

            var jwtSettings = _configuration.GetSection("Jwt");

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings["Issuer"],
                    ValidAudience = jwtSettings["Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Key"] ?? string.Empty))
                };
            });
            services.AddAuthorization();
        }

        public void Configure(WebApplication app, IWebHostEnvironment webHostEnvironment)
        {
            app.UseMiddleware<CustomExceptionHandlingMiddleware>(); // Before Prod Modification needed for throw the exception message.

            if (webHostEnvironment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.Run();

        }
    }
}
