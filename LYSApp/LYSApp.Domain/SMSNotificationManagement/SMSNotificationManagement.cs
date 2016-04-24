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
        private IBaseRepository<Data.DBEntity.PhoneVerificationCode> phoneVerificationCodeRepository = null;
        public SMSNotificationManagement()
        {
            _AccountSid = ConfigurationManager.AppSettings["Twilio:AccountSID"];
            _AuthToken = ConfigurationManager.AppSettings["Twilio:AuthToken"];
            _Sendnumber = ConfigurationManager.AppSettings["Twilio:SendNumber"];
            unitOfWork = new UnitOfWork();
            phoneVerificationCodeRepository = new BaseRepository<Data.DBEntity.PhoneVerificationCode>(unitOfWork);//Initializing userRepository through BaseRepository
        }

        //need to move this to Facade after MVP2
        //due to short span of time hacked it here
        public string SendPhoneVerificationCode(string PhoneNumber,long UserID)
        {
            try
            {
                    // Step 1:
                    string guid = Guid.NewGuid().GetHashCode().ToString().Substring(0, 6);

                    var twilio = new TwilioRestClient(_AccountSid, _AuthToken);
                    var message = twilio.SendSmsMessage(_Sendnumber, PhoneNumber, "Ahoy from Lockyourstay! Your Phone verification code is " + guid +".");
                    string sId = message.Sid;

                    // Step 2: Make all the valid phone number as invail which matches the incoming phone number
                    var PhoneNumbers = phoneVerificationCodeRepository.Where(x => x.PhoneNumber == PhoneNumber && x.IsValid == true && x.UserID==UserID).ToList();
                    if (PhoneNumbers != null && PhoneNumbers.Count > 0)
                    {
                        foreach (var phoneNumber in PhoneNumbers)
                        {
                            phoneNumber.IsValid = false;
                            phoneVerificationCodeRepository.Update(phoneNumber);
                        }
                    }

                    // Step 3:  insert phone verification code in db        
                    var dbPhoneVerificationCode = new Data.DBEntity.PhoneVerificationCode();
                    dbPhoneVerificationCode.PhoneNumber = PhoneNumber;
                    dbPhoneVerificationCode.VerificationCode = guid;
                    dbPhoneVerificationCode.IsValid = true;
                    dbPhoneVerificationCode.UserID = UserID;
                    dbPhoneVerificationCode.CreatedOn = DateTime.Now;
                    dbPhoneVerificationCode.Sid = sId;
                    phoneVerificationCodeRepository.Insert(dbPhoneVerificationCode);                  

                    return sId;
                
            }
            catch (Exception ex)
            {
                //ExceptionHandler.LogException(ex);
                return "exception_logged";
            }
        }

        //public IList<Data.DBEntity.User> GetUsersByPhoneNumber(string PhoneNumber)
        //{
        //    return userRepository.Get(x => x.PhoneNumber == PhoneNumber).ToList();
        //}

        ////update user from management only
        //public int UpdateUser(Data.DBEntity.User user)
        //{
        //    //getting the lates data from DB
        //    userRepository.Update(user);

        //    return unitOfWork.SaveChanges();
        //}
    }
}
