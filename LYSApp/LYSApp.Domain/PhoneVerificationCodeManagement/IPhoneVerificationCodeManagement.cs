using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LYSApp.Domain.PhoneVerificationCodeManagement
{
    public interface IPhoneVerificationCodeManagement
    {
        Model.PhoneVerificationCode GetPhoneVerificationCode(string phoneNumber, string verificationCode, long userID);
    }
}
