using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace LYSApp.Web.Services.Common
{
    public sealed class LYSConfig
    {
        //
        public static string EnvironmentName = ConfigurationManager.AppSettings.Get("EnvironmentName").ToString();
        //Social Links
        public static string BlogUrl = ConfigurationManager.AppSettings.Get("BlogUrl").ToString();
        public static string FacebookUrl = ConfigurationManager.AppSettings.Get("FacebookUrl").ToString();
        public static string TwitterUrl = ConfigurationManager.AppSettings.Get("TwitterUrl").ToString();
        public static string GooglePlusUrl = ConfigurationManager.AppSettings.Get("GooglePlusUrl").ToString();
        public static string LinkedinUrl = ConfigurationManager.AppSettings.Get("LinkedinUrl").ToString();
        //Email service
        public static string MandrillKey = ConfigurationManager.AppSettings.Get("MandrillKey").ToString();
        public static string SupportEmailID = ConfigurationManager.AppSettings.Get("SupportEmailID").ToString();
        //Subscription
        public static string MailChimpApiKey = ConfigurationManager.AppSettings.Get("MailChimpApiKey").ToString();
        public static string MailChimpListName = ConfigurationManager.AppSettings.Get("MailChimpListName").ToString();
        //Security
        public static string EncryptionKey = ConfigurationManager.AppSettings.Get("EncryptionKey").ToString();

    }
}