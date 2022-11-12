using LearningPortal.Domain.Users.RoleAgg.Entities;
using LearningPortal.Framework.Common.ExMethod;
using LearningPortal.Framework.Contracts;
using LearningPortal.Infrastructure.EFCore.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace LearningPortal.Infrastructure.EFCore.Data.Seed
{
    public class Seed_Roles
    {
        private BaseRepository<tblRoles> _RepRoles;
        public Seed_Roles()
        {
            _RepRoles = new BaseRepository<tblRoles>(new MainContext());
        }

        public async Task RunAsync()
        {
            #region AccessLevels
            {
                var _Id = new Guid().SequentialGuid();
                if (!await _RepRoles.Get.Where(a => a.Name=="CanManageAccessLevel").AnyAsync())
                {
                    await _RepRoles.AddAsync(new tblRoles
                    {
                        Id=_Id,
                        ParentId=null,
                        Name="CanManageAccessLevel",
                        NormalizedName="CanManageAccessLevel".ToUpper(),
                        PageName="AccessLevelPage",
                        Sort=0,
                        Description="توانایی مدیریت سطوح دسترسی"
                    }, default, false);
                }
                else
                {
                    _Id= await _RepRoles.Get.Where(a => a.Name=="CanManageAccessLevel").Select(a => a.Id).SingleAsync();
                }

                if (!await _RepRoles.Get.Where(a => a.Name=="CanAddAccessLevel").AnyAsync())
                {
                    await _RepRoles.AddAsync(new tblRoles
                    {
                        Id=new Guid().SequentialGuid(),
                        ParentId=_Id,
                        Name="CanAddAccessLevel",
                        NormalizedName="CanAddAccessLevel".ToUpper(),
                        PageName="AccessLevelPage",
                        Sort=10,
                        Description="توانایی افزودن سطح دسترسی"
                    }, default, false);
                }

                if (!await _RepRoles.Get.Where(a => a.Name=="CanEditAccessLevel").AnyAsync())
                {
                    await _RepRoles.AddAsync(new tblRoles
                    {
                        Id=new Guid().SequentialGuid(),
                        ParentId=_Id,
                        Name="CanEditAccessLevel",
                        NormalizedName="CanEditAccessLevel".ToUpper(),
                        PageName="AccessLevelPage",
                        Sort=20,
                        Description="توانایی ویرایش سطح دسترسی"
                    }, default, false);
                }

                if (!await _RepRoles.Get.Where(a => a.Name=="CanDeleteAccessLevel").AnyAsync())
                {
                    await _RepRoles.AddAsync(new tblRoles
                    {
                        Id=new Guid().SequentialGuid(),
                        ParentId=_Id,
                        Name="CanDeleteAccessLevel",
                        NormalizedName="CanDeleteAccessLevel".ToUpper(),
                        PageName="AccessLevelPage",
                        Sort=30,
                        Description="توانایی حذف سطح دسترسی"
                    }, default, false);
                }
            }
            #endregion

            #region Users
            {
                var _Id = new Guid().SequentialGuid();
                if (!await _RepRoles.Get.Where(a => a.Name=="CanManageUser").AnyAsync())
                {
                    await _RepRoles.AddAsync(new tblRoles
                    {
                        Id=_Id,
                        ParentId=null,
                        Name="CanManageUser",
                        NormalizedName="CanManageUser".ToUpper(),
                        PageName="UserPage",
                        Sort=40,
                        Description="توانایی مدیریت کاربران"
                    }, default, false);
                }
                else
                {
                    _Id= await _RepRoles.Get.Where(a => a.Name=="CanManageUser").Select(a => a.Id).SingleAsync();
                }

                if (!await _RepRoles.Get.Where(a => a.Name=="CanDeleteUser").AnyAsync())
                {
                    await _RepRoles.AddAsync(new tblRoles
                    {
                        Id=new Guid().SequentialGuid(),
                        ParentId=_Id,
                        Name="CanDeleteUser",
                        NormalizedName="CanDeleteUser".ToUpper(),
                        PageName="UserPage",
                        Sort=40,
                        Description="توانایی حذف کاربر"
                    }, default, false);
                }

                if (!await _RepRoles.Get.Where(a => a.Name=="CanLockUser").AnyAsync())
                {
                    await _RepRoles.AddAsync(new tblRoles
                    {
                        Id=new Guid().SequentialGuid(),
                        ParentId=_Id,
                        Name="CanLockUser",
                        NormalizedName="CanLockUser".ToUpper(),
                        PageName="UserPage",
                        Sort=50,
                        Description="توانایی قفل حساب کاربر"
                    }, default, false);
                }

                if (!await _RepRoles.Get.Where(a => a.Name=="CanUnLockUser").AnyAsync())
                {
                    await _RepRoles.AddAsync(new tblRoles
                    {
                        Id=new Guid().SequentialGuid(),
                        ParentId=_Id,
                        Name="CanUnLockUser",
                        NormalizedName="CanUnLockUser".ToUpper(),
                        PageName="UserPage",
                        Sort=60,
                        Description="توانایی باز کردن قفل حساب کاربر"
                    }, default, false);
                }
            }
            #endregion

            await _RepRoles.SaveChangeAsync();
        }
    }
}
