using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using LYSApp.Model;
using LYSApp.Domain.UserManagement;
using LYSApp.Domain;
using LYSApp.Web.Services;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace LYSApp.Web.Controllers
{
    public class UserController : Controller
    {

        private IUserManagement userManagement;
        UserViewModel userViewModel = new UserViewModel();
       
        
        public UserController(UserManagement userManagement)
        {
            this.userManagement = userManagement;//Initializing UserManageManagement
        }

        // GET: User
        public ActionResult ViewProfile()
        {
           
            var user = SessionManager.GetSessionUser();
            if (user != null)
            {
                userViewModel.ManageUserViewModel = new ManageUserViewModel();
                userViewModel.HouseReviewModel = new HouseReviewModel();
                userViewModel.UserID = user.Id;
                userViewModel.PhoneNumber = user.PhoneNumber;
                userViewModel.FirstName = user.FirstName;
                userViewModel.LastName = user.LastName;
                userViewModel.Gender = user.Gender;
                userViewModel.ProfilePicture = user.ProfilePicture;
                userViewModel.ManageUserViewModel.OldPassword = user.PasswordHash;
                userViewModel.Email = user.Email;
                userViewModel.EmailConfirmed = user.EmailConfirmed;
                userViewModel.PhoneNumber = user.PhoneNumber;
                userViewModel.PhoneNumberConfirmed = user.PhoneNumberConfirmed;
                userViewModel.UserName = user.UserName;
                userViewModel.HouseReviewModel.HouseID = userManagement.GetHouseID(user.Id);
                if (user.UserDetails != null && user.UserDetails.Count > 0)
                {
                    userViewModel.PresentAddress = user.UserDetails.FirstOrDefault().PresentAddress;
                    userViewModel.PermanentAddress = user.UserDetails.FirstOrDefault().PermanentAddress;
                    userViewModel.GovtIDType = user.UserDetails.FirstOrDefault().GovtIDType;
                    userViewModel.GovtID = user.UserDetails.FirstOrDefault().GovtID;
                    userViewModel.UserProfession = user.UserDetails.FirstOrDefault().UserProfession;
                    userViewModel.OfficeLocation = user.UserDetails.FirstOrDefault().OfficeLocation;
                    userViewModel.CurrentEmployer = user.UserDetails.FirstOrDefault().CurrentEmployer;
                    userViewModel.EmployeeID = user.UserDetails.FirstOrDefault().EmployeeID;
                    userViewModel.HighestEducation = user.UserDetails.FirstOrDefault().HighestEducation;
                    userViewModel.InstitutionName = user.UserDetails.FirstOrDefault().InstitutionName;
                }

                if (user.HouseReviews != null && user.HouseReviews.Count > 0){
                    userViewModel.houseReviews = user.HouseReviews.ToList();
                }
            }
            return View(userViewModel);
        }

        [HttpPost]
        public ActionResult ViewProfile(UserViewModel userViewModel)
        {
            
            userViewModel.Status = 1;
            userViewModel.LastUpdatedOn = DateTime.Now;
           

            if (userViewModel.UserID == 0)
            {
                //UserController accountController = new UserController((UserManagement)userManagement);
                //accountController.Logout();

            }

            int count = userManagement.UpdateUser(userViewModel);
            if (count > 0)
            {
                var user = SessionManager.GetSessionUser();
                user.ProfilePicture = userViewModel.ProfilePicture;
                TempData["message"] = "Profile updated successfully!";
            }
            else
            {
                TempData["message"] = "Error in updating your profile.Please try again later";
                
            }
            return PartialView("_EditProfile", userViewModel);
        }


        [HttpPost]
        public virtual ActionResult CropImage(string imagePath, decimal? cropPointX, decimal? cropPointY, decimal? imageCropWidth, decimal? imageCropHeight, string fileName)
        {
            if (string.IsNullOrEmpty(imagePath) || !cropPointX.HasValue || !cropPointY.HasValue || !imageCropWidth.HasValue || !imageCropHeight.HasValue)
            {
                return new HttpStatusCodeResult((int)HttpStatusCode.BadRequest);
            }
            byte[] imageBytes = null;
            string[] imageUriPart = imagePath.Split(',');
            string base64String = imageUriPart[1];
            imageBytes = Convert.FromBase64String(base64String);
            byte[] croppedImage = ImageHelper.CropImage(imageBytes, (int)cropPointX.Value, (int)cropPointY.Value, (int)imageCropWidth.Value, (int)imageCropHeight.Value);

            if (!string.IsNullOrEmpty(fileName))
            {
                string[] getID = fileName.Split('_');
                string tempFolderName = Server.MapPath("~/files/croppedImages/" + getID[0]);
                DateTime timestamp = DateTime.Now;
                string filename = getID[0] + String.Format("{0:d-M-yyyy HH-mm-ss}", timestamp);
                FileHelper.SaveFile(croppedImage, tempFolderName, filename);

                string photoPath = string.Concat("../files/croppedImages/", "/" + getID[0], "/" + filename + ".png");
                return Json(new { PhotoPath = photoPath, filename = filename + ".png" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return new HttpStatusCodeResult((int)HttpStatusCode.BadRequest);
            }
        }

        [HttpPost]
        public ActionResult AddComment(UserViewModel userViewModel)
        {
            userManagement.UpdateComment(userViewModel);
            return PartialView("_ReviewComments", userViewModel);
        }

        public ActionResult UpdateProfilePicture(UserViewModel userViewModel)
        {
            int count = userManagement.UpdateProfilePicture(userViewModel);
            return PartialView("_ProfilePicture", userViewModel);

        }
    }
}