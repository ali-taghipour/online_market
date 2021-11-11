using BLL;
using Entities;
using KargahProject.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TAD;
using TAD_Security;

namespace Shop
{
    public partial class Sign : System.Web.UI.Page
    {
        UserManager UserManager;
        public Sign()
        {
            UserManager = new UserManager();
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }



        /// <summary>
        /// لاگین
        /// </summary>
        protected void Login_ServerClick(object sender, EventArgs e)
        {
            LoginError.InnerHtml = "";
            LoginError.Attributes["class"] = "";
            
            
            //کپچا
            string[] amounts = Request.Form.GetValues("g-recaptcha-response");
            var CaptchaRes = CheckCaptcha(amounts[0]);
            if (!CaptchaRes.IsValid)
            {
                LoginError.InnerHtml = CaptchaRes.Errors;
                LoginError.Attributes["class"] = "show-error";
                return;
            }
            

            //لاگین
            string Username = LoginUsername.Value;
            string Password = LoginPassword.Value;


             ValidateResultViewModel res = UserManager.UserLogin(Username, Password);
            if (!res.IsValid)
            {
                LoginError.InnerHtml = res.Errors;
                LoginError.Attributes["class"] = "show-error";
                return;
            }
            else
                Response.Redirect("~/");

        }






        /// <summary>
        /// رجیستر
        /// </summary>
        protected void Register_ServerClick(object sender, EventArgs e)
        {
            LoginForm.Attributes["class"] = LoginForm.Attributes["class"].Replace("show", "");
            RegisterForm.Attributes["class"] = RegisterForm.Attributes["class"] + " show";

            RegisterError.InnerHtml = "";
            RegisterError.Attributes["class"] = "";

            //کپچا
            string[] amounts = Request.Form.GetValues("g-recaptcha-response");
            var CaptchaRes = CheckCaptcha(amounts[1]);
            if (!CaptchaRes.IsValid)
            {
                RegisterError.InnerHtml = CaptchaRes.Errors;
                RegisterError.Attributes["class"] = "show-error";
                return;
            }

            //ایجاد ابجکت کاربر و ولیدیشن
            string Username = RegisterUsername.Value;
            string Password = RegisterPassword.Value;
            string RePassword = RegisterRePassword.Value;

            User User = new User()
            {
                CreateDate = DateTime.Now,
                IsEnabled = true,
                IsMale = true,
                Password = Password.GetHash(),
                Type = Enums.UserType.Customer,
                Username = Username
            };

            ValidateResultViewModel Res = UserManager.Validate(User, null, Password, RePassword, false);
            if (!Res.IsValid)
            {
                RegisterError.InnerHtml = Res.Errors;
                RegisterError.Attributes["class"] = "show-error";
                return;
            }

            //ایجاد کاربر در دیتابیس
            bool IsSuccess = UserManager.Create(User);
            if (!IsSuccess)
            {
                RegisterError.InnerHtml = "خطا رخ داده است. مجددا اقدام کنید";
                RegisterError.Attributes["class"] = "show-error";
                return;
            }
            ValidateResultViewModel res = UserManager.UserLogin(Username, Password);
            if (!res.IsValid)
            {
                RegisterError.InnerHtml = res.Errors;
                RegisterError.Attributes["class"] = "show-error";
                return;
            }
            else
                Response.Redirect("~/EditProfile.aspx");
        }







        /// <summary>
        /// چک کردن کپچا
        /// </summary>
        /// <returns></returns>
        public ValidateResultViewModel CheckCaptcha(string CaptchaValue)
        {
            //تایید کلیک بر روی من ربات نیستم
            CaptchaManager CaptchaManager = new CaptchaManager();
            if (string.IsNullOrWhiteSpace(CaptchaValue))
                return new ValidateResultViewModel()
                {
                    IsValid = false,
                    Errors = "بر روی \"من ربات نیستم\" کلیک کنید."
                };

            ReCaptchaResponse reCaptchaResponse = CaptchaManager.VerifyCaptcha("6Lddd5MUAAAAABMcq_sYEmcf5ubOJJHf0rLEZO3F", CaptchaValue);
            if (reCaptchaResponse == null || !reCaptchaResponse.success)
                return new ValidateResultViewModel()
                {
                    IsValid = false,
                    Errors = "ربات نبودن شما تأیید نشد. مجدداً امتحان کنید."
                };

            return new ValidateResultViewModel()
            {
                IsValid = true
            };
        }









    }
}