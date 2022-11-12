using LearningPortal.Domain.Region.CountryAgg.Contracts;
using LearningPortal.Domain.Region.CountryAgg.Entity;
using LearningPortal.Framework.Contracts;
using LearningPortal.Infrastructure.EFCore.Context;

namespace LearningPortal.Infrastructure.EFCore.Repositories.CountryTranslates
{
    public class CountryTranslatesRepository : BaseRepository<tblCountryTranslates>, ICountryTranslatesRepository
    {
        public CountryTranslatesRepository(MainContext context) : base(context)
        {
        }
    }
}
