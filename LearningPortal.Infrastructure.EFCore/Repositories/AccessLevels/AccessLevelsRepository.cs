using LearningPortal.Domain.Users.AccessLevelAgg.Contracts;
using LearningPortal.Domain.Users.AccessLevelAgg.Entities;
using LearningPortal.Framework.Contracts;
using LearningPortal.Infrastructure.EFCore.Context;
using Microsoft.EntityFrameworkCore;

namespace LearningPortal.Infrastructure.EFCore.Repositories.AccessLevels
{
    public class AccessLevelsRepository : BaseRepository<tblAccessLevels>, IAccessLevelsRepository
    {
        public AccessLevelsRepository(MainContext dbContext) : base(dbContext)
        {

        }
    }
}
