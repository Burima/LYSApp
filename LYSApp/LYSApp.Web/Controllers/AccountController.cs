using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Owin;
using LYSApp.Model;
using LYSApp.Domain.NotificationManagement;
using LYSApp.Web.Services.Security;
using System.Configuration;
using LYSApp.Web.Services.Common;
using LYSApp.Web.Services;

namespace LYSApp.Web.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private UserManager _userManager;
        AccountViewModel accountViewModel = new AccountViewModel();
        MandrillMailer mandrillMailer = new MandrillMailer();
        TripleDES tripleDES = new TripleDES();

       
        public AccountController()
        {
        }

        public AccountController(UserManager userManager)
        {
            UserManager = userManager;
        }

        public UserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<UserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        [AllowAnonymous]
        public ActionResult Index()
        {
            return View(accountViewModel);
        }

        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
         
            ViewBag.ReturnUrl = returnUrl;
            return View("Index");
        }

        //
        // POST: /Account/Login
        //[HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(AccountViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindAsync(model.LoginViewModel.Email, model.LoginViewModel.Password);
                if (user != null)
                {
                    await SignInAsync(user, model.LoginViewModel.RememberMe);
                    //sessionize user
                    SessionManager.SessionizeUser(user);
                                     
                    return RedirectToLocal(returnUrl);
                    
                    
                }
                else
                {
                    ModelState.AddModelError("", "Invalid username or password.");
                    model.LoginViewModel.LoginError = "Invalid username or password.";
                }
            }

            // If we got this far, something failed, redisplay form
            return View("Index",model);
        }

        //
        //[HttpGet]
        //[AllowAnonymous]
        //public ActionResult Register()
        //{
        //    return View("Index");
        //}

        //
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(AccountViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new User()
                {
                    FirstName = model.RegisterViewModel.FirstName,
                    LastName = model.RegisterViewModel.LastName,
                    UserName = model.RegisterViewModel.Email.ToUpper(),
                    Email = model.RegisterViewModel.Email.ToUpper(),
                    CreatedOn=DateTime.Now,
                    LastUpdatedOn=DateTime.Now,
                    Status=1,
                    LockoutEndDateUtc=DateTime.Now.AddDays(60),
                    LockoutEnabled=true
                };
                IdentityResult result = await UserManager.CreateAsync(user, model.RegisterViewModel.Password);
                
                if (result.Succeeded)
                {
                    await SignInAsync(user, isPersistent: false);
                    //sessionize user
                    SessionManager.SessionizeUser(user);
                    //save to mailchimp subscription list
                    //enables for production only
                    if (LYSConfig.EnvironmentName == "Production")
                    {
                        mandrillMailer.SaveToMailChimpList(user.Email, user.FirstName, user.LastName);
                    }

                    //Send Activation emai
                    await SendAccountActivationMail(user);

                    //return RedirectToAction("Index", "Home");
                }
                else
                {
                    model.RegisterViewModel.RegisterError = result.Errors.FirstOrDefault();
                    AddErrors(result);
                }
            }

            // If we got this far, something failed, redisplay form
            return View("Index",model);
        }

        //
        // GET: /Account/ConfirmEmail
        [AllowAnonymous]
        public async Task<ActionResult> ConfirmEmail(string userId, string code)
        {
            if (userId == null || userId==String.Empty || code == null ||  code==String.Empty)
            {
                return View("Error");
            }
            try
            {
                IdentityResult result = await UserManager.ConfirmEmailAsync(Convert.ToInt64(tripleDES.Decrypt(userId).Trim()), tripleDES.Decrypt(code));
                if (result.Succeeded)
                {
                    //once UserManagement is ready we need to upate User Staus here
                    //return View("ConfirmEmail");
                }
                else
                {
                    AddErrors(result);
                    ViewBag.Error = "Invalid Token! ";
                   // return View();
                }
            }
            catch(Exception ex)
            {
                //Incorrect Token
                ViewBag.Error = "Invalid Token! ";
                //return View();
            }
            return View(); 
        }

        //
        // GET: /Account/ForgotPassword
        [AllowAnonymous]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        //
        // POST: /Account/ForgotPassword
        [HttpPost]
        [AllowAnonymous]
        //[ValidateAntiForgeryToken]
        public async Task<ActionResult> ForgotPassword(string email)
        {
            if (ModelState.IsValid || email!=String.Empty)
            {
                var user = await UserManager.FindByNameAsync(email);
                if (user == null)
                {
                    return Content("The email id "+email+" does not exist");
                }
                else if (!(await UserManager.IsEmailConfirmedAsync(user.Id)))
                {
                    return Content("The email id " + email + " is not confirmed.");
                }

                // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                // Send an email with this link
                string code = await UserManager.GeneratePasswordResetTokenAsync(user.Id);
                var callbackUrl = Url.Action("ResetPassword", "Account", new { userId =tripleDES.Encrypt((user.Id).ToString()), code =tripleDES.Encrypt(code) }, protocol: Request.Url.Scheme);
                //send Reset Password link
                mandrillMailer.SendEmailForUser(user.Email, callbackUrl, "Activate Your Account", "Reset Your Password");
                // await UserManager.SendEmailAsync(user.Id, "Reset Password", "Please reset your password by clicking <a href=\"" + callbackUrl + "\">here</a>");
                // return RedirectToAction("ForgotPasswordConfirmation", "Account");                
                return Content("A link to reset your password has been sent to " + email);
            }

            // If we got this far, something failed, redisplay form
            return Content("Something went wrong! Please contact support@lockyourstay.com.");
        }

        //
        // GET: /Account/ForgotPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ForgotPasswordConfirmation()
        {
            return View();
        }

        //
        // GET: /Account/ResetPassword
        [AllowAnonymous]
        public ActionResult ResetPassword(string userId, string code)
        {
            if (code == null || code==String.Empty || userId==null || userId==String.Empty)
            {
                return View("Error");
            }
            AccountViewModel accountViewModel = new AccountViewModel();
            accountViewModel.ResetPasswordViewModel = new ResetPasswordViewModel()
            {
                Code = code,
                UserID = userId
            };    
            //TempData.Keep("Message");
            return View(accountViewModel);
        }

        //
        // POST: /Account/ResetPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ResetPassword(AccountViewModel model)
        {
            if (ModelState.IsValid)
            {                
                var user = await UserManager.FindByIdAsync(Convert.ToInt64(tripleDES.Decrypt(model.ResetPasswordViewModel.UserID).Trim()));
                if (user == null)
                {
                    TempData["Message"] = "No user found! ";
                    return RedirectToAction("ResetPassword", "Account", new { userId = model.ResetPasswordViewModel.UserID, code = model.ResetPasswordViewModel.Code });
                }
                IdentityResult result = await UserManager.ResetPasswordAsync(user.Id, tripleDES.Decrypt(model.ResetPasswordViewModel.Code.Trim()), model.ResetPasswordViewModel.Password);
                if (result.Succeeded)
                {
                    return RedirectToAction("ResetPasswordConfirmation", "Account");
                }
                else
                {
                    TempData["Message"] =  result.Errors.FirstOrDefault();
                    return RedirectToAction("ResetPassword", "Account", new { userId=model.ResetPasswordViewModel.UserID,code=model.ResetPasswordViewModel.Code });
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/ResetPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ResetPasswordConfirmation()
        {
            return View();
        }

        //
        // POST: /Account/Disassociate
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Disassociate(string loginProvider, string providerKey)
        {
          
            ManageMessageId? message = null;
            var result = await UserManager.RemoveLoginAsync(long.Parse(User.Identity.GetUserId()), new UserLoginInfo(loginProvider, providerKey));
            if (result.Succeeded)
            {
                
                var user = await UserManager.FindByIdAsync(long.Parse(User.Identity.GetUserId()));
                await SignInAsync(user, isPersistent: false);
                message = ManageMessageId.RemoveLoginSuccess;
            }
            else
            {
                message = ManageMessageId.Error;
            }
            return RedirectToAction("Manage", new { Message = message });
        }

        //
        // GET: /Account/Manage
        public ActionResult Manage(ManageMessageId? message)
        {
            ViewBag.StatusMessage =
                message == ManageMessageId.ChangePasswordSuccess ? "Your password has been changed."
                : message == ManageMessageId.SetPasswordSuccess ? "Your password has been set."
                : message == ManageMessageId.RemoveLoginSuccess ? "The external login was removed."
                : message == ManageMessageId.Error ? "An error has occurred."
                : "";
            ViewBag.HasLocalPassword = HasPassword();
            ViewBag.ReturnUrl = Url.Action("Manage");
            return View();
        }

        //
        // POST: /Account/Manage
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Manage(ManageUserViewModel model)
        {
            bool hasPassword = HasPassword();
            ViewBag.HasLocalPassword = hasPassword;
            ViewBag.ReturnUrl = Url.Action("Manage");
            if (hasPassword)
            {
                if (ModelState.IsValid)
                {
                    var result = await UserManager.ChangePasswordAsync(long.Parse(User.Identity.GetUserId()), model.OldPassword, model.NewPassword);
                    if (result.Succeeded)
                    {
                        var user = await UserManager.FindByIdAsync(long.Parse(User.Identity.GetUserId()));
                        await SignInAsync(user, isPersistent: false);
                        return RedirectToAction("Manage", new { Message = ManageMessageId.ChangePasswordSuccess });
                    }
                    else
                    {
                        AddErrors(result);
                    }
                }
            }
            else
            {
                // User does not have a password so remove any validation errors caused by a missing OldPassword field
                ModelState state = ModelState["OldPassword"];
                if (state != null)
                {
                    state.Errors.Clear();
                }

                if (ModelState.IsValid)
                {
                    var result = await UserManager.AddPasswordAsync(long.Parse(User.Identity.GetUserId()), model.NewPassword);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Manage", new { Message = ManageMessageId.SetPasswordSuccess });
                    }
                    else
                    {
                        AddErrors(result);
                    }
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ChangePassword(UserViewModel userViewModel)
        {
            bool hasPassword = HasPassword();
            ViewBag.HasLocalPassword = hasPassword;
            ViewBag.StatusMessage = "An error has occurred.";
            if (hasPassword)
            {
                if (ModelState.IsValid)
                {
                    var result = await UserManager.ChangePasswordAsync(long.Parse(User.Identity.GetUserId()),
                        userViewModel.ManageUserViewModel.OldPassword, userViewModel.ManageUserViewModel.NewPassword);
                    if (result.Succeeded)
                    {
                        var user = await UserManager.FindByIdAsync(long.Parse(User.Identity.GetUserId()));
                        await SignInAsync(user, isPersistent: false);
                        ViewBag.StatusMessage = "Your password has been changed.";
                           
                    }
                   
                }
            }

            return PartialView("_Verification", userViewModel);
        }

        //
        // POST: /Account/ExternalLogin
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ExternalLogin(string provider, string returnUrl)
        {
            // Request a redirect to the external login provider
            return new ChallengeResult(provider, Url.Action("ExternalLoginCallback", "Account", new { ReturnUrl = returnUrl }));
        }

        //
        // GET: /Account/ExternalLoginCallback
        [AllowAnonymous]
        public async Task<ActionResult> ExternalLoginCallback(string returnUrl)
        {
            var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync();
            if (loginInfo == null)
            {
                return RedirectToAction("Login");
            }

            // Sign in the user with this external login provider if the user already has a login
            var user = await UserManager.FindAsync(loginInfo.Login);
            if (user != null)
            {
                await SignInAsync(user, isPersistent: false);
                //sessionize user
                SessionManager.SessionizeUser(user);
                return RedirectToLocal(returnUrl);
            }
            else
            {
                // If the user does not have an account, then prompt the user to create an account
                ViewBag.ReturnUrl = returnUrl;
                ViewBag.LoginProvider = loginInfo.Login.LoginProvider;
                
                return View("ExternalLoginConfirmation", new AccountViewModel { ExternalLoginConfirmationViewModel = new ExternalLoginConfirmationViewModel() { Email = loginInfo.Email } });
            }
        }

        //
        // POST: /Account/LinkLogin
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LinkLogin(string provider)
        {
            // Request a redirect to the external login provider to link a login for the current user
            return new ChallengeResult(provider, Url.Action("LinkLoginCallback", "Account"), User.Identity.GetUserId());
        }

        //
        // GET: /Account/LinkLoginCallback
        public async Task<ActionResult> LinkLoginCallback()
        {
            var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync(XsrfKey, User.Identity.GetUserId());
            if (loginInfo == null)
            {
                return RedirectToAction("Manage", new { Message = ManageMessageId.Error });
            }
            IdentityResult result = await UserManager.AddLoginAsync(long.Parse(User.Identity.GetUserId()), loginInfo.Login);
            if (result.Succeeded)
            {
                return RedirectToAction("Manage");
            }
            return RedirectToAction("Manage", new { Message = ManageMessageId.Error });
        }

        //
        // POST: /Account/ExternalLoginConfirmation
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ExternalLoginConfirmation(AccountViewModel model, string returnUrl)
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Manage");
            }

            if (ModelState.IsValid)
            {
                // Get the information about the user from the external login provider
                var info = await AuthenticationManager.GetExternalLoginInfoAsync();
                if (info == null)
                {
                    return View("ExternalLoginFailure");
                }
                
                var user = new User()
                {
                    FirstName = String.Empty,
                    LastName = String.Empty,
                    UserName = model.ExternalLoginConfirmationViewModel.Email.ToUpper(),
                    Email = model.ExternalLoginConfirmationViewModel.Email.ToUpper(),
                    CreatedOn=DateTime.Now,
                    LastUpdatedOn=DateTime.Now,
                    Status=1,
                    LockoutEndDateUtc=DateTime.Now.AddDays(60),
                    LockoutEnabled=true
                };
                
                IdentityResult result = await UserManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    result = await UserManager.AddLoginAsync(user.Id, info.Login);
                    if (result.Succeeded)
                    {
                        await SignInAsync(user, isPersistent: false);

                        //sessionize user
                        SessionManager.SessionizeUser(user);
                        //save to mailchimp subscription list
                        //enables for production only
                        if (LYSConfig.EnvironmentName == "Production")
                        {
                            mandrillMailer.SaveToMailChimpList(user.Email, user.FirstName, user.LastName);
                        }

                        //send Email Action emai
                        await SendAccountActivationMail(user);

                        return RedirectToLocal(returnUrl);
                    }
                }
                AddErrors(result);
            }

            ViewBag.ReturnUrl = returnUrl;
            return View(model);
        }

        //
        // POST: /Account/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut();
            SessionManager.DeSessionizeUser();            
            return RedirectToAction("Index", "Account");
        }

        //
        // GET: /Account/ExternalLoginFailure
        [AllowAnonymous]
        public ActionResult ExternalLoginFailure()
        {
            return View();
        }

        [ChildActionOnly]
        public ActionResult RemoveAccountList()
        {
            var linkedAccounts = UserManager.GetLogins(long.Parse(User.Identity.GetUserId()));
            ViewBag.ShowRemoveButton = HasPassword() || linkedAccounts.Count > 1;
            return (ActionResult)PartialView("_RemoveAccountPartial", linkedAccounts);
        }          

        //Email Subscribe
        [AllowAnonymous]
        [HttpPost]        
        public async Task<ActionResult> EmailSubscribe(string email)
        {
            if (email != null && email.ToString().Trim() != String.Empty)
            {
                if (mandrillMailer.SaveToMailChimpList(email, "", ""))
                {
                    return Content("Success");
                }
            }
            //return Content("Whoops! That didn't go so well did it?");
            return Content("Error");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && UserManager != null)
            {
                UserManager.Dispose();
                UserManager = null;
            }
            base.Dispose(disposing);
        }

        /// <summary>
        /// if Coplete Task including save to mailchimp and sending mails.
        /// </summary>
        /// <param name="user">currently registed user</param>
        /// <returns></returns>

        private async Task<bool> SendAccountActivationMail(User user)
        {
            try
            {
                // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                // Send an email with this link
                string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = tripleDES.Encrypt((user.Id).ToString()), code = tripleDES.Encrypt(code) }, protocol: Request.Url.Scheme);

                //send email activation link
                mandrillMailer.SendEmailForUser(user.Email, callbackUrl, "Activate Your Account", "Activate Your Account | Lockyourstay");

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }            
        }

        /// <summary>
        /// Will send verification mai for existing user
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> SendAccountActivationMailForExistingUser()
        {
            bool result = await SendAccountActivationMail(SessionManager.GetSessionUser());
            if (result)
            {
                return Content("Success");
            }
            else
            {
                return Content("Failed");
            }
        }

        #region Helpers
        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private async Task SignInAsync(User user, bool isPersistent)
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie);
            AuthenticationManager.SignIn(new AuthenticationProperties() { IsPersistent = isPersistent }, await user.GenerateUserIdentityAsync(UserManager));
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private bool HasPassword()
        {
            var user = UserManager.FindById(long.Parse(User.Identity.GetUserId()));
            if (user != null)
            {
                return user.PasswordHash != null;
            }
            return false;
        }

        private void SendEmail(string email, string callbackUrl, string subject, string message)
        {
            // For information on sending mail, please visit http://go.microsoft.com/fwlink/?LinkID=320771
        }

        public enum ManageMessageId
        {
            ChangePasswordSuccess,
            SetPasswordSuccess,
            RemoveLoginSuccess,
            Error
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);                
            }
            else
            {
                return View("Index");
            }
        }

        private class ChallengeResult : HttpUnauthorizedResult
        {
            public ChallengeResult(string provider, string redirectUri)
                : this(provider, redirectUri, null)
            {
            }

            public ChallengeResult(string provider, string redirectUri, string userId)
            {
                LoginProvider = provider;
                RedirectUri = redirectUri;
                UserId = userId;
            }

            public string LoginProvider { get; set; }
            public string RedirectUri { get; set; }
            public string UserId { get; set; }

            public override void ExecuteResult(ControllerContext context)
            {
                var properties = new AuthenticationProperties() { RedirectUri = RedirectUri };
                if (UserId != null)
                {
                    properties.Dictionary[XsrfKey] = UserId;
                }
                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
            }
        }
        #endregion

     }
}