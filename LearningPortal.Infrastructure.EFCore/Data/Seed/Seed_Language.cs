using LearningPortal.Domain.Region.LanguageAgg.Entities;
using LearningPortal.Framework.Common.ExMethod;
using LearningPortal.Framework.Contracts;
using LearningPortal.Infrastructure.EFCore.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningPortal.Infrastructure.EFCore.Data.Seed
{
    public class Seed_Language
    {
        BaseRepository<tblLanguage> _baseRepository;        
        public Seed_Language()
        {
            _baseRepository = new BaseRepository<tblLanguage>(new MainContext());
        }
        public async Task RunAsync()
        {
            try
            {
                if (!_baseRepository.Get.Where(l => l.Name == "Persian (Iran)").Any())
                {
                    await _baseRepository.AddAsync(new tblLanguage()
                    {
                        Id= new Guid().SequentialGuid(),
                        Name = "Persian (Iran)",
                        Code = "fa-IR",
                        Abbr = "fa",
                        NativeName = "فارسی (ایران)",
                        IsRtl = true,
                        IsActive = true,
                        UseForSiteLanguage = true
                    }, default) ;
                }

                if (!_baseRepository.Get.Where(l => l.Name == "English (USA)").Any())
                {
                    await _baseRepository.AddAsync(new tblLanguage()
                    {
                        Id= new Guid().SequentialGuid(),
                        Name = "English (USA)",
                        Code = "en-US",
                        Abbr = "en",
                        NativeName = "English (USA)",
                        IsRtl = true,
                        IsActive = true,
                        UseForSiteLanguage = true
                    }, default);
                }                
            }
            catch (Exception ex)
            {

            }
        }
    }
}
