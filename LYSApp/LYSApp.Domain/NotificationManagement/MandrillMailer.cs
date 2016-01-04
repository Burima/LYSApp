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

        string key = ConfigurationManager.AppSettings["MandrillKey"];
        /// <summary>
        /// This function will add user to Mailchimp Subscription List
        /// </summary>
        /// <param name="emailId">EmailId of the user</param>
        /// <param name="firstName">First Name of the user</param>
        /// <param name="LastName">Last Name of the User</param>
        /// <param name="isSubscribeOnly">Checks if this is only subscribtion . while registering we'll add user to list , but we'll send subscription mail to user..in that case it'll be false.</param>
        /// <returns></returns>
        public string SaveToMailChimpList(string emailId, string firstName, string LastName,bool isSubscribeOnly)
        {
           

            MCApi api = new MCApi(ConfigurationManager.AppSettings["MailChimpApiKey"], false);

            List.Filter lf = new List.Filter();
            lf.ListName = ConfigurationManager.AppSettings["MailChimpListName"];
            List.Lists lists = api.Lists(lf);

            if (lists.Total > 0)
            {
                string listId = lists.Data[0].ListID;

                List.SubscribeOptions so = new List.SubscribeOptions();
                so.DoubleOptIn = false;

                List.Merges merges = new List.Merges();
                merges.Add("FNAME", firstName);
                merges.Add("LNAME", LastName);
                var uservailble = api.ListMemberInfo(listId, emailId);
                if (uservailble.Data[0].Email==null)
                {
                    if (api.ListSubscribe(listId, emailId, merges, so))
                    {
                        //send mail to user
                        if (isSubscribeOnly)
                        {
                            NotifyUser(emailId, "Thanks for showing interest in LockYourStay!", "Thank you for subscribing Lockyourstay.We will keep you posted with all updates", "Notify User");
                        }
                        return "Thank you for your subscription!";
                    }
                    else
                    {
                        return "Something went wrong! Please try again later.";
                    }
                }
                else
                {
                    return "Email already exists!";
                }
            }
            else
            {
                //no list available
                return "Something went wrong! Please try again later.";
            }
           
        }




        //email to reset password, Reset user, user activation
        public void SendEmailForUser(string emailID, string url, string templateName, string subject)
        {
           
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
        public void NotifyUser(string email,string subject,string body,string templateName)
        {
            var m = new MandrillApi(key);
            //Mail settings for mandrill
            var message = new MN.Messages.Message();
            message.Subject = subject;
            message.FromEmail = ConfigurationManager.AppSettings["SupportEmailID"];
            message.FromName = "LockYourStay";
            message.To = new[] { new MN.Messages.Recipient(email, email) };

            //mergevars for dynamic content in mandrill template
            var globalMergeVars = new Mandrill.Merges();
            globalMergeVars.Add("SUBJECT", message.Subject);
            globalMergeVars.Add("FirstName", email);
            globalMergeVars.Add("Body", body);

            message.GlobalMergeVars = globalMergeVars; // common information for all receipient

            //dynamic template content
            var templateContent = new List<Mandrill.NameContentPair<string>>();
            templateContent.Add(new Mandrill.NameContentPair<string>("SUBJECT", message.Subject));
            templateContent.Add(new Mandrill.NameContentPair<string>("FirstName", email));
            templateContent.Add(new Mandrill.NameContentPair<string>("Body", body));

            //Send mail
            m.SendTemplate(templateName, templateContent, message);
        }
        
    }
}
