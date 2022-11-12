using LearningPortal.Domain.FileServers.FilePathAgg.Entity;
using LearningPortal.Domain.FileServers.ServerAgg.Entity;
using LearningPortal.Framework.Common.ExMethod;
using LearningPortal.Framework.Contracts;
using LearningPortal.Infrastructure.EFCore.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningPortal.Infrastructure.EFCore.Data.Seed
{
    public class Seed_FilePath
    {
        BaseRepository<tblFileServers> _RepFileServer;
        BaseRepository<tblFilePaths> _RepFilePath;
        public Seed_FilePath()
        {
            _RepFileServer = new BaseRepository<tblFileServers>(new MainContext());
            _RepFilePath = new BaseRepository<tblFilePaths>(new MainContext());
        }
        public async Task RunAsync()
        {
            try
            {
                #region Public FileServer
                {
                    string _FileServerId = await _RepFileServer.Get.Where(a => a.Name=="Public").Select(a => a.Id.ToString()).SingleOrDefaultAsync();

                    if (!await _RepFilePath.Get.AnyAsync(a => a.FileServerId==_FileServerId.ToGuid() && a.Path == "/ProfileImg"))
                    {
                        await _RepFilePath.AddAsync(new tblFilePaths
                        {
                            Id= new Guid().SequentialGuid(),
                            FileServerId=_FileServerId.ToGuid(),
                            Path="/ProfileImg/"
                        }, default, false);
                    }
                }
                #endregion

                await _RepFilePath.SaveChangeAsync();
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
