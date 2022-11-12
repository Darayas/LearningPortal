using LearningPortal.Domain.FileServers.FileAgg.Contratcs;
using LearningPortal.Domain.FileServers.FileAgg.Entity;
using LearningPortal.Framework.Contracts;
using LearningPortal.Infrastructure.EFCore.Context;

namespace LearningPortal.Infrastructure.EFCore.Repositories.Files
{
    public class FileRepository : BaseRepository<tblFiles>, IFileRepository
    {
        public FileRepository(MainContext Context) : base(Context)
        {

        }
    }
}
