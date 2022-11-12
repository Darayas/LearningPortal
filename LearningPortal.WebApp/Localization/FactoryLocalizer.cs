using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using System;
using System.Reflection;

namespace LearningPortal.WebApp.Localization
{
    public class FactoryLocalizer
    {
        public IStringLocalizer Set(IStringLocalizerFactory factory, Type typeOfSharedResource)
        {
            var assemblyName = new AssemblyName(typeOfSharedResource.GetTypeInfo().Assembly.FullName);

            return factory.Create("SharedResource", assemblyName.Name);
        }        
        public IStringLocalizer Set(IServiceCollection services, Type typeOfSharedResource)
        {
            var factory = services.BuildServiceProvider().GetService<IStringLocalizerFactory>();

            return Set(factory, typeOfSharedResource);
        }
    }
}
