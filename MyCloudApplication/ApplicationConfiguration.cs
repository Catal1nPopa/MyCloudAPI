﻿using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using MyCloudApplication.DTOs;
using MyCloudApplication.Interfaces;
using MyCloudApplication.Services;
using System.Text;

namespace MyCloudApplication
{
    public static class ApplicationConfiguration
    {
        public static void ConfigureJWT(this IServiceCollection services, IConfiguration configuration)
        {
            var key = Encoding.ASCII.GetBytes(configuration["JWT:SecretKey"]);

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(o =>
            {
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidateAudience = false,
                    ValidateIssuer = false,
                    ClockSkew = TimeSpan.Zero
                };
            });
        }

        public static IServiceCollection AddApplicationServices(this IServiceCollection Services, IConfiguration configuration)
        {
            Services.AddScoped<IAuth, AuthService>();
            Services.AddScoped<IStorage, StorageService>();
            Services.AddScoped<IFiles, FilesService>();
            Services.AddScoped<IGroups, GroupsService>();

            Services.AddSignalR();
            Services.Configure<StorageSettingsDTO>(configuration.GetSection("StorageSettings"));
            return Services;
        }
    }
}
