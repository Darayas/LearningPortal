using LearningPortal.Framework.Contracts;
using LearningPortal.WebApp.Localization.Resource;
using Microsoft.Extensions.Localization;

namespace LearningPortal.WebApp.Localization
{
    public class Localizer : ILocalizer
    {
        private readonly IStringLocalizer _sharedLocalizer;
        public Localizer(IStringLocalizerFactory stringLocalizerFactory)
        {
            _sharedLocalizer = new FactoryLocalizer().Set(stringLocalizerFactory, typeof(SharedResource));
        }
        public Localizer(IStringLocalizer sharedLocalizer)
        {
            _sharedLocalizer = sharedLocalizer;
        }

        public string this[string Name] => Get(Name);

        public string this[string Name, params object[] args] => Get(Name, args);

        private string Get(string Name)
        {
            return _sharedLocalizer[Name];
        }        
        private string Get(string Name, params object[] args)
        {
            return _sharedLocalizer[Name, args];
        }
    }
}
