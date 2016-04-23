using LYSApp.Data.DBRepository;
using LYSApp.Domain.UserManagement;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twilio;

namespace LYSApp.Domain.SMSNotificationManagement
{
    public class SMSNotificationManagement : ISMSNotificationManagement
    {
        private readonly string _AccountSid;
        private readonly string _AuthToken;
        private readonly string _Sendnumber;
        private IUnitOfWork unitOfWork = null;
        private IBaseRepository<Data.DBEntity.User> userRepository = null;
        public SMSNotificationManagement()
        {
            _AccountSid = ConfigurationManager.AppSettings["Twilio:AccountSID"];
            _AuthToken = ConfigurationManager.AppSettings["Twilio:AuthToken"];
            _Sendnumber = ConfigurationManager.AppSettings["Twilio:SendNumber"];
            unitOfWork = new UnitOfWork();
            userRepository = new BaseRepository<Data.DBEntity.User>(unitOfWork);//Initializing userRepository through BaseRepository
        }
        public string SendPhoneVerificationCode(string PhoneNumber,long UserID)
        {
            try
            {
                    // Step 1:
                    string guid = Guid.NewGuid().ToString("N").Substring(0, 6);

                    var twilio = new TwilioRestClient(_AccountSid, _AuthToken);
                    var message = twilio.SendSmsMessage(_Sendnumber, PhoneNumber, "Ahoy from Lockyourstay! Your Phone verification code is " + guid +".");
                    string sId = message.Sid;

                    // Step 2:
                    var usersHavingSamePhoneNumber = GetUsersByPhoneNumber(PhoneNumber);
                    if (usersHavingSamePhoneNumber != null && usersHavingSamePhoneNumber.Count>0)
                    {
                        foreach (var user in usersHavingSamePhoneNumber)
                        {
                            //user.PhoneNumber = null;
                            user.PhoneNumberConfirmed = false;
                            UpdateUser(user);
                        }
                    }

                    // Step 3:   Update User table with new phone number and verification code         
                    var dbUser = userRepository.Get(x => x.UserID == UserID).FirstOrDefault();
                    dbUser.PhoneNumber = PhoneNumber;
                    dbUser.PhoneVerificationCode = guid;
                    UpdateUser(dbUser);

                    //Check User phonenumber is available for someother users

                    return sId;
                
            }
            catch (Exception ex)
            {
                //ExceptionHandler.LogException(ex);
                return "exception_logged";
            }
        }

        public IList<Data.DBEntity.User> GetUsersByPhoneNumber(string PhoneNumber)
        {
            return userRepository.Get(x => x.PhoneNumber == PhoneNumber).ToList();
        }

        //update user from management only
        public int UpdateUser(Data.DBEntity.User user)
        {
            //getting the lates data from DB
            userRepository.Update(user);

            return unitOfWork.SaveChanges();
        }
    }
}
