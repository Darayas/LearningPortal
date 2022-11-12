using LearningPortal.Domain.Users.RoleAgg.Contracts;
using LearningPortal.Domain.Users.RoleAgg.Entities;
using LearningPortal.Framework.Contracts;
using LearningPortal.Infrastructure.EFCore.Context;

namespace LearningPortal.Infrastructure.EFCore.Repositories.Roles
{
    public class RoleRepository : BaseRepository<tblRoles>, IRoleRepository
    {
        public RoleRepository(MainContext Context) : base(Context)
        {

        }
    }
}
