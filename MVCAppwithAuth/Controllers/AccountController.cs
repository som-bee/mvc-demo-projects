using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using MVCAppwithAuth.Models;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using System.Security.Cryptography;
using System.Threading.Tasks;
using MVCAppwithAuth.EmailServices;
using static Org.BouncyCastle.Crypto.Engines.SM2Engine;
using System.Reflection;
using System.Data.Entity.Infrastructure;
using System.Web.Helpers;

namespace MVCAppwithAuth.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Login()
        {
            ViewBag.Message = "No Message";
            if (TempData.ContainsKey("Message"))
            {
                ViewBag.Message = TempData["Message"].ToString();
                TempData.Remove("Message");
            }
           
            return View();
        }

        [HttpPost]

        public ActionResult Login(Models.Membership model) { 

            //authenticating user
            using (var context = new officeEntities())
            {
                //matching user name and pass
                bool isValid = context.User.Any(x=> x.UserName == model.UserName && x.Password==model.Password);
                //if vald user then adding user name to cookie and redirecting to index page
                if (isValid)
                {
                    FormsAuthentication.SetAuthCookie(model.UserName, false);
                    return RedirectToAction("Index","Employees");
                }
                // if not a valid user returning the login view
                ModelState.AddModelError("", "Invide User Name / Password");
                return View();
            }
        
        }

        public ActionResult Signup()
        {
            return View();
        }

        [HttpPost]

        public ActionResult Signup(User model)
        {
            if (ModelState.IsValid)
            {
                //creating user
                using (var context = new officeEntities())
                {
                    var alreadyExists = context.User.Any(x => x.UserName == model.UserName);
                    if (alreadyExists)
                    {
                        TempData["Message"] = "User with the same email already exists..";
                        return RedirectToAction("login");
                    }
                    context.User.Add(model);
                    context.SaveChanges();
                    return RedirectToAction("login");
                }
            }
           
            return RedirectToAction("signup");
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }




        // GET: Account/ForgotPassword
        public ActionResult ForgotPassword()
        {
            return View();
        }

        // POST: Account/ForgotPassword
        [HttpPost]
        public ActionResult ForgotPassword(string email)
        {
            using (var context = new officeEntities())
            {
                //matching user name and pass
                bool isValid = context.User.Any(x => x.UserName == email);
                //if vald user then adding user name to cookie and redirecting to index page
                if (isValid)
                {
                    EmailService emailService = new EmailService();
                    // Validate the email address and generate the password reset link
                    string resetPasswordLink = GenerateResetPasswordLink(email);
                    //var resetPasswordToken = GenerateForgotPasswordTokenAsync();
                    // Send the forgot password email
                    emailService.SendForgotPasswordEmail(email, resetPasswordLink);
                }
            }

            // Redirect to a confirmation page
            return RedirectToAction("ForgotPasswordConfirmation");
        }

        
        // GET: Account/ForgotPasswordConfirmation
        public ActionResult ForgotPasswordConfirmation()
        {
            return View();
        }



        [HttpGet]
        public ActionResult ResetPassword(string UserId, string Token)
        {

            using(var context = new officeEntities())
            {
                var isValid = context.ForgotPass.Any(x => x.UserName == UserId && x.ForgotPassToken == Token);
                if (isValid)
                {
                    ViewBag.UserId = UserId;
                    ViewBag.Token = Token;
                    return View(new User()
                    {
                        UserName = UserId
                    });
                }
            }
            TempData["Message"] = "Link Expired.. Login using your credentials...";
            return RedirectToAction("Login");

        }


        [HttpPost]
        public ActionResult ResetPassword(User user, string token)
        {
            if (ModelState.IsValid)
            {
                using (var context = new officeEntities())
                {
                    var forgotPass = context.ForgotPass.FirstOrDefault(x => x.UserName == user.UserName && x.ForgotPassToken == token);
                    if (forgotPass != null)
                    {
                        var entity = context.User.FirstOrDefault(x => x.UserName == user.UserName);
                        entity.Password = user.Password;
                        forgotPass.ForgotPassToken = null;
                        context.SaveChanges();
                        TempData["Message"] = "Password Udated Successfully..";
                        return RedirectToAction("Login");
                    }
                }

                TempData["Message"] = "Can not update password.. something gone wrong...";
                return RedirectToAction("Login");
            }

            TempData["Message"] = "Can not update password.. something gone wrong...";
            return RedirectToAction("Login");


        }

        // Generate the password reset link (dummy implementation)
        private string GenerateResetPasswordLink(string email)
        {
            // Logic to generate the reset password link

            // Generate a random token using a cryptographically strong random number generator
            byte[] randomBytes = new byte[32];
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(randomBytes);
            }


            // Convert the random bytes to a string representation (e.g., hexadecimal or Base64)
            string token = Convert.ToBase64String(randomBytes);
           // storing token in database
            using (var context = new officeEntities())
            {
                    var entity = context.ForgotPass.Find(email);
                    if(entity != null)
                    {
                        entity.ForgotPassToken = token;
                        context.SaveChanges();
                    }
                    else
                    {
                        context.ForgotPass.Add(new ForgotPass()
                        {
                            UserName = email,
                            ForgotPassToken = token
                        });
                        context.SaveChanges();
                    }
            }


            var callbackUrl = Url.Action("ResetPassword", "Account",
    new { UserId = email, Token = token }, protocol: Request.Url.Scheme);


            return callbackUrl;
        }
    }

}