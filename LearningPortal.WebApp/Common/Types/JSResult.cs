using Microsoft.AspNetCore.Mvc;

namespace LearningPortal.WebApp.Common.Types
{
    public class JSResult : ContentResult
    {
        public JSResult(string Script)
        {
            Content = Script;
            ContentType = "application/javascript";
        }
    }
}
