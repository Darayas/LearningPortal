using LearningPortal.Framework.Const;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;

namespace LearningPortal.WebApp.Authentication
{
    public static class ConfigIdentityJWT
    {
        private static string _SecretKey;
        private static byte[] _SecretCode;
        public static void AddJwtAuthentication(this IServiceCollection Services)
        {
            _SecretKey= AuthConst.SecretKey;
            _SecretCode = Encoding.ASCII.GetBytes(AuthConst.SecretCode);

            Services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(opt =>
            {
                opt.RequireHttpsMetadata= false;
                opt.TokenValidationParameters= new TokenValidationParameters
                {
                    ClockSkew=TimeSpan.Zero,
                    RequireSignedTokens=true,

                    ValidateIssuerSigningKey=true,
                    IssuerSigningKey=new SymmetricSecurityKey(_SecretCode),

                    RequireExpirationTime=true,
                    ValidateLifetime=true,

                    ValidAudience=AuthConst.Audience,
                    ValidateAudience=true,

                    ValidIssuer=AuthConst.Issuer,
                    ValidateIssuer=true
                };
            });
        }

        public static void UseJwtAuthentication(this IApplicationBuilder app)
        {
            app.UseMiddleware<JwtAuthenticationWebAppMiddleware>();

            app.UseAuthentication();
            app.UseAuthorization();
        }
    }
}
