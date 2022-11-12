using LearningPortal.Framework.Contracts;
using System;
using System.ComponentModel;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace LearningPortal.Framework.Services.Email
{
    public class DnlnSender : IEmailSender
    {
        private readonly string SenderTitle;
        private readonly string UserName;
        private readonly string Password;
        private readonly int Port;
        private readonly bool UseSSL;

        private readonly ILogger _Logger;
        public DnlnSender(ILogger logger)
        {
            _Logger=logger;

            SenderTitle = "LearningPortal";
            UserName = "LearningPortal@dotnetlearn.com";
            Password = "123456Aa!@#";
            Port = 25;
            UseSSL = false;
        }

        public bool SendMail(string To, string Subject, string Message)
        {
            try
            {
                MailMessage mail = new MailMessage();
                mail.From= new MailAddress(UserName, SenderTitle, Encoding.UTF8);
                mail.To.Add(new MailAddress(To));
                mail.Subject= Subject;
                mail.Body=Message;
                mail.IsBodyHtml= true;
                mail.BodyEncoding=Encoding.UTF8;
                mail.Priority=MailPriority.Normal;

                SmtpClient smtp = new SmtpClient("mail.dotnetlearn.com", Port);
                smtp.Credentials= new NetworkCredential(UserName, Password);
                smtp.EnableSsl=UseSSL;
                smtp.Send(mail);

                return true;
            }
            catch (Exception ex)
            {
                _Logger.Error(ex);
                return false;
            }
        }

        public async Task SendMailAsync(string To, string Subject, string Message)
        {
            try
            {
                MailMessage mail = new MailMessage();
                mail.From= new MailAddress(UserName, SenderTitle, Encoding.UTF8);
                mail.To.Add(new MailAddress(To));
                mail.Subject= Subject;
                mail.Body=Message;
                mail.IsBodyHtml= true;
                mail.BodyEncoding=Encoding.UTF8;
                mail.Priority=MailPriority.Normal;

                SmtpClient smtp = new SmtpClient("mail.dotnetlearn.com", Port);
                smtp.Credentials= new NetworkCredential(UserName, Password);
                smtp.EnableSsl=UseSSL;

                smtp.SendCompleted+= new SendCompletedEventHandler(SendCompletedCallback);
                smtp.SendAsync(mail, To);
            }
            catch (Exception ex)
            {
                _Logger.Error(ex);
            }
        }

        private void SendCompletedCallback(object sender, AsyncCompletedEventArgs e)
        {
            string Token = (string)e.UserState;
            if (e.Cancelled)
            {

            }
            else if (e.Error!=null)
            {
                _Logger.Error(new Exception($"Token: [{Token}], Error: [{e.Error.Message}]", e.Error));
            }
            else
            {
                // Success
            }
        }
    }
}
