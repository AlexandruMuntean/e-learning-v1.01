using E_LearningApplication.CustomExceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;

namespace E_LearningApplication.Utils.MailUtil {
    public class MailUtil: IMailUtil {
        private static string GmailSmtpClient = "smtp.gmail.com";
        private static string GmailUserAddress = "ELearningAppMailService@gmail.com";
        private static string GmailUserPassword = "elearningapp123";
        private static int GmailPort = 587;
        private static bool GmailSsl = true;

        /// <summary>
        /// Sends the email.
        /// </summary>
        /// <param name="receiver">The receiver.</param>
        /// <param name="subject">The subject.</param>
        /// <param name="body">The body.</param>
        /// <param name="sender">The sender.</param>
        /// <exception cref="CustomException"></exception>
        public void SendEmail(string receiver, string subject, string body, string sender = "ELearningAppMailService@gmail.com") {
            try {
                SmtpClient smtpClient = new SmtpClient(GmailSmtpClient);
                smtpClient.Port = GmailPort;
                smtpClient.EnableSsl = GmailSsl;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Timeout = 20000;
                smtpClient.Credentials = new NetworkCredential(GmailUserAddress, GmailUserPassword);

                MailMessage message = new MailMessage(sender, receiver, subject, body);
                smtpClient.Send(message);
            }
            catch (Exception ex) {
                throw new CustomException(ex.Message);
            }
        }
    }
}