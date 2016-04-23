using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LYSApp.Data.DBRepository;
using LYSApp.Model;
using System.Collections;



namespace LYSApp.Domain.UserManagement
{
    public class UserManagement: IUserManagement
    {
        private IUnitOfWork unitOfWork = null;
        private IBaseRepository<Data.DBEntity.User> userRepository = null;
        private IBaseRepository<Data.DBEntity.UserDetail> userDetailsRepository = null;
        private IBaseRepository<Data.DBEntity.PGReview> pgReviewRepository = null;
        private IBaseRepository<Data.DBEntity.Bed> bedRepository = null;
        private IBaseRepository<Data.DBEntity.House> houseRepository = null;
        private IBaseRepository<Data.DBEntity.Room> roomRepository = null;
        private IBaseRepository<Data.DBEntity.PGDetail> pgDetailRepository = null;
       
        public UserManagement()
        {
            unitOfWork = new UnitOfWork();
            userRepository = new BaseRepository<Data.DBEntity.User>(unitOfWork);//Initializing userRepository through BaseRepository
            userDetailsRepository = new BaseRepository<Data.DBEntity.UserDetail>(unitOfWork);
            bedRepository = new BaseRepository<Data.DBEntity.Bed>(unitOfWork);
            roomRepository = new BaseRepository<Data.DBEntity.Room>(unitOfWork);
            houseRepository = new BaseRepository<Data.DBEntity.House>(unitOfWork);
            pgReviewRepository = new BaseRepository<Data.DBEntity.PGReview>(unitOfWork);
            pgDetailRepository = new BaseRepository<Data.DBEntity.PGDetail>(unitOfWork);
            bedRepository = new BaseRepository<Data.DBEntity.Bed>(unitOfWork);
           
        }

        public int UpdateUser(UserViewModel userViewModel)
        {
            //getting the lates data from DB
          
            var dbUser = userRepository.FirstOrDefault(m => m.UserID == userViewModel.UserID);
            if (dbUser != null){
                    dbUser.UserName = userViewModel.UserName;
                    dbUser.PhoneNumber = userViewModel.PhoneNumber;
                    dbUser.FirstName = userViewModel.FirstName;
                    dbUser.LastName = userViewModel.LastName;
                    dbUser.Gender = userViewModel.Gender;
                    dbUser.ProfilePicture = String.Empty;
                    dbUser.Email = userViewModel.Email;

                if(dbUser.UserDetails != null && dbUser.UserDetails.Count > 0){
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
                    dbUser.UserDetails.FirstOrDefault().LastUpdatedOn = DateTime.Now;
                }
                else
                {
                    LYSApp.Data.DBEntity.UserDetail userDetail = new LYSApp.Data.DBEntity.UserDetail();
                    userDetail.PresentAddress = userViewModel.PresentAddress;
                    userDetail.PermanentAddress = userViewModel.PermanentAddress;
                    userDetail.GovtIDType = userViewModel.GovtIDType;
                    userDetail.GovtID = userViewModel.GovtID;
                    userDetail.UserProfession = userViewModel.UserProfession;
                    userDetail.OfficeLocation = userViewModel.OfficeLocation;
                    userDetail.CurrentEmployer = userViewModel.CurrentEmployer;
                    userDetail.EmployeeID = userViewModel.EmployeeID;
                    userDetail.HighestEducation = userViewModel.HighestEducation;
                    userDetail.InstitutionName = userViewModel.InstitutionName;
                    userDetail.CreatedOn = DateTime.Now;
                    dbUser.UserDetails = new List<LYSApp.Data.DBEntity.UserDetail>();
                    dbUser.UserDetails.Add(userDetail);
                    userDetailsRepository.Insert(userDetail);
                }
            }

            userRepository.Update(dbUser);
           
            return unitOfWork.SaveChanges();
        }

        public int UpdateComment(UserViewModel userviewModel)
        {
            LYSApp.Data.DBEntity.PGReview pgReview = new LYSApp.Data.DBEntity.PGReview();
            pgReview.UserID = userviewModel.UserID;
            pgReview.Comments = userviewModel.HouseReviewModel.Comments;
            pgReview.CommentTime = DateTime.Now;
            pgReview.Rating = userviewModel.HouseReviewModel.Rating;
            /**------commenting code due to DBUpdate**/
            pgReview.PGDetailID = (from pg in pgDetailRepository.Get()
                                   join h in houseRepository.Get() on pg.PGDetailID equals h.PGDetailID
                                   join r in roomRepository.Get() on h.HouseID equals r.HouseID
                                   join b in bedRepository.Get() on r.RoomID equals b.RoomID
                                   join u in userRepository.Get() on b.UserID equals u.UserID
                                   select pg.PGDetailID).FirstOrDefault();
            pgReviewRepository.Insert(pgReview);
            return unitOfWork.SaveChanges();
        }

        public int GetHouseID(long userID)
        {

            IList<int> roomIDList = (from b in bedRepository
                                     where b.UserID == userID
                                      select b.RoomID).ToList();
            IList<int> houseIDList = (from r in roomRepository
                                      where roomIDList.Contains(r.RoomID)
                                     select r.HouseID).ToList();
            return houseIDList.Count>0? houseIDList.FirstOrDefault():0;
        }

        public int UpdateProfilePicture(long UserID, string ProfilePicture)
        {
            var dbUser = userRepository.FirstOrDefault(m => m.UserID == UserID);
            dbUser.ProfilePicture = ProfilePicture;

            userRepository.Update(dbUser);
            return unitOfWork.SaveChanges();
        }
    }
}
