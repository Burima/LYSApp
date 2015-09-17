using MailChimp;
using MailChimp.Types;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MN = MailChimp.Types.Mandrill;

namespace LYSApp.Domain.NotificationManagement
{
    public class MandrillMailer
    {
        //email to reset password, Reset user, user activation
        public async Task SendEmailForUser(string emailID, string url, string templateName, string subject)
        {
            var key = ConfigurationManager.AppSettings["MandrillKey"];
            var m = new MandrillApi(key);

            //Mail settings for mandrill
            var message = new MN.Messages.Message();
            message.Subject = subject;
            message.FromEmail = ConfigurationManager.AppSettings["SupportEmailID"];
            message.FromName = "LockYourStay";
            message.To = new[] { new MN.Messages.Recipient(emailID, emailID) };

            //mergevars for dynamic content in mandrill template
            var globalMergeVars = new Mandrill.Merges();
            globalMergeVars.Add("SUBJECT", subject);
            globalMergeVars.Add("URL", url);
            
            message.GlobalMergeVars = globalMergeVars; // common information for all receipient

            //dynamic template content
            var templateContent = new List<Mandrill.NameContentPair<string>>();
            templateContent.Add(new Mandrill.NameContentPair<string>("SUBJECT", subject));
            templateContent.Add(new Mandrill.NameContentPair<string>("URL", url));           

            //Send mail
            m.SendTemplate(templateName, templateContent, message);
        }

        
    }
}
