using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LYSApp.Domain.SMSNotificationManagement
{
    public interface ISMSNotificationManagement
    {
        string SendPhoneVerificationCode(string PhoneNumber, long UserID);
    }
}
