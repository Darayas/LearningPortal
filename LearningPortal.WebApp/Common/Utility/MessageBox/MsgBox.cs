using LearningPortal.Framework.Contracts;
using LearningPortal.WebApp.Common.Types;

namespace LearningPortal.WebApp.Common.Utility.MessageBox
{
    public class MsgBox : IMsgBox
    {
        private readonly ILocalizer _localizer;
        public MsgBox(ILocalizer localizer)
        {
            _localizer = localizer;
        }
        private string Show(string Title, string Message, MsgBoxType Type, string? CallBackFunction = null)
        {
            CallBackFunction = CallBackFunction ?? "return '';";

            string js = $@"swal.fire({{ 
                title: '{Title.Replace("'", "`")}',
                html: '{Message.Replace("'", "`")}',
                icon: '{Type}',
                confirmButtonText: '{_localizer["Ok"]}'
            }}).then((result) => {{ 
                if (result.isConfirmed) {{
                    {CallBackFunction}
                }}
            }});";

            return js;
        }

        public JSResult SuccessMsg(string? Message = null, string? CallBackFunction = null)
        {
            Message = Message ?? "Operation was succeded";

            return new JSResult(Show("", Message, MsgBoxType.success, CallBackFunction));
        }

        public JSResult WarningMsg(string Message, string? CallBackFunction = null)
        {
            return new JSResult(Show("", Message, MsgBoxType.warning, CallBackFunction));
        }

        public JSResult InfoMsg(string Message, string? CallBackFunction = null)
        {
            return new JSResult(Show("", Message, MsgBoxType.info, CallBackFunction));
        }

        public JSResult ErrorMsg(string? Message = null, string? CallBackFunction = null)
        {
            Message = Message ?? _localizer["Operation was failure"];
            Message = Message.Replace(" , ", "<br/>");

            return new JSResult(Show("", Message, MsgBoxType.error, CallBackFunction));
        }

        public JSResult ErrorDefMsg(string? CallBackFunction = null)
        {
            string Message = _localizer["Error500"];

            return new JSResult(Show("", Message, MsgBoxType.error, CallBackFunction));
        }

        public JSResult ModelStateMsg(string Message, string? CallBackFunction = null)
        {
            return new JSResult(Show("", Message, MsgBoxType.error, CallBackFunction));
        }

        public JSResult AccessDeniedMsg(string CallBackFunction = "function(){location.reload();}")
        {
            return new JSResult(Show("", _localizer["Access Denied"], MsgBoxType.error, CallBackFunction));
        }
    }

    public enum MsgBoxType
    {
        success,
        warning,
        info,
        error
    }
}
