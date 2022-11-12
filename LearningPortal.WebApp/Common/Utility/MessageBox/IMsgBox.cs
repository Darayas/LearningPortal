using LearningPortal.WebApp.Common.Types;

namespace LearningPortal.WebApp.Common.Utility.MessageBox
{
    public interface IMsgBox
    {
        JSResult AccessDeniedMsg(string CallBackFunction = "function(){location.reload();}");
        JSResult ErrorDefMsg(string CallBackFunction = null);
        JSResult ErrorMsg(string? Message = null, string? CallBackFunction = null);
        JSResult InfoMsg(string Message, string? CallBackFunction = null);
        JSResult ModelStateMsg(string Message, string? CallBackFunction = null);
        JSResult SuccessMsg(string? Message = null, string? CallBackFunction = null);
        JSResult WarningMsg(string Message, string? CallBackFunction = null);
    }
}