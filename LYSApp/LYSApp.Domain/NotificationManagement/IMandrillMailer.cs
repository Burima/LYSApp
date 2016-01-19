using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LYSApp.Domain.NotificationManagement
{
    public interface IMandrillMailer
    {
        string SaveToMailChimpList(string emailId, string firstName, string LastName, bool isSubscribeOnly);
        void SendEmailForUser(string emailID, string url, string templateName, string subject);
        void NotifyUser(string email, string name, string subject, string body, string templateName);
        void NotifySuperAdmin(string email, string subject, string body, string templateName);
    }
}
