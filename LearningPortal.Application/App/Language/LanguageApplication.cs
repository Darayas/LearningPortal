using LearningPortal.Application.Contract.ApplicationDTO.Language;
using LearningPortal.Application.Contract.ApplicationDTO.Result;
using LearningPortal.Domain.Region.LanguageAgg.Contract;
using LearningPortal.Framework.Common.ExMethod;
using LearningPortal.Framework.Contracts;
using LearningPortal.Framework.Exceptions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LearningPortal.Application.App.Language
{
    public class LanguageApplication : ILanguageApplication
    {
        private readonly ILogger _Logger;
        private readonly IServiceProvider _ServiceProvider;
        private readonly ILanguageRepository _LanguageRepository;
        private List<OutSiteLangCache> _Languages;

        public LanguageApplication(ILanguageRepository languageApplication, IServiceProvider serviceProvider, ILogger logger)
        {
            _LanguageRepository = languageApplication;
            _ServiceProvider=serviceProvider;
            _Logger=logger;
        }

        public async Task<string> GetCodeByAbbrAsync(InpGetCodeByAbbr Input)
        {
            //TODO: Validation            

            await LoadCacheAsync();

            return _Languages.Where(a => a.Abbreviation == Input.Abbreviation).Select(a => a.Code).SingleOrDefault();
        }
        public async Task LoadCacheAsync()
        {
            try
            {
                if (_Languages == null)
                {
                    _Languages = await _LanguageRepository.Get
                        .Where(l => l.IsActive)
                        .Where(l => l.UseForSiteLanguage)
                        .Select(a => new OutSiteLangCache
                        {
                            Id = a.Id.ToString(),
                            Name = a.Name,
                            Code = a.Code,
                            Abbreviation = a.Abbr,
                            NativeName = a.NativeName,
                            IsRtl = a.IsRtl
                        })
                        .ToListAsync();

                    //var qdata = _LanguageRepository.Get;

                    //var qdata2 = qdata.Where(a => a.IsActive);
                    //var qdata3 = qdata2.Where(a => a.UseForSiteLanguage);
                    //var qdata4 = qdata3.Select(a => new OutSiteLangCache
                    //{
                    //    Id = a.Id.ToString(),
                    //    Name = a.Name,
                    //    Code = a.Code,
                    //    Abbreviation = a.Abbr,
                    //    NativeName = a.NativeName,
                    //    IsRtl = a.IsRtl
                    //});

                    //var qdata5 = qdata4.ToList();
                }
            }
            catch (Exception)
            {
                //TODO: Log
            }
        }
        public async Task<bool> IsValidAbbrForSiteLangAsync(InpIsValidAbbrForSiteLang Input)
        {
            //TODO: Validation 

            await LoadCacheAsync();

            return _Languages.Where(a => a.Abbreviation == Input.Abbr).Any();
        }
        public async Task<List<OutGetLanguagesForSiteLang>> GetLanguagesForSiteLangAsync()
        {
            //TODO: Validation 

            await LoadCacheAsync();

            return _Languages.Select(l => new OutGetLanguagesForSiteLang { Abbreviation = l.Abbreviation, NativeName = l.NativeName }).ToList();
        }

        public async Task<OperationResult<string>> GetIdByLangCode(InpGetIdByLangCode Input)
        {
            try
            {
                #region Validation
                Input.CheckModelState(_ServiceProvider);
                #endregion

                await LoadCacheAsync();

                string Id = _Languages.Where(a => a.Code == Input.LangCode).Select(a => a.Id.ToString()).SingleOrDefault();

                return new OperationResult<string>().Succeeded(Id);
            }
            catch (ArgumentInvalidException ex)
            {
                _Logger.Debug(ex);
                return new OperationResult<string>().Failed(ex.Message);
            }
            catch (Exception ex)
            {
                _Logger.Error(ex);
                return new OperationResult<string>().Failed("Error500");
            }
        }
    }
}
