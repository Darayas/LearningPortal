using LearningPortal.Domain.Users.AccessLevelAgg.Contracts;
using LearningPortal.Domain.Users.AccessLevelAgg.Entities;
using LearningPortal.Framework.Contracts;
using LearningPortal.Infrastructure.EFCore.Context;

namespace LearningPortal.Infrastructure.EFCore.Repositories.AccessLevels
{
    public class AccessLevelRoleRepository : BaseRepository<tblAccessLevelRoles>, IAccessLevelRoleRepository
    {
        public AccessLevelRoleRepository(MainContext Context) : base(Context)
        {

        }
    }
}
