using LYSApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using LYSApp.Web.Services;
using System.Configuration;
using LYSApp.Web.Services.Common;
using LYSApp.Domain.TransactionManagement;
using LYSApp.Model.Constants;

namespace LYSApp.Web.Controllers
{
    public class PaymentController : Controller
    {
        private ITransactionManagement transactionManagement;
        public PaymentController(TransactionManagement transactionManagement)
        {
            this.transactionManagement = transactionManagement;
        }
        // POST: Payment/RedirectToPayumoney
        [HttpPost]
        public void RedirectToPayumoney(BookingDetailsViewModel bookingDetailsViewModel)
        {

            var user = SessionManager.GetSessionUser();
            var searchCriteria = (SearchViewModel)Session["SearchCriteria"];
            if (user == null && user.Id == 0)
            {
                //user session time out 
            }
            else if (searchCriteria == null && searchCriteria.BookingFromDate == null)
            {
                //search criteria is removed...redirect user for search again
            }
            else
            {
                string txnid = Generatetxnid();
                //transaction management
                if (transactionManagement.AddTransaction(bookingDetailsViewModel, txnid, user.Id, searchCriteria.BookingFromDate) > 1)
                {
                    string firstName = user.FirstName;
                    string amount = bookingDetailsViewModel.Price.ToString();
                    string productInfo = bookingDetailsViewModel.RoomID.ToString();
                    string email = user.Email;
                    string phone = user.PhoneNumber;
                    string surl = Url.Action("Payment", "Success");
                    string furl = Url.Action("Payment", "Success");


                    RemotePost myremotepost = new RemotePost();

                    string key = LYSConfig.PayUmoneyKey;
                    string salt = LYSConfig.PayUmoneySalt;

                    //posting all the parameters required for integration.
                    myremotepost.Url = LYSConfig.PayUmoneyRedirectURL;
                    myremotepost.Add("key", key);
                    myremotepost.Add("txnid", txnid);
                    myremotepost.Add("amount", bookingDetailsViewModel.Price.ToString());
                    myremotepost.Add("productinfo", productInfo);
                    myremotepost.Add("firstname", firstName);
                    myremotepost.Add("phone", phone);
                    myremotepost.Add("email", email);
                    myremotepost.Add("surl", surl);//Change the success url here depending upon the port number of your local system.
                    myremotepost.Add("furl", furl);//Change the failure url here depending upon the port number of your local system.
                    myremotepost.Add("service_provider", "payu_paisa");
                    string hashString = key + "|" + txnid + "|" + amount + "|" + productInfo + "|" + firstName + "|" + email + "|||||||||||" + salt;
                    //string hashString = "3Q5c3q|2590640|3053.00|OnlineBooking|vimallad|ladvimal@gmail.com|||||||||||mE2RxRwx";
                    string hash = Generatehash512(hashString);
                    myremotepost.Add("hash", hash);

                    myremotepost.Post();
                }
                else
                {
                    //bed not available
                }
            }

        }


        // POST: Payment/ReturnFromPayumoney
        [HttpPost]
        public void ReturnFromPayumoney(FormCollection form)
        {
            Transaction transaction = new Transaction()
            {
                OrderID = form["txnid"],
                mode = form["mode"],
                TransactionStatusID = (int)Constants.Transaction_Status.failure,
                amount = Convert.ToDecimal(form["amount"]),
                Error = form["Error"],
                PG_TYPE = form["PG_TYPE"],
                bank_ref_num = form["bank_ref_num"],
                payuMoneyId = form["payuMoneyId"],
                additionalCharges = form["additionalCharges"],
                IsValidTransaction = false
            };
            try
            {
                string[] merc_hash_vars_seq;
                string merc_hash_string = string.Empty;
                string merc_hash = string.Empty;
                string order_id = string.Empty;
                string hash_seq = "key|txnid|amount|productinfo|firstname|email|udf1|udf2|udf3|udf4|udf5|udf6|udf7|udf8|udf9|udf10";
               
                if (form["status"].ToString() == "success")
                {
                    //TransactionStatusID 
                    transaction.TransactionStatusID = (int)Constants.Transaction_Status.success;
                    merc_hash_vars_seq = hash_seq.Split('|');
                    Array.Reverse(merc_hash_vars_seq);
                    //merc_hash_string = ConfigurationManager.AppSettings["SALT"] + "|" + form["status"].ToString();//without Convenience Fee Model
                    merc_hash_string = form["additionalCharges"].ToString() + "|" + ConfigurationManager.AppSettings["SALT"] + "|" + form["status"].ToString();//with Convenience Fee Model


                    foreach (string merc_hash_var in merc_hash_vars_seq)
                    {
                        merc_hash_string += "|";
                        merc_hash_string = merc_hash_string + (form[merc_hash_var] != null ? form[merc_hash_var] : "");

                    }
                    Response.Write(merc_hash_string);
                    merc_hash = Generatehash512(merc_hash_string).ToLower();

                    //if hash matches it means a valid transaction
                    if (merc_hash == form["hash"])
                    {
                        transaction.IsValidTransaction = true;

                    }
                    
                }               
               
            }
            catch (Exception ex)
            {
                Response.Write("<span style='color:red'>" + ex.Message + "</span>");

            }
            finally
            {
                //update Transaction Status and Bed Status
                transactionManagement.UpdateTransaction(transaction);
            }
        }


        public class RemotePost
        {
            private System.Collections.Specialized.NameValueCollection Inputs = new System.Collections.Specialized.NameValueCollection();


            public string Url = "";
            public string Method = "post";
            public string FormName = "form1";

            public void Add(string name, string value)
            {
                Inputs.Add(name, value);
            }

            public void Post()
            {
                System.Web.HttpContext.Current.Response.Clear();

                System.Web.HttpContext.Current.Response.Write("<html><head>");

                System.Web.HttpContext.Current.Response.Write(string.Format("</head><body onload=\"document.{0}.submit()\">", FormName));
                System.Web.HttpContext.Current.Response.Write(string.Format("<form name=\"{0}\" method=\"{1}\" action=\"{2}\" >", FormName, Method, Url));
                for (int i = 0; i < Inputs.Keys.Count; i++)
                {
                    System.Web.HttpContext.Current.Response.Write(string.Format("<input name=\"{0}\" type=\"hidden\" value=\"{1}\">", Inputs.Keys[i], Inputs[Inputs.Keys[i]]));
                }
                System.Web.HttpContext.Current.Response.Write("</form>");
                System.Web.HttpContext.Current.Response.Write("</body></html>");

                System.Web.HttpContext.Current.Response.End();
            }
        }


        //Hash generation Algorithm

        public string Generatehash512(string text)
        {

            byte[] message = Encoding.UTF8.GetBytes(text);

            UnicodeEncoding UE = new UnicodeEncoding();
            byte[] hashValue;
            SHA512Managed hashString = new SHA512Managed();
            string hex = "";
            hashValue = hashString.ComputeHash(message);
            foreach (byte x in hashValue)
            {
                hex += String.Format("{0:x2}", x);
            }
            return hex;

        }


        public string Generatetxnid()
        {
            Random rnd = new Random();
            string strHash = Generatehash512(rnd.ToString() + DateTime.Now);
            //string txnid1 = strHash.ToString().Substring(0, 20);

            return strHash.ToString().Substring(0, 20);
        }
    }
}