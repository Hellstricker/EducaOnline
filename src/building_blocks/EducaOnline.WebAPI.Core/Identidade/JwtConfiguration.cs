using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace EducaOnline.WebAPI.Core.Identidade
{
    public static class JwtConfiguration
    {
        public static WebApplicationBuilder AddJwtConfiguration(this WebApplicationBuilder builder)
        {           

            var appSettingSection = builder.Configuration.GetSection(nameof(JwtSettings));
            builder.Services.Configure<JwtSettings>(appSettingSection);

            var appsSettings = appSettingSection.Get<JwtSettings>();
            var key = Encoding.ASCII.GetBytes(appSettingSection.Key);

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = true;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidAudience = appsSettings!.Audiencia,
                    ValidIssuer = appsSettings.Emissor
                };
            });
            return builder;
        }

        public static WebApplication UseAuthConfiguration(this WebApplication app)
        {
            app.UseAuthorization();
            app.UseAuthentication();
            return app;
        }
    }
}
