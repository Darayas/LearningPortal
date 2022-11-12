using LearningPortal.Domain.Region.LanguageAgg.Contract;
using LearningPortal.Domain.Region.LanguageAgg.Entities;
using LearningPortal.Framework.Contracts;
using LearningPortal.Infrastructure.EFCore.Context;

namespace LearningPortal.Infrastructure.EFCore.Repositories.Language
{
    public class LanguageRepository : BaseRepository<tblLanguage>, ILanguageRepository
    {
        public LanguageRepository(MainContext Context) : base(Context)
        {

        }
    }
}
