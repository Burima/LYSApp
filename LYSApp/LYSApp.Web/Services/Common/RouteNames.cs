using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LYSApp.Web.Services.Common
{
    public class RouteNames
    {
        //Home Controller
        public const string AboutUs = "AboutUs";
        public const string ContactUs = "ContactUs";
        public const string ContactUsPost = "ContactUsPost";
        public const string Jobs = "Jobs";
        public const string TermsAndPrivacy = "TermsAndPrivacy";
        public const string ListYourPropertyPost = "ListYourPropertyPost";

        //Account Controller
        public const string Login = "Login";
        public const string LoginPost = "LoginPost";
        public const string ResendAccountActivationMailForNewUserPost="ResendAccountActivationMailForNewUserPost";
        public const string ForgotPasswordPost = "ForgotPasswordPost";
        //public const string ResetPassword = "ResetPassword";
        //public const string ResetPasswordPost = "ResetPasswordPost";
        
    }
}