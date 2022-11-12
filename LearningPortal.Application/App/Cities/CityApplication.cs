using LearningPortal.Domain.Region.CitryAgg.Contracts;
using LearningPortal.Framework.Contracts;

namespace LearningPortal.Application.App.Cities
{
    public class CityApplication : ICityApplication
    {
        private readonly ILogger _Logger;
        private readonly ICityRepository _CityRepository;
        public CityApplication(ILogger logger, ICityRepository cityRepository)
        {
            _Logger = logger;
            _CityRepository = cityRepository;
        }
    }
}
