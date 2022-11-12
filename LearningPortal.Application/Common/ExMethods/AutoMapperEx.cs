using AutoMapper;
using LearningPortal.Application.Contract.Mapping;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace LearningPortal.Application.Common.ExMethods
{
    public static class AutoMapperEx
    {
        public static void AddCustomAutoMapper(this IServiceCollection services)
        {
            var ProfileAssem = typeof(AutoMapping).Assembly
                                                  .GetTypes()
                                                  .Where(a => a != typeof(Profile))
                                                  .Where(a => typeof(Profile).IsAssignableFrom(a));

            services.AddAutoMapper(opt =>
            {
                opt.AddProfiles(ProfileAssem.Select(a => (Profile)Activator.CreateInstance(a)));
            });
        }
    }
}
