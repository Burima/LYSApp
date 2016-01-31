using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace LYSApp.Model
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class ExternalLoginListViewModel
    {
        public string Action { get; set; }
        public string ReturnUrl { get; set; }
    }

    public class ManageUserViewModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Current password")]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }

    public class LoginViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }

    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [Required]
        public string FirstName { get; set; }

        public string LastName { get; set; }       
    }

    public class ResetPasswordViewModel
    {

        public string UserID { get; set; }
        public string Code { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }


    }

    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class AccountViewModel
    {
        public RegisterViewModel RegisterViewModel { get; set; }
        public LoginViewModel LoginViewModel { get; set; }
        public ResetPasswordViewModel ResetPasswordViewModel { get; set; }
        public ExternalLoginConfirmationViewModel ExternalLoginConfirmationViewModel { get; set; }

    }

    [JsonObject(MemberSerialization.OptIn)]
    public class UserViewModel
    {
        public long UserID { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsBackGroundVerified { get; set; }
        public string ProfilePicture { get; set; }
        public Nullable<int> Gender { get; set; }
        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }
        public string PhoneNumber { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        public string PresentAddress { get; set; }
        public string PermanentAddress { get; set; }
        public int GovtIDType { get; set; }
        public string GovtID { get; set; }
        public int UserProfession { get; set; }
        public string CurrentEmployer { get; set; }
        public string OfficeLocation { get; set; }
        public string EmployeeID { get; set; }
        public string HighestEducation { get; set; }
        public string InstitutionName { get; set; }
        public int Status { get; set; }
        public Nullable<System.DateTime> LastUpdatedOn { get; set; }
        public IList<PGReview> pgReviews { get; set; }
        public HouseReviewModel HouseReviewModel { get; set; }
        public ManageUserViewModel ManageUserViewModel { get; set; }
    }
    public class EmailSubscribeViewModel
    {
        public string Email { get; set; }
    }

    public class EmailVerificationViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

    }
}
