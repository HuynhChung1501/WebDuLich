using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebDuLich.App_Start;
using WebDuLich.Areas.Admin.Models;
using WebDuLich.DB;

namespace WebDuLich.Areas.Admin.Controllers
{
    public class LoginController : Controller
    {
        TestDataEntities2 db = new TestDataEntities2();
        // GET: Admin/Login

        public ActionResult DangNhap()
        {
            return View();
        }


        [HttpPost]
        public ActionResult DangNhap(string UserName, string Password)
        {


            ViewBag.thongbao = "";
            if (string.IsNullOrEmpty(UserName) && string.IsNullOrEmpty(Password))
            {
                ViewBag.thongbao = "Tài khoản, mật khẩu không được để trống!";
                return View();
            }

            User user = new MapTaiKhoan().Chitiet(UserName, Password);

            if (user == null) {
                ViewBag.thongbao = "Tài khoản, mật khẩu không chính xác!";
                return View();
            }
            Session["user"] = user;

            CookieHelper.Create("userName", user.UserName, DateTime.Now.AddDays(10));
            CookieHelper.Create("passWord", user.Password, DateTime.Now.AddDays(10));

            return Redirect("/Admin/Home");
        }
    }
}