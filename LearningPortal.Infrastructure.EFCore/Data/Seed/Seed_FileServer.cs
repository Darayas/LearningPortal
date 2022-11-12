using LearningPortal.Domain.FileServers.ServerAgg.Entity;
using LearningPortal.Framework.Common.ExMethod;
using LearningPortal.Framework.Const;
using LearningPortal.Framework.Contracts;
using LearningPortal.Infrastructure.EFCore.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace LearningPortal.Infrastructure.EFCore.Data.Seed
{
    public class Seed_FileServer
    {
        BaseRepository<tblFileServers> _RepFileServer;
        public Seed_FileServer()
        {
            _RepFileServer= new BaseRepository<tblFileServers>(new MainContext());
        }
        public async Task RunAsync()
        {
            try
            {
                #region Public FileServer
                {
                    #region Encrypt FtpData
                    string _FtpData = "";
                    {
                        string JsonData = File.ReadAllText($@"{AssemblyDirectory}\Data\Seed\Seed_FileServer.Public.json", Encoding.UTF8);
                        _FtpData = JsonData.AesEncrypt(AuthConst.SecretKey);
                    }
                    #endregion

                    if (!await _RepFileServer.Get.Where(a => a.Name == "Public").AnyAsync())
                    {
                        await _RepFileServer.AddAsync(new tblFileServers
                        {
                            Id= new Guid().SequentialGuid(),
                            Name="Public",
                            Description="سرور فایل های عمومی",
                            IsActive=true,
                            Capacity=10737418240,
                            IsPrivate=false,
                            HttpPath="/Public",
                            HttpDomin="http://127.0.0.12",
                            FtpData = _FtpData
                        }, default, true);
                    }
                }
                #endregion
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public static string AssemblyDirectory
        {
            get
            {
                string CodeBase = Assembly.GetExecutingAssembly().CodeBase;
                UriBuilder uri = new UriBuilder(CodeBase);
                string path = Uri.UnescapeDataString(uri.Path);

                return Path.GetDirectoryName(path);
            }
        }
    }
}
