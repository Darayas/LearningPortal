using LearningPortal.Domain.Users.RoleAgg.Contracts;
using LearningPortal.Domain.Users.RoleAgg.Entities;
using LearningPortal.Framework.Contracts;
using LearningPortal.Infrastructure.EFCore.Context;

namespace LearningPortal.Infrastructure.EFCore.Repositories.Roles
{
    public class UserRoleRepository : BaseRepository<tblUserRoles>, IUserRoleRepository
    {
        public UserRoleRepository(MainContext Context) : base(Context)
        {

        }
    }
}
