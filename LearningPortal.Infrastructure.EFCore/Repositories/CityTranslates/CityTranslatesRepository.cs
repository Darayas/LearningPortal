using LearningPortal.Domain.Region.CitryAgg.Contracts;
using LearningPortal.Domain.Region.CitryAgg.Entity;
using LearningPortal.Framework.Contracts;
using LearningPortal.Infrastructure.EFCore.Context;

namespace LearningPortal.Infrastructure.EFCore.Repositories.CityTranslates
{
    public class CityTranslatesRepository : BaseRepository<tblCityTranslates>, ICityTranslatesRepository
    {
        public CityTranslatesRepository(MainContext context):base(context)
        {
        }
    }
}
