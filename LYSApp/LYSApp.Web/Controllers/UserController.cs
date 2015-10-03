using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using LYSApp.Model;
using LYSApp.Data;
using LYSApp.Domain.UserManagement;
using LYSApp.Domain;

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

        public UserController()
        {

        }
        // GET: User
        public ActionResult ViewProfile()
        {
            if (Session["User"] != null)
            {
                var user = (LYSApp.Data.DBEntity.User)Session["User"];
                userViewModel.UserID = user.UserID;
                userViewModel.PhoneNumber = user.PhoneNumber;
                userViewModel.FirstName = user.FirstName;
                userViewModel.LastName = user.LastName;
                userViewModel.Gender = user.Gender;
                userViewModel.ProfilePicture = user.ProfilePicture;
                userViewModel.PasswordHash = user.PasswordHash;
                userViewModel.Email = user.Email;
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
            return View(userViewModel);
        }

        [HttpPost]
        public ActionResult ViewProfile(UserViewModel userViewModel)
        {
            userViewModel.Status = 1;
            userViewModel.LastUpdatedOn = DateTime.Now;
            userViewModel.UserID = TempData["UserID"] != null ? Convert.ToInt32(TempData["UserID"]) : 0;

            if (userViewModel.UserID == 0)
            {
                //UserController accountController = new UserController((UserManagement)userManagement);
                //accountController.Logout();

            }

            int count = userManagement.UpdateUser(userViewModel);
            if (count > 0)
            {
                var user = (User)Session["User"];
                user.ProfilePicture = userViewModel.ProfilePicture;
                TempData["message"] = "Profile updated successfully!";
            }
            else
            {
                TempData["message"] = "Error in updating your profile.Please try again later";
            }
            return View(userViewModel);
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
    }
}