using LearningPortal.Domain.Region.LanguageAgg.Entities;
using LearningPortal.Domain.Settings.NotificationTemplateAgg.Entities;
using LearningPortal.Framework.Common.ExMethod;
using LearningPortal.Framework.Contracts;
using LearningPortal.Infrastructure.EFCore.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace LearningPortal.Infrastructure.EFCore.Data.Seed
{
    public class Seed_NotificationTemplate
    {
        BaseRepository<tblLanguage> _RepLang = null;
        BaseRepository<tblNotificationTemplates> _RepNotify = null;
        public Seed_NotificationTemplate()
        {
            _RepLang= new BaseRepository<tblLanguage>(new MainContext());
            _RepNotify= new BaseRepository<tblNotificationTemplates>(new MainContext());
        }

        public async Task RunAsync()
        {
            try
            {
                var EnLangId = await _RepLang.Get.Where(a => a.Code=="en-US").Select(a => a.Id).SingleOrDefaultAsync();
                var FaLangId = await _RepLang.Get.Where(a => a.Code=="fa-IR").Select(a => a.Id).SingleOrDefaultAsync();

                if (!await _RepNotify.Get.Where(a => a.LangId==EnLangId).Where(a => a.Name=="RegisterEmailConfirm").AnyAsync())
                {
                    await _RepNotify.AddAsync(new tblNotificationTemplates
                    {
                        Id= new Guid().SequentialGuid(),
                        LangId=EnLangId,
                        Name="RegisterEmailConfirm",
                        Text="<a href='[Link]'>Click Me!!!</a>"
                    }, default, false);
                }

                if (!await _RepNotify.Get.Where(a => a.LangId==FaLangId).Where(a => a.Name=="RegisterEmailConfirm").AnyAsync())
                {
                    await _RepNotify.AddAsync(new tblNotificationTemplates
                    {
                        Id= new Guid().SequentialGuid(),
                        LangId=FaLangId,
                        Name="RegisterEmailConfirm",
                        Text="<a href='[Link]'>کلیک کنید</a>"
                    }, default, false);
                }

                if (!await _RepNotify.Get.Where(a => a.LangId==EnLangId).Where(a => a.Name=="ForgotPassword").AnyAsync())
                {
                    await _RepNotify.AddAsync(new tblNotificationTemplates
                    {
                        Id= new Guid().SequentialGuid(),
                        LangId=EnLangId,
                        Name="ForgotPassword",
                        Text="<a href='[Link]'>Click Me!!!</a>"
                    }, default, false);
                }

                if (!await _RepNotify.Get.Where(a => a.LangId==FaLangId).Where(a => a.Name=="ForgotPassword").AnyAsync())
                {
                    await _RepNotify.AddAsync(new tblNotificationTemplates
                    {
                        Id= new Guid().SequentialGuid(),
                        LangId=FaLangId,
                        Name="ForgotPassword",
                        Text="<a href='[Link]'>کلیک کنید</a>"
                    }, default, false);
                }

                if (!await _RepNotify.Get.Where(a => a.LangId==EnLangId).Where(a => a.Name=="DisposableLink").AnyAsync())
                {
                    await _RepNotify.AddAsync(new tblNotificationTemplates
                    {
                        Id= new Guid().SequentialGuid(),
                        LangId=EnLangId,
                        Name="DisposableLink",
                        Text="<a href='[Link]'>Click Me!!!</a>"
                    }, default, false);
                }

                if (!await _RepNotify.Get.Where(a => a.LangId==FaLangId).Where(a => a.Name=="DisposableLink").AnyAsync())
                {
                    await _RepNotify.AddAsync(new tblNotificationTemplates
                    {
                        Id= new Guid().SequentialGuid(),
                        LangId=FaLangId,
                        Name="DisposableLink",
                        Text="<a href='[Link]'>کلیک کنید</a>"
                    }, default, false);
                }

                if (!await _RepNotify.Get.Where(a => a.LangId==EnLangId).Where(a => a.Name=="ChangeEmailConfirm").AnyAsync())
                {
                    await _RepNotify.AddAsync(new tblNotificationTemplates
                    {
                        Id= new Guid().SequentialGuid(),
                        LangId=EnLangId,
                        Name="ChangeEmailConfirm",
                        Text="<a href='[Link]'>Click Me!!!</a>"
                    }, default, false);
                }

                if (!await _RepNotify.Get.Where(a => a.LangId==FaLangId).Where(a => a.Name=="ChangeEmailConfirm").AnyAsync())
                {
                    await _RepNotify.AddAsync(new tblNotificationTemplates
                    {
                        Id= new Guid().SequentialGuid(),
                        LangId=FaLangId,
                        Name="ChangeEmailConfirm",
                        Text="<a href='[Link]'>کلیک کنید</a>"
                    }, default, false);
                }

                await _RepNotify.SaveChangeAsync();
            }
            catch (Exception)
            {

            }
        }
    }
}
