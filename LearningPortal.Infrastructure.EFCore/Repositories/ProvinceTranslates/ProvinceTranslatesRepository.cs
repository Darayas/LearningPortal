using LearningPortal.Domain.Region.ProvinceAgg.Contracts;
using LearningPortal.Domain.Region.ProvinceAgg.Entity;
using LearningPortal.Framework.Contracts;
using LearningPortal.Infrastructure.EFCore.Context;

namespace LearningPortal.Infrastructure.EFCore.Repositories.ProvinceTranslates
{
    public class ProvinceTranslatesRepository : BaseRepository<tblProvinceTranslates>, IProvinceTranslatesRepository
    {
        public ProvinceTranslatesRepository(MainContext context):base(context)
        {
        }
    }
}
