using LYSApp.Data.DBRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace LYSApp.Domain.PhoneVerificationCodeManagement
{
    public class PhoneVerificationCodeManagement:IPhoneVerificationCodeManagement
    {
        private IUnitOfWork unitOfWork = null;
        private IBaseRepository<Data.DBEntity.PhoneVerificationCode> phoneVerificationCodeRepository = null;
        public PhoneVerificationCodeManagement()
        {
            unitOfWork = new UnitOfWork();
            phoneVerificationCodeRepository = new BaseRepository<Data.DBEntity.PhoneVerificationCode>(unitOfWork);//Initializing userRepository through BaseRepository
            Mapper.CreateMap<Data.DBEntity.PhoneVerificationCode, Model.PhoneVerificationCode>();
        }
        public Model.PhoneVerificationCode GetPhoneVerificationCode(string phoneNumber, string verificationCode,long userID)
        {
            var phoneVerificationCode = phoneVerificationCodeRepository.Where(x => x.PhoneNumber == phoneNumber && x.UserID == userID && x.VerificationCode == verificationCode && x.IsValid == true).FirstOrDefault();
            return Mapper.Map<Data.DBEntity.PhoneVerificationCode,Model.PhoneVerificationCode>(phoneVerificationCode);
        }
    }
}
