using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using SocialMedia.Core.Interfaces;
using SocialMedia.Core.Services;
using SocialMedia.Infrastructure.Data;
using SocialMedia.Infrastructure.Interfaces;
using SocialMedia.Infrastructure.Options;
using SocialMedia.Infrastructure.Repositories;
using SocialMedia.Infrastructure.Services;
using System.Text;

namespace SocialMedia.Infrastructure.Extensions
{
    public static class ServiceCollectionExtension
    {
      
        public static void AddOptions(this IServiceCollection services, IConfiguration configuration)
        {
            //appsetting section adding data to call PaginationOptions
            services.Configure<PaginationOptions>(configuration.GetSection("Pagination"));
            services.Configure<PasswordOption>(configuration.GetSection("PasswordOptions"));
        }
        public static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<IPostService, PostService>();
            services.AddScoped<ISecurityService, SecurityService>();
            //This is a generic repository using pattern repository
            services.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));

            //Using pattern UnitOfWork with repository pttern
            services.AddTransient<IUnitOfWork, UnitOfWork>();

            services.AddSingleton<IPasswordHasher, PasswordService>();


            services.AddSingleton<IUriService>(provider =>
            {
                var accessor = provider.GetRequiredService<IHttpContextAccessor>();
                var request = accessor?.HttpContext!.Request;
                var absoluteUri = string.Concat(request!.Scheme, "://", request.Host.ToUriComponent());
                return new UriService(absoluteUri);
            });
        }
        public static void AddDbContexts(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<SocialMediaContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("SqlConnection")));
        }
        public static void AddAuthenticationService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = configuration["Authentication:Issuer"],
                    ValidAudience = configuration["Authentication:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Authentication:SecretKey"]!))
                };
            });
        }
    }
}
