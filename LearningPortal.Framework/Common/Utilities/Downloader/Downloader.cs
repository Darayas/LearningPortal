using LearningPortal.Framework.Contracts;
using LearningPortal.Framework.Exceptions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace LearningPortal.Framework.Common.Utilities.Downloader
{
    public class Downloader : IDownloader
    {
        private readonly ILogger _Logger;

        public Downloader(ILogger logger)
        {
            _Logger=logger;
        }

        public async Task<string> GetHtmlForPageAsync(string PageUrl, object Data, Dictionary<string, string> Headers)
        {
            try
            {
                if (PageUrl==null)
                    throw new ArgumentInvalidException("PageUrl is null.");

                string UrlParameter = "";

                if (Data!=null)
                    UrlParameter=UrlEncodeParameterGenarator(Data);

                string Url = PageUrl+UrlParameter.Trim('&');

                HttpWebRequest objRequest = (HttpWebRequest)HttpWebRequest.Create(Url);
                objRequest.ContentType="text/html; charset=utf-8";
                objRequest.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/94.0.4606.81 Safari/537.36";
                objRequest.Method="GET";

                if (Headers!=null)
                    foreach (var item in Headers)
                        objRequest.Headers.Add(item.Key, item.Value);

                objRequest.Headers.Add("Accept-charset", "ISO-8859-9,URF-8;q=0.7,*;q=0.7");
                objRequest.Headers["Accept-Encoding"] = "deflate";

                var Response = (HttpWebResponse)await objRequest.GetResponseAsync();
                StreamReader sr = new StreamReader(Response.GetResponseStream(), Encoding.UTF8);

                string Result = await sr.ReadToEndAsync();

                sr.Close();

                return Result;
            }
            catch (ArgumentInvalidException ex)
            {
                _Logger.Debug(ex);
                return null;
            }
            catch (Exception ex)
            {
                _Logger.Error(ex);
                return null;
            }
        }

        /*
         * {Name={"Ali","Hassan"},Family="Ahmadi",Age="25"}
         * ?Name=Ali&Name=Hassan&Family=Ahmnadi&Age=20
         * https://dotnetlearn.com/fa/Login?Name=Ali&Family=Ahmnadi&Age=20
         */

        private string UrlEncodeParameterGenarator(object Data)
        {
            if (Data == null)
                return "";

            var Parameter = GetModelParameters(Data);

            string UrlParameter = "";

            foreach (var item in Parameter)
            {
                if (item.Value != null)
                    UrlParameter += "&" + item.Name + "=" + item.Value;
            }

            UrlParameter="?"+UrlParameter.Trim('&');

            return UrlParameter;
        }
        private List<ParameterItemData> GetModelParameters(object Data)
        {
            if (Data == null)
                return new List<ParameterItemData>();

            Type t = Data.GetType();
            PropertyInfo[] props = t.GetProperties();

            List<ParameterItemData> LstParameter = new List<ParameterItemData>();

            foreach (var prop in props)
            {
                object value = prop.GetValue(Data, new object[] { });
                if (value != null)
                {
                    if (value.GetType() == typeof(string[]))
                        foreach (var item in (string[])value)
                            if (item != null)
                                LstParameter.Add(new ParameterItemData { Name=prop.Name, Value=item });

                    if (value.GetType() == typeof(string))
                        LstParameter.Add(new ParameterItemData { Name=prop.Name, Value=value.ToString() });

                    if (value.GetType() == typeof(int))
                        LstParameter.Add(new ParameterItemData { Name=prop.Name, Value=value.ToString() });

                    if (value.GetType() == typeof(double))
                        LstParameter.Add(new ParameterItemData { Name=prop.Name, Value=value.ToString() });

                    if (value.GetType() == typeof(float))
                        LstParameter.Add(new ParameterItemData { Name=prop.Name, Value=value.ToString() });

                    if (value.GetType() == typeof(long))
                        LstParameter.Add(new ParameterItemData { Name=prop.Name, Value=value.ToString() });

                    if (value.GetType() == typeof(bool))
                        LstParameter.Add(new ParameterItemData { Name=prop.Name, Value=value.ToString() });
                }
            }

            return LstParameter;
        }
    }

    class ParameterItemData
    {
        public string Name { get; set; }
        public string Value { get; set; }
    }
}
