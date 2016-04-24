using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LYSApp.Model
{
    public class PhoneVerificationCode
    {
        public int PhoneVerificationCodeID { get; set; }
        public long UserID { get; set; }
        public string PhoneNumber { get; set; }
        public string VerificationCode { get; set; }
        public string Sid { get; set; }
        public bool IsValid { get; set; }
        public System.DateTime CreatedOn { get; set; }

        public virtual User User { get; set; }
    }
}
