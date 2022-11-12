using LearningPortal.Infrastructure.EFCore.Data.Seed;
using System;
using System.Threading.Tasks;

namespace LearningPortal.Infrastructure.EFCore.Data.Main
{
    public class MainData
    {
        public async Task RunAsync()
        {
            try
            {
                await new Seed_FileServer().RunAsync();
                await new Seed_FilePath().RunAsync();
                await new Seed_Language().RunAsync();
                await new Seed_NotificationTemplate().RunAsync();
                await new Seed_Roles().RunAsync();
                await new Seed_AccessLevel().RunAsync();
                await new Seed_Users().RunAsync();
            }
            catch (Exception ex)
            {

            }
        }
    }
}
