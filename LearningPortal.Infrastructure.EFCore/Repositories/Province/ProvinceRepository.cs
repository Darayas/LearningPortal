using LearningPortal.Domain.Region.ProvinceAgg.Contracts;
using LearningPortal.Domain.Region.ProvinceAgg.Entity;
using LearningPortal.Framework.Contracts;
using LearningPortal.Infrastructure.EFCore.Context;

namespace LearningPortal.Infrastructure.EFCore.Repositories.Province
{
    public class ProvinceRepository : BaseRepository<tblProvinces>, IProvinceRepository
    {
        public ProvinceRepository(MainContext context) : base(context)
        {

        }
    }
}
