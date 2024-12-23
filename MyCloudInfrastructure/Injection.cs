﻿using Microsoft.Extensions.DependencyInjection;
using MyCloudDomain.Interfaces;
using MyCloudInfrastructure.Repository;

namespace MyCloudInfrastructure
{
    public static class Injection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddScoped<IAuthRepository, AuthRepository>();
            services.AddScoped<IFilesRepository, FilesRepository>();
            services.AddScoped<IGroupRepository, GroupsRepository>();

            services.AddScoped<MyDbContext>();
            return services;
        }
    }
}
