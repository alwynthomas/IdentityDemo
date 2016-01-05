using System;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;

namespace IdentityDemo.Identity
{
    internal class UserEmailService : IIdentityMessageService
    {
        public async Task SendAsync(IdentityMessage message)
        {
            var eMail = BuildEmail(message.Destination, GetConfig("EmailsFromAddress"), GetConfig("EmailsFrom"), message.Subject, message.Body);
            var smtpClient = BuildClient();

            await smtpClient.SendMailAsync(eMail);
        }

        private SmtpClient BuildClient()
        {
            var smtpClient = new SmtpClient();

            smtpClient.Port = int.Parse(GetConfig("SmtpPort"));
            smtpClient.Host = GetConfig("SmtpHost");

            smtpClient.Credentials = new NetworkCredential() { UserName = GetConfig("SmtpUsername"), Password = GetConfig("SmtpPassword") };

            return smtpClient;
        }

        private MailMessage BuildEmail(string toAddress, string fromAddress, string fromName, string subject, string msgBody)
        {
            var eMail = new MailMessage();

            eMail.To.Add(toAddress);
            eMail.From = new MailAddress(fromAddress, fromName);

            eMail.Subject = subject;
            eMail.Body = msgBody;
            eMail.IsBodyHtml = true;

            return eMail;
        }

        private string GetConfig(string k)
        {
            return System.Configuration.ConfigurationManager.AppSettings[k].ToString();
        }

    }
}