using LearningPortal.Domain.FileServers.FilePathAgg.Contracts;
using LearningPortal.Domain.FileServers.FilePathAgg.Entity;
using LearningPortal.Framework.Contracts;
using LearningPortal.Infrastructure.EFCore.Context;

namespace LearningPortal.Infrastructure.EFCore.Repositories.FilePath
{
    public class FilePathRepository : BaseRepository<tblFilePaths>, IFilePathRepository
    {
        public FilePathRepository(MainContext context) : base(context)
        {

        }
    }
}
