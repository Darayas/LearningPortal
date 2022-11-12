using System.Threading.Tasks;

namespace LearningPortal.Framework.Contracts
{
    public interface IEmailSender
    {
        bool SendMail(string To, string Subject, string Message);
        Task SendMailAsync(string To, string Subject, string Message);
    }
}
