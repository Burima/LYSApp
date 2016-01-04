﻿using LYSApp.Model;
using MailChimp;
using MailChimp.Types;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MN = MailChimp.Types.Mandrill;

namespace LYSApp.Domain.NotificationManagement
{
    public class OwnerPropertyListingRequestMailer : IOwnerPropertyListingRequestMailer
    {
        string key = ConfigurationManager.AppSettings["MandrillKey"];        
        public void NotifyUser(OwnerPropertyListingRequestViewModel model)
        {
            var m = new MandrillApi(key);
            //Mail settings for mandrill
            var message = new MN.Messages.Message();
            message.Subject = "Thank you for choosing Lockyoustay for listing your property";
            message.FromEmail = ConfigurationManager.AppSettings["SupportEmailID"];
            message.FromName = "LockYourStay";
            message.To = new[] { new MN.Messages.Recipient(model.Email,model.FirstName) };

            //mergevars for dynamic content in mandrill template
            var globalMergeVars = new Mandrill.Merges();
            globalMergeVars.Add("SUBJECT", message.Subject);
            globalMergeVars.Add("NAME", model.FirstName+" "+model.LastName);
            globalMergeVars.Add("Body", "We have received request for listing your property.We have forwarded your request to our concern team and will update you soon.");

            message.GlobalMergeVars = globalMergeVars; // common information for all receipient

            //dynamic template content
            var templateContent = new List<Mandrill.NameContentPair<string>>();
            templateContent.Add(new Mandrill.NameContentPair<string>("SUBJECT", message.Subject));
            templateContent.Add(new Mandrill.NameContentPair<string>("NAME", model.FirstName + " " + model.LastName));
            templateContent.Add(new Mandrill.NameContentPair<string>("BODY", "We have received request for listing your property.We have forwarded your request to our concern team and will update you soon."));

            //Send mail
            m.SendTemplate("Notify User", templateContent, message);
        }
        public void NotifySuperAdmin(OwnerPropertyListingRequestViewModel model)
        {
            var toList = ConfigurationManager.AppSettings["SuperAdminToEmails"].Split(';');
            var recipientCount = toList.Count();
            var count = 0;

            MN.Messages.Recipient[] recipients = new MN.Messages.Recipient[recipientCount];
            foreach (var to in toList)
            {
                recipients[count] = new MN.Messages.Recipient(to, to);
                count++;
            }
            var m = new MandrillApi(key);
            //Mail settings for mandrill
            var message = new MN.Messages.Message();
            message.Subject =model.Email+ " have contacted you for listing his/her property";
            message.FromEmail = ConfigurationManager.AppSettings["SupportEmailID"];
            message.FromName = "LockYourStay";
            message.To = recipients;

            //mergevars for dynamic content in mandrill template
            var globalMergeVars = new Mandrill.Merges();
            globalMergeVars.Add("SUBJECT", message.Subject);
            globalMergeVars.Add("FIRSTNAME", model.FirstName);
            globalMergeVars.Add("LASTNAME", model.LastName);
            globalMergeVars.Add("EMAIL", model.Email);
            globalMergeVars.Add("MOBILE", model.Mobile);
            globalMergeVars.Add("ADDRESS", model.Address);

            message.GlobalMergeVars = globalMergeVars; // common information for all receipient

            //dynamic template content
            var templateContent = new List<Mandrill.NameContentPair<string>>();
            templateContent.Add(new Mandrill.NameContentPair<string>("SUBJECT", message.Subject));
            templateContent.Add(new Mandrill.NameContentPair<string>("FIRSTNAME", model.FirstName));
            templateContent.Add(new Mandrill.NameContentPair<string>("LASTNAME", model.LastName));
            templateContent.Add(new Mandrill.NameContentPair<string>("EMAIL", model.Email));
            templateContent.Add(new Mandrill.NameContentPair<string>("MOBILE", model.Mobile));
            templateContent.Add(new Mandrill.NameContentPair<string>("ADDRESS", model.Address));

            //Send mail
            m.SendTemplate("Notify SuperAdmin", templateContent, message);
        }
    }
}
