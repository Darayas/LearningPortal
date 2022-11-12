using LearningPortal.Domain.Region.CountryAgg.Contracts;
using LearningPortal.Domain.Region.CountryAgg.Entity;
using LearningPortal.Framework.Contracts;
using LearningPortal.Infrastructure.EFCore.Context;

namespace LearningPortal.Infrastructure.EFCore.Repositories.Country
{
    public class CountryRepository : BaseRepository<tblCountry>, ICountryRepository
    {
        public CountryRepository(MainContext Context) : base(Context)
        {

        }
    }
}
