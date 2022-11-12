using LearningPortal.Domain.Region.ProvinceAgg.Contracts;
using LearningPortal.Framework.Contracts;

namespace LearningPortal.Application.App.Province
{
    public class ProvinceApplication : IProvinceApplication
    {
        private readonly ILogger _Logger;
        private readonly IProvinceRepository _ProvinceRepository;
        public ProvinceApplication(ILogger logger, IProvinceRepository provinceRepository)
        {
            _Logger = logger;
            _ProvinceRepository = provinceRepository;
        }
    }
}
