using DAL;
using Entities;
using Enums;
using KargahProject.Models.BLL;
using KargahProject.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using TAD_ExtentionMethods;
using TAD_ImageResizer;
using TAD_Security;



namespace BLL
{
    /// <summary>
    /// Summary description for UserManager
    /// </summary>
    public class UserManager
    {
        private UserRepository Repo;
        public UserManager()
        {
            Repo = new UserRepository();
        }


        /// <summary>
        /// گرفتن کاربر با آیدی
        /// </summary>
        /// <param name="Id">آیدی کاربر</param>
        /// <returns></returns>
        public User GetById(int? Id)
        {
            if (Id == null)
                return null;
            DataRow DataRow = Repo.GetById((int)Id);
            return ToDataModel(DataRow);
        }





        /// <summary>
        /// گرفتن لیست همه کاربران
        /// </summary>
        /// <returns></returns>
        public List<User> GetAll()
        {
            DataTable DataTable = Repo.GetAll();
            return ToDataModel(DataTable);
        }




        /// <summary>
        /// گرفتن تعداد همه کاربران
        /// </summary>
        /// <returns></returns>
        public int? GetAllCount()
        {
            return Repo.GetAllCount();
        }




        /// <summary>
        /// ایجاد کاربر جدید
        /// </summary>
        /// <returns></returns>
        public bool Create(User User)
        {
            if (User.File != null && User.File.ContentLength > 0 && User.File.IsImage())
            {
                //string FileName = (customer.FirstName + "-" + customer.LastName + "-" + Guid.NewGuid().ToString()).GetImgUrlFriendly();
                string FileName = Guid.NewGuid().ToString().GetImgUrlFriendly() + User.File.GetExtention();
                string FilePath = "~/Uploads/Users/" + FileName;
                User.File.SaveAs(HttpContext.Current.Server.MapPath(FilePath));
                ImageResizer.ResizeImage(HttpContext.Current.Server.MapPath(FilePath), HttpContext.Current.Server.MapPath(FilePath), 200, 200, 90);
                User.Pic = FilePath;
            }
            return Repo.Create(User);
        }




        /// <summary>
        /// آپدیت کاربر
        /// </summary>
        /// <returns></returns>
        public bool Update(User User)
        {
            if (User.File != null && User.File.ContentLength > 0 && User.File.IsImage())
            {
                string FileName = Guid.NewGuid().ToString().GetImgUrlFriendly() + User.File.GetExtention();
                string FilePath = "~/Uploads/Users/" + FileName;
                User.File.SaveAs(HttpContext.Current.Server.MapPath(FilePath));
                ImageResizer.ResizeImage(HttpContext.Current.Server.MapPath(FilePath), HttpContext.Current.Server.MapPath(FilePath), 200, 200, 90);

                if(!string.IsNullOrEmpty(User.Pic) && File.Exists(HttpContext.Current.Server.MapPath(User.Pic)))
                {
                    File.Delete(HttpContext.Current.Server.MapPath(User.Pic));
                }

                User.Pic = FilePath;
            }
            return Repo.Update(User);
        }




        /// <summary>
        /// حذف کاربر
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public bool Delete(int Id)
        {
            DataRow DataRow = Repo.Delete(Id);
            var User = ToDataModel(DataRow);
            if (User == null)
                return false;
            if (!string.IsNullOrEmpty(User.Pic) && File.Exists(HttpContext.Current.Server.MapPath(User.Pic)))
                File.Delete(HttpContext.Current.Server.MapPath(User.Pic));
            return true;
        }



        /// <summary>
        /// حذف تصویر کاربر
        /// </summary>
        /// <param name="UserId">آیدی کاربر</param>
        /// <returns></returns>
        public bool DeleteUserPic(int UserId)
        {
            string Pic = Repo.DeleteUserPic(UserId);
            if (string.IsNullOrEmpty(Pic))
                return false;
            if (File.Exists(HttpContext.Current.Server.MapPath(Pic)))
                File.Delete(HttpContext.Current.Server.MapPath(Pic));
            return true;
        }





        /// <summary>
        /// آیا نام کاربری انتخاب شده برای کاربر معتبر است؟
        /// </summary>
        public bool UsernameIsValid(string Username, int? UserId)
        {
            if (UserId == null || UserId == 0)
                return Repo.UsernameIsValid(Username);
            else
                return Repo.UsernameIsValid(Username, (int)UserId);
        }





        /// <summary>
        /// گرفتن کاربر با یوزرنیم و پسورد
        /// </summary>
        /// <returns></returns>
        public User GetByUsernameAndPassword(string Username, string Password)
        {
            var DataRow = Repo.GetByUsernameAndPassword(Username, Password.GetHash());
            return ToDataModel(DataRow);
        }



        /// <summary>
        /// آیا کاربری که در کوکی است به پنل ادمین دسترسی دارد؟
        /// </summary>
        /// <returns></returns>
        public ValidateResultViewModel UserIsValidForAdminPanel()
        {
            if (HttpContext.Current.Request.Cookies["Admin"] == null)
            {
                HttpContext.Current.Response.Redirect("~/Admin/LogOut.aspx");
                return null;
            }

            int UserId;
            if (!int.TryParse(HttpContext.Current.Request.Cookies["Admin"]["Id"], out UserId))
            {
                HttpContext.Current.Response.Redirect("~/Admin/LogOut.aspx");
                return null;
            }
            
            var User = GetById(UserId);
            if (User == null)
            {
                HttpContext.Current.Response.Redirect("~/Admin/LogOut.aspx");
                return null;
            }

            if (User.Type != UserType.Admin)
                return new ValidateResultViewModel()
                {
                    IsValid = false,
                    Errors = "شما به این بخش دسترسی ندارید."
                };

            if (!User.IsEnabled)
                return new ValidateResultViewModel()
                {
                    IsValid = false,
                    Errors = "حساب کاربری شما مسدود میباشد!"
                };

            return new ValidateResultViewModel()
            {
                IsValid = true,
                Errors = ""
            };
        }





        /// <summary>
        /// لاگین کردن کاربر عادی
        /// </summary>
        /// <param name="Username"></param>
        /// <param name="Password"></param>
        /// <returns></returns>
        public ValidateResultViewModel UserLogin(string Username, string Password)
        {
            if (string.IsNullOrEmpty(Username))
                return new ValidateResultViewModel()
                {
                    IsValid = false,
                    Errors = "نام کاربری را وارد کنید."
                };

            if (string.IsNullOrEmpty(Password))
                return new ValidateResultViewModel()
                {
                    IsValid = false,
                    Errors = "کلمه عبور را وارد کنید."
                };


            var User = GetByUsernameAndPassword(Username, Password);
            if (User == null)
                return new ValidateResultViewModel()
                {
                    IsValid = false,
                    Errors = "نام کاربری یا کلمه عبور اشتباه است."
                };

            if (!User.IsEnabled)
                return new ValidateResultViewModel()
                {
                    IsValid = false,
                    Errors = "حساب کاربری شما مسدود میباشد!"
                };


            //اضافه کردن کاربر به کوکی
            if (HttpContext.Current.Request.Cookies["User"] != null)
                HttpContext.Current.Response.Cookies.Remove("User");

            HttpCookie Cookie = new HttpCookie("User");
            Cookie.Values["Id"] = User.Id.ToString();
            Cookie.Values["Username"] = User.Username;
            Cookie.Values["FullName"] = HttpContext.Current.Server.UrlEncode(User.FullName);
            Cookie.Values["Pic"] = User.Pic;
            Cookie.Values["Type"] = ((int)User.Type).ToString();
            Cookie.Expires = DateTime.Now.AddMonths(3);
            HttpContext.Current.Response.Cookies.Add(Cookie);


            //اضافه کردن سبد خرید باز کاربر به کوکی
            BasketManager BasketManager = new BasketManager();
            //اگر سبد بازی وجود داشت
            if (HttpContext.Current.Request.Cookies["BasketId"] != null)
            {
                int BasketId = int.Parse(HttpContext.Current.Request.Cookies["BasketId"].Value);
                var Basket = BasketManager.GetById(BasketId);
                // اگر سبد موجود متعلق به کاربر دیگری بود پاک کن
                if (Basket == null || Basket.UserId != null)
                    HttpContext.Current.Response.Cookies.Remove("BasketId");
                else // اگر سبد باز متعلق به کسی نبود، به کاربر لاگین کرده اضافه شود
                {
                    Basket.UserId = User.Id;
                    BasketManager.Update(Basket);
                    return new ValidateResultViewModel()
                    {
                        IsValid = true,
                        Errors = ""
                    };
                }
            }

            //اگر کاربر سبد باز دارد به  کوکی اضافه شود
            Basket _Basket = BasketManager.GetUserOpenBasket(User.Id).FirstOrDefault();
            if (_Basket != null)
            {
                HttpCookie BasketCookie = new HttpCookie("BasketId");
                BasketCookie.Value = _Basket.Id.ToString();
                BasketCookie.Expires = DateTime.Now.AddMonths(3);
                HttpContext.Current.Response.Cookies.Add(BasketCookie);
            }

            return new ValidateResultViewModel()
            {
                IsValid = true,
                Errors = ""
            };
        }









        /// <summary>
        /// لاگین در پنل ادمین
        /// </summary>
        /// <param name="Username"></param>
        /// <param name="Password"></param>
        /// <returns></returns>
        public ValidateResultViewModel AdminPanelLogin(string Username , string Password)
        {
            if (string.IsNullOrEmpty(Username))
                return new ValidateResultViewModel()
                {
                    IsValid = false,
                    Errors = "نام کاربری را وارد کنید."
                };

            if (string.IsNullOrEmpty(Password))
                return new ValidateResultViewModel()
                {
                    IsValid = false,
                    Errors = "کلمه عبور را وارد کنید."
                };
            

            var User = GetByUsernameAndPassword(Username, Password);
            if (User == null)
                return new ValidateResultViewModel()
                {
                    IsValid = false,
                    Errors = "نام کاربری یا کلمه عبور اشتباه است."
                };

            if (User.Type != UserType.Admin)
                return new ValidateResultViewModel()
                {
                    IsValid = false,
                    Errors = "شما به این بخش دسترسی ندارید."
                };

            if (!User.IsEnabled)
                return new ValidateResultViewModel()
                {
                    IsValid = false,
                    Errors = "حساب کاربری شما مسدود میباشد!"
                };


            //اضافه کردن کاربر به کوکی
            if (HttpContext.Current.Request.Cookies["Admin"] != null)
                HttpContext.Current.Response.Cookies.Remove("Admin");

            HttpCookie Cookie = new HttpCookie("Admin");
            Cookie.Values["Id"] = User.Id.ToString();
            Cookie.Values["Username"] = User.Username;
            Cookie.Values["FullName"] = HttpContext.Current.Server.UrlEncode(User.FullName);
            Cookie.Values["Pic"] = User.Pic;
            Cookie.Values["Type"] = ((int)User.Type).ToString();
            Cookie.Expires = DateTime.Now.AddMonths(3);
            HttpContext.Current.Response.Cookies.Add(Cookie);
                                    
            return new ValidateResultViewModel()
            {
                IsValid = true,
                Errors = ""
            };
        }








        /// <summary>
        /// ولیدیت کردن آبجکت اسلاید
        /// </summary>
        /// <param name="User">کاربر</param>
        /// <param name="RePassword">تکرار کلمه عبور</param>
        /// <param name="IsEdit">آیا ولیدیت برای ویرایش است؟ (یا ایجاد)</param>
        /// <returns></returns>
        public ValidateResultViewModel Validate(User User , string OldPassword, string NewPassword, string RePassword, bool IsEdit)
        {
            bool IsValid = true;
            string Errors = "";
            if (User.File != null && User.File.ContentLength > 0 && !User.File.IsImage())
            {
                IsValid = false;
                Errors += "- فرمت فایل انتخاب شده صحیح نمی باشد. <br />";
            }


            //نام کاربری
            if (string.IsNullOrEmpty(User.Username))
            {
                IsValid = false;
                Errors += "- نام کاربری الزامی است. <br />";
            }
            else if(User.Username.Length < 3)
            {
                IsValid = false;
                Errors += "- نام کاربری حداقل باید 3 کاراکتر باشد. <br />";
            }

            if(IsEdit)
            {
                if (!UsernameIsValid(User.Username, User.Id))
                {
                    IsValid = false;
                    Errors += "- نام کاربری تکراری است. <br />";
                }
            }
            else if (!UsernameIsValid(User.Username, null))
            {
                IsValid = false;
                Errors += "- نام کاربری تکراری است. <br />";
            }



            //کلمه عبور
            
            if (IsEdit)
            {
                if (!string.IsNullOrEmpty(NewPassword))
                {
                    if (string.IsNullOrEmpty(OldPassword))
                    {
                        IsValid = false;
                        Errors += "- کلمه عبور فعلی الزامی است. <br />";
                    }
                    if(OldPassword.GetHash() != User.Password)
                    {
                        IsValid = false;
                        Errors += "- کلمه عبور فعلی صحیح نمی باشد. <br />";
                    }

                    if (NewPassword != RePassword)
                    {
                        IsValid = false;
                        Errors += "- تکرار کلمه عبور صحیح نمی باشد. <br />";
                    }

                    if (NewPassword.Length < 4)
                    {
                        IsValid = false;
                        Errors += "- کلمه عبور حداقل باید 4 کاراکتر باشد. <br />";
                    }
                }
            }
            else
            {
                if (string.IsNullOrEmpty(NewPassword))
                {
                    IsValid = false;
                    Errors += "- کلمه عبور الزامی است. <br />";
                }
                if (string.IsNullOrEmpty(RePassword))
                {
                    IsValid = false;
                    Errors += "- تکرار کلمه عبور الزامی است. <br />";
                }

                if (NewPassword.Length < 4)
                {
                    IsValid = false;
                    Errors += "- کلمه عبور حداقل باید 4 کاراکتر باشد. <br />";
                }

                if (NewPassword != RePassword)
                {
                    IsValid = false;
                    Errors += "- تکرار کلمه عبور صحیح نمی باشد. <br />";
                }
            }

            return new ValidateResultViewModel()
            {
                IsValid = IsValid,
                Errors = Errors
            };
        }






        /// <summary>
        /// تبدیل یک سطر از جدول کاربران به یک آبجکت کاربر
        /// </summary>
        /// <param name="DataRow"></param>
        /// <returns></returns>
        public User ToDataModel(DataRow DataRow)
        {
            if (DataRow == null)
                return null;
            return new User()
            {
                Id = DataRow.Field<int>("Id"),
                FirstName = DataRow.Field<string>("FirstName"),
                LastName = DataRow.Field<string>("LastName"),
                Username = DataRow.Field<string>("Username"),
                Password = DataRow.Field<string>("Password"),
                Email = DataRow.Field<string>("Email"),
                Address = DataRow.Field<string>("Address"),
                PostalCode = DataRow.Field<int?>("PostalCode"),
                CreateDate = DataRow.Field<DateTime>("CreateDate"),
                IsEnabled = DataRow.Field<bool>("IsEnabled"),
                IsMale = DataRow.Field<bool>("IsMale"),
                Type = DataRow.Field<UserType>("Type"),
                Pic = DataRow.Field<string>("Pic"),
            };
        }





        /// <summary>
        /// تبدیل چند سطر از جدول کاربران به یک لیست از آبجکت کاربر
        /// </summary>
        /// <param name="DataRow"></param>
        /// <returns></returns>
        public List<User> ToDataModel(DataTable DataTable)
        {
            if (DataTable == null)
                return null;
            return DataTable.Select().Select(dr => new User
            {
                Id = dr.Field<int>("Id"),
                FirstName = dr.Field<string>("FirstName"),
                LastName = dr.Field<string>("LastName"),
                Username = dr.Field<string>("Username"),
                Password = dr.Field<string>("Password"),
                Email = dr.Field<string>("Email"),
                Address = dr.Field<string>("Address"),
                PostalCode = dr.Field<int?>("PostalCode"),
                CreateDate = dr.Field<DateTime>("CreateDate"),
                IsEnabled = dr.Field<bool>("IsEnabled"),
                IsMale = dr.Field<bool>("IsMale"),
                Type = dr.Field<UserType>("Type"),
                Pic = dr.Field<string>("Pic"),
            }).ToList();
        }


    }
}
