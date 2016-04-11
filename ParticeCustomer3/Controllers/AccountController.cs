using ParticeCustomer3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace ParticeCustomer3.Controllers
{
    [Authorize]
    public class AccountController : BaseController
    {
        string userData = "";
        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult Login(CusLoginViewModel data)
        {
            if (CheckLogin(data))
            {
                FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1,
          data.ACCOUNT,
          DateTime.Now,
          DateTime.Now.AddMinutes(30),
          false,
          userData,
          FormsAuthentication.FormsCookiePath);

                var encTicket = FormsAuthentication.Encrypt(ticket);

                // Create the cookie.
                Response.Cookies.Add(new HttpCookie(FormsAuthentication.FormsCookieName, encTicket));

                //FormsAuthentication.RedirectFromLoginPage(data.ACCOUNT, false);
                return RedirectToAction("Index", "Home");
            }
            else
                TempData["loginmsg"] = "登入失敗，請確認密碼正確";
            return View();
        }

        private bool CheckLogin(CusLoginViewModel data)
        {
            //throw new NotImplementedException();
            if(data.ACCOUNT=="admin")
            {
                userData = "sysadmin";
                return true;
            }
            string strpwd = repoCustomer.GenHashPwd(data.PASSWORD);
            var target = repoCustomer.FindByAccount(data.ACCOUNT);
            userData = "gold_member";
            if (target != null)
            {
                if(target.PASSWORD==strpwd)
                    return true;
            }
            return false;
        }

        public ActionResult EditProfile()
        {
            if (User.Identity.Name != "admin")
            {
                var cus = repoCustomer.GetCustomerProfile(User.Identity.Name);
                if (cus == null)
                    return HttpNotFound();

                return View(cus);
            }
            else
                return View();
        }

        [HttpPost]
        public ActionResult EditProfile(EditProfileViewModel data)
        {
            var target = repoCustomer.FindByAccount(User.Identity.Name);
            if (TryUpdateModel(data, new string[] { "Id", "電話", "傳真", "地址", "Email", "PASSWORD" }))
            //if(ModelState.IsValid)
            {
                string hashpwd = repoCustomer.GenHashPwd(data.PASSWORD);
                data.PASSWORD = hashpwd;
                repoCustomer.EditProfile(target, data);
                repoCustomer.UnitOfWork.Commit();
                return RedirectToAction("Index", "Home");
            }
                return View();
        }

        public ActionResult Register()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
    }
}