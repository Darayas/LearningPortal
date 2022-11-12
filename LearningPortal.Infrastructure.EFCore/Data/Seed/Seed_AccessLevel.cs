using LearningPortal.Domain.Users.AccessLevelAgg.Entities;
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
    public class Seed_AccessLevel
    {
        BaseRepository<tblAccessLevels> _RepAccLevel;
        BaseRepository<tblAccessLevelRoles> _RepAccLevelRoles;
        BaseRepository<tblRoles> _RepRoles;
        public Seed_AccessLevel()
        {
            _RepAccLevel= new BaseRepository<tblAccessLevels>(new MainContext());
            _RepAccLevelRoles= new BaseRepository<tblAccessLevelRoles>(new MainContext());
            _RepRoles= new BaseRepository<tblRoles>(new MainContext());
        }

        public async Task RunAsync()
        {
            try
            {
                #region GeneralManager
                {
                    var _Id = new Guid().SequentialGuid();
                    if (!await _RepAccLevel.Get.Where(a => a.Name=="GeneralManager").AnyAsync())
                    {
                        await _RepAccLevel.AddAsync(new tblAccessLevels
                        {
                            Id= _Id,
                            Name="GeneralManager",
                            tblAccessLevelRoles=default
                        }, default, true);
                    }
                    else
                        _Id= await _RepAccLevel.Get.Where(a => a.Name=="GeneralManager").Select(a => a.Id).SingleAsync();


                    #region Remove All Roles
                    {
                        var _qAccRoleIds = await _RepAccLevelRoles.Get.Where(a => a.AccessLevelId==_Id).ToListAsync();
                        await _RepAccLevelRoles.DeleteRangeAsync(_qAccRoleIds, default, true);
                    }
                    #endregion

                    #region Set Roles
                    {
                        var qRoles = await _RepRoles.Get.Select(a => a.Id).ToListAsync();
                        foreach (var item in qRoles)
                        {
                            var tAccRole = new tblAccessLevelRoles
                            {
                                Id=new Guid().SequentialGuid(),
                                AccessLevelId=_Id,
                                RoleId=item
                            };

                            await _RepAccLevelRoles.AddAsync(tAccRole, default, false);
                        }

                        await _RepAccLevelRoles.SaveChangeAsync();
                    }
                    #endregion
                }
                #endregion



                if (!await _RepAccLevel.Get.Where(a => a.Name=="ConfirmedUser").AnyAsync())
                {
                    await _RepAccLevel.AddAsync(new tblAccessLevels
                    {
                        Id= new Guid().SequentialGuid(),
                        Name="ConfirmedUser"
                    }, default, true);
                }

                if (!await _RepAccLevel.Get.Where(a => a.Name=="UnConfirmedUser").AnyAsync())
                {
                    await _RepAccLevel.AddAsync(new tblAccessLevels
                    {
                        Id= new Guid().SequentialGuid(),
                        Name="UnConfirmedUser"
                    }, default, true);
                }

            }
            catch (Exception ex)
            {

            }
        }
    }
}
