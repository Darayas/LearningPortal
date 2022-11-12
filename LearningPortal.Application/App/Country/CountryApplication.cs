using LearningPortal.Domain.Region.CountryAgg.Contracts;
using LearningPortal.Framework.Contracts;

namespace LearningPortal.Application.App.Country
{
    public class CountryApplication : ICountryApplication
    {
        private readonly ILogger _Logger;
        private readonly ICountryRepository _CountryRepository;

        public CountryApplication(ILogger logger, ICountryRepository countryRepository)
        {
            _Logger=logger;
            _CountryRepository=countryRepository;
        }
    }
}
