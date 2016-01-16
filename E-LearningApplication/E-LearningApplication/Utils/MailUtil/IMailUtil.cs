using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace E_LearningApplication.Utils.MailUtil {
    public interface IMailUtil {
        /// <summary>
        /// Sends the email.
        /// </summary>
        /// <param name="receiver">The receiver.</param>
        /// <param name="subject">The subject.</param>
        /// <param name="body">The body.</param>
        /// <exception cref="CustomException"></exception>
        void SendEmail(string receiver, string subject, string body);
    }
}
