using LearningPortal.Application.Contract.ApplicationDTO.Language;
using LearningPortal.Application.Contract.ApplicationDTO.Result;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LearningPortal.Application.App.Language
{
    public interface ILanguageApplication
    {
        Task<string> GetCodeByAbbrAsync(InpGetCodeByAbbr Input);
        Task<OperationResult<string>> GetIdByLangCode(InpGetIdByLangCode Input);
        Task<List<OutGetLanguagesForSiteLang>> GetLanguagesForSiteLangAsync();
        Task<bool> IsValidAbbrForSiteLangAsync(InpIsValidAbbrForSiteLang Input);
    }
}