using LearningPortal.Domain.Region.CitryAgg.Contracts;
using LearningPortal.Domain.Region.CitryAgg.Entity;
using LearningPortal.Framework.Contracts;
using LearningPortal.Infrastructure.EFCore.Context;

namespace LearningPortal.Infrastructure.EFCore.Repositories.Cities
{
    public class CityRepository : BaseRepository<tblCities>, ICityRepository
    {
        public CityRepository(MainContext context) : base(context)
        {

        }
    }
}
