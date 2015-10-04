using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LYSApp.Data.DBRepository;
using LYSApp.Model;


namespace LYSApp.Domain.UserManagement
{
    public class UserManagement: IUserManagement
    {
        private IUnitOfWork unitOfWork = null;
        private IBaseRepository<Data.DBEntity.User> userRepository = null;

        public UserManagement()
        {
            unitOfWork = new UnitOfWork();
            userRepository = new BaseRepository<Data.DBEntity.User>(unitOfWork);//Initializing userRepository through BaseRepository
           
        }

        public int UpdateUser(LYSApp.Model.UserViewModel userViewModel)
        {
            //getting the lates data from DB
          
            var dbUser = userRepository.FirstOrDefault(m => m.UserID == userViewModel.UserID);
            dbUser.UserName = userViewModel.UserName;
            dbUser.PhoneNumber = userViewModel.PhoneNumber;
            dbUser.FirstName = userViewModel.FirstName;
            dbUser.LastName = userViewModel.LastName;
            dbUser.Gender = userViewModel.Gender;
            dbUser.ProfilePicture = userViewModel.ProfilePicture;
            dbUser.PasswordHash = userViewModel.Password;
            dbUser.Email = userViewModel.Email;
            
            dbUser.UserDetails.FirstOrDefault().PresentAddress = userViewModel.PresentAddress;
            dbUser.UserDetails.FirstOrDefault().PermanentAddress = userViewModel.PermanentAddress;
            dbUser.UserDetails.FirstOrDefault().GovtIDType = userViewModel.GovtIDType;
            dbUser.UserDetails.FirstOrDefault().GovtID = userViewModel.GovtID;
            dbUser.UserDetails.FirstOrDefault().UserProfession = userViewModel.UserProfession;
            dbUser.UserDetails.FirstOrDefault().OfficeLocation = userViewModel.OfficeLocation;
            dbUser.UserDetails.FirstOrDefault().CurrentEmployer = userViewModel.CurrentEmployer;
            dbUser.UserDetails.FirstOrDefault().EmployeeID = userViewModel.EmployeeID;
            dbUser.UserDetails.FirstOrDefault().HighestEducation = userViewModel.HighestEducation;
            dbUser.UserDetails.FirstOrDefault().InstitutionName = userViewModel.InstitutionName;
            
            userRepository.Update(dbUser);
           
            return unitOfWork.SaveChanges();
        }
    
    }
}
