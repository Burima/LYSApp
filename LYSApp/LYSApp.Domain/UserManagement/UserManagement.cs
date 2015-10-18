﻿using System;
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
        private IBaseRepository<Data.DBEntity.UserDetail> userDetailsRepository = null;
        private IBaseRepository<Data.DBEntity.HouseReview> houseReviewRepository = null;
        private IBaseRepository<Data.DBEntity.Bed> bedRepository = null;
        private IBaseRepository<Data.DBEntity.House> houseRepository = null;
        private IBaseRepository<Data.DBEntity.Room> roomRepository = null;
        public UserManagement()
        {
            unitOfWork = new UnitOfWork();
            userRepository = new BaseRepository<Data.DBEntity.User>(unitOfWork);//Initializing userRepository through BaseRepository
            userDetailsRepository = new BaseRepository<Data.DBEntity.UserDetail>(unitOfWork);
            bedRepository = new BaseRepository<Data.DBEntity.Bed>(unitOfWork);
            roomRepository = new BaseRepository<Data.DBEntity.Room>(unitOfWork);
            houseRepository = new BaseRepository<Data.DBEntity.House>(unitOfWork);
            houseReviewRepository = new BaseRepository<Data.DBEntity.HouseReview>(unitOfWork);
        }

        public int UpdateUser(UserViewModel userViewModel)
        {
            //getting the lates data from DB
          
            var dbUser = userRepository.FirstOrDefault(m => m.UserID == userViewModel.UserID);
            dbUser.UserName = userViewModel.UserName;
            dbUser.PhoneNumber = userViewModel.PhoneNumber;
            dbUser.FirstName = userViewModel.FirstName;
            dbUser.LastName = userViewModel.LastName;
            dbUser.Gender = userViewModel.Gender;
            dbUser.ProfilePicture = String.Empty;
            dbUser.Email = userViewModel.Email;
            //Have to update/insert user details based on the requirement
            //if (dbUser != null && dbUser.UserDetails != null && dbUser.UserDetails.Count > 0)
            //{

            //    dbUser.UserDetails.FirstOrDefault().PresentAddress = userViewModel.PresentAddress;
            //    dbUser.UserDetails.FirstOrDefault().PermanentAddress = userViewModel.PermanentAddress;
            //    dbUser.UserDetails.FirstOrDefault().GovtIDType = userViewModel.GovtIDType;
            //    dbUser.UserDetails.FirstOrDefault().GovtID = userViewModel.GovtID;
            //    dbUser.UserDetails.FirstOrDefault().UserProfession = userViewModel.UserProfession;
            //    dbUser.UserDetails.FirstOrDefault().OfficeLocation = userViewModel.OfficeLocation;
            //    dbUser.UserDetails.FirstOrDefault().CurrentEmployer = userViewModel.CurrentEmployer;
            //    dbUser.UserDetails.FirstOrDefault().EmployeeID = userViewModel.EmployeeID;
            //    dbUser.UserDetails.FirstOrDefault().HighestEducation = userViewModel.HighestEducation;
            //    dbUser.UserDetails.FirstOrDefault().InstitutionName = userViewModel.InstitutionName;
            //    dbUser.UserDetails.FirstOrDefault().LastUpdatedOn = DateTime.Now;
            //}
            
            userRepository.Update(dbUser);
           
            return unitOfWork.SaveChanges();
        }

        public int UpdateComment(UserViewModel userviewModel)
        {
            LYSApp.Data.DBEntity.HouseReview houseReview = new LYSApp.Data.DBEntity.HouseReview();
            houseReview.UserID = userviewModel.UserID;
            houseReview.Comments = userviewModel.HouseReviewModel.Comments;
            houseReview.CommentTime = DateTime.Now;
            houseReview.Rating = userviewModel.HouseReviewModel.Rating;
            houseReview.HouseID = userviewModel.HouseReviewModel.HouseID;
            houseReview.User = userRepository.FirstOrDefault(m => m.UserID == userviewModel.UserID);
            houseReview.House = houseRepository.FirstOrDefault(m => m.HouseID == userviewModel.HouseReviewModel.HouseID);
            houseReviewRepository.Insert(houseReview);
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
            return houseIDList.First();
        }

        public int UpdateProfilePicture(UserViewModel userViewModel)
        {
            var dbUser = userRepository.FirstOrDefault(m => m.UserID == userViewModel.UserID);
            dbUser.ProfilePicture = userViewModel.ProfilePicture;

            userRepository.Update(dbUser);
            return unitOfWork.SaveChanges();
        }
    }
}
