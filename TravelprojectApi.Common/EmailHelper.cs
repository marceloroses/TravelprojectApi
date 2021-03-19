using System;
using System.Net;
using System.Net.Mail;

namespace TravelprojectApi.Common
{
    public class EmailHelper
    {
        private string _host;
        private string _port;
        private string _from;
        private string _alias;
        private string _password;

        public EmailHelper(string host, string port, string from, string alias, string password)
        {

            _host = host;
            _port = port;
            _from = from;
            _alias = alias;
            _password = password;

        }

        public void SendEmail(EmailModel emailModel)
        {
            try
            {
                using (SmtpClient client = new SmtpClient(_host, int.Parse(_port)))
                {
                    string[] toList = emailModel.To.Split(';');
                    client.Credentials = new NetworkCredential(_from, _password);    
                    client.EnableSsl = true;
                    client.UseDefaultCredentials = true;

                    MailMessage mailMessage = new MailMessage();
                    mailMessage.From = new MailAddress(_from, _alias);
                    //mailMessage.BodyEncoding = Encoding.;
                    foreach (var emailTo in toList)
                    {
                        mailMessage.To.Add(emailTo);
                    }

                    mailMessage.Body = emailModel.Message;
                    mailMessage.Subject = emailModel.Subject;
                    mailMessage.IsBodyHtml = emailModel.IsBodyHtml;
                    client.Send(mailMessage);
                }
            }
            catch
            {
                throw;
            }
        }
    }
}
