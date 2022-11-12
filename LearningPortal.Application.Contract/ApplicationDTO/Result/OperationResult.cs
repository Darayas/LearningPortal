using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningPortal.Application.Contract.ApplicationDTO.Result
{
    public class OperationResult
    {
        public bool IsSucceeded { get; set; }
        public string Message { get; set; }

        public OperationResult Succeeded()
        {
            return Succeeded("Operation was Succeeded");
        }

        public OperationResult Succeeded(string _Msg)
        {
            IsSucceeded=true;
            Message=_Msg;

            return this;
        }

        public OperationResult Failed(string _Msg)
        {
            IsSucceeded=false;
            Message=_Msg;

            return this;
        }
    }

    public class OperationResult<T> : OperationResult
    {
        public T Data { get; set; }

        public OperationResult<T> Succeeded(T _Data)
        {
            return Succeeded("Operation was Succeeded", _Data);
        }

        public OperationResult<T> Succeeded(string _Msg,T _Data)
        {
            IsSucceeded=true;
            Message=_Msg;
            Data=_Data;

            return this;
        }

        public OperationResult<T> Failed(string _Message)
        {
            IsSucceeded=false;
            Message=_Message;

            return this;
        }
    }
}
