using LearningPortal.Domain.FileServers.ServerAgg.Contracts;
using LearningPortal.Domain.FileServers.ServerAgg.Entity;
using LearningPortal.Framework.Contracts;
using LearningPortal.Infrastructure.EFCore.Context;

namespace LearningPortal.Infrastructure.EFCore.Repositories.FileServers
{
    public class FileServerRepository : BaseRepository<tblFileServers>, IFileServerRepository
    {
        public FileServerRepository(MainContext Context) : base(Context)
        {

        }
    }
}
