using System.Collections.Generic;
using System.Threading.Tasks;

namespace LearningPortal.Framework.Common.Utilities.Downloader
{
    public interface IDownloader
    {
        Task<string> GetHtmlForPageAsync(string PageUrl, object Data, Dictionary<string, string> Headers);
    }
}