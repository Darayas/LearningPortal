using LearningPortal.Domain.Users.AccessLevelAgg.Entities;
using LearningPortal.Domain.Users.RoleAgg.Entities;
using LearningPortal.Domain.Users.UserAgg.Entities;
using LearningPortal.Framework.Common.ExMethod;
using LearningPortal.Framework.Contracts;
using LearningPortal.Infrastructure.EFCore.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace LearningPortal.Infrastructure.EFCore.Data.Seed
{
    public class Seed_Users
    {
        BaseRepository<tblUsers> _RepUser;
        BaseRepository<tblRoles> _RepRoles;
        BaseRepository<tblUserRoles> _RepUserRoles;
        BaseRepository<tblAccessLevels> _RepAccessLevel;
        public Seed_Users()
        {
            _RepUser= new BaseRepository<tblUsers>(new MainContext());
            _RepRoles= new BaseRepository<tblRoles>(new MainContext());
            _RepAccessLevel= new BaseRepository<tblAccessLevels>(new MainContext());
            _RepUserRoles= new BaseRepository<tblUserRoles>(new MainContext());
        }

        public async Task RunAsync()
        {
            var _Id = new Guid().SequentialGuid();
            if (!await _RepUser.Get.Where(a => a.Email=="reza9025@gmail.com").AnyAsync())
            {
                await _RepUser.AddAsync(new tblUsers
                {
                    Id=_Id,
                    AccessLevelId=await _RepAccessLevel.Get.Where(a => a.Name=="GeneralManager").Select(a => a.Id).SingleAsync(),
                    Email="reza9025@gmail.com",
                    NormalizedEmail="reza9025@gmail.com".ToUpper(),
                    EmailConfirmed=true,
                    FullName="MohammadReza Ahmadi",
                    IsActive=true,
                    UserName="reza9025@gmail.com",
                    NormalizedUserName="reza9025@gmail.com".ToUpper(),
                    PhoneNumber="09010112829",
                    PhoneNumberConfirmed=true,
                    PasswordHash="AQAAAAEAACcQAAAAEO3Ro+1N3qaDwJUK02Qih+FlDMKZxhO0Z2JPMgd3rgrQSPeFLQh3txpgkEvlFMRUXA==",
                    SecurityStamp="QHZXXDN4PZUNNXGC6LQRVNOZ5EGGIKWH",

                }, default, true);
            }
            else
                _Id= await _RepUser.Get.Where(a => a.Email=="reza9025@gmail.com").Select(a => a.Id).SingleAsync();

            #region Remove All Roles
            {
                var qUserRolesId = await _RepUserRoles.Get.Where(a => a.UserId==_Id).ToListAsync();
                await _RepUserRoles.DeleteRangeAsync(qUserRolesId, default, true);
            }
            #endregion

            #region Set All Roles
            {
                var qRoleIds = await _RepRoles.Get.Select(a => a.Id).ToListAsync();
                foreach (var item in qRoleIds)
                {
                    var tUserRole = new tblUserRoles
                    {
                        Id= new Guid().SequentialGuid(),
                        UserId=_Id,
                        RoleId=item
                    };

                    await _RepUserRoles.AddAsync(tUserRole, default, false);
                }

                await _RepUserRoles.SaveChangeAsync();
            }
            #endregion
        }
    }
}
