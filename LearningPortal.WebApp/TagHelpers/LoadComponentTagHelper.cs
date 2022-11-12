using LearningPortal.Framework.Common.Utilities.Downloader;
using LearningPortal.Framework.Const;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LearningPortal.WebApp.TagHelpers
{
    [HtmlTargetElement("LoadComponent")]
    public class LoadComponentTagHelper : TagHelper
    {
        public string Id { get; set; }
        public string Class { get; set; }
        public string Url { get; set; }
        public object Data { get; set; }
        public HttpContext HttpContext { get; set; }

        private readonly IDownloader _Downloader;
        public LoadComponentTagHelper(IDownloader downloader)
        {
            _Downloader=downloader;
        }

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(Url))
                    throw new ArgumentNullException(nameof(Url));

                if (HttpContext == null)
                    throw new ArgumentNullException(nameof(HttpContext));

                Url = SiteSettingConst.SiteUrl.Trim('/') + "/" + Url.Trim('/');

                var _Headers = HttpContext.Request.Headers
                                .Select(a => new KeyValuePair<string, string>(a.Key, a.Value))
                                .ToDictionary(k => k.Key, v => v.Value);

                string HtmlData = await _Downloader.GetHtmlForPageAsync(Url, Data, _Headers);

                if (HtmlData==null)
                    throw new Exception("Html data is null");

                output.TagName="div";
                if (Id !=null)
                    output.Attributes.Add("id", Id);

                if (Class !=null)
                    output.Attributes.Add("class", Class);

                output.Content.SetHtmlContent(HtmlData);
            }
            catch (Exception)
            {
                output.TagName = "div";

                if (Id != null)
                    output.Attributes.SetAttribute("id", Id);

                if (Class != null)
                    output.Attributes.SetAttribute("class", Class);

                output.Content.SetHtmlContent("<err500>Module Error: 500, Internal Server Error</err500>");
            }
        }
    }
}
