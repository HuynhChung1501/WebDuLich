using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using WebDuLich.Areas.Admin.Models;
using WebDuLich.DB;
using WebDuLich.Models.ModelView.TaiKhoan;

namespace WebDuLich.App_Start
{
    public class kiemTraQuyen : System.Web.Mvc.AuthorizeAttribute
    {
        TestDataEntities db = new TestDataEntities();
        public string ChucNang { get; set; }
        public override void OnAuthorization(AuthorizationContext fillterContext)
        {
            var User = (User)HttpContext.Current.Session["user"];
            //mếu user chưa có trong sesstion, thì kiếm tra trong cookie
            string userName = CookieHelper.Get("userName");
            string passWord = CookieHelper.Get("passWord");

            User taiKhoan = new MapTaiKhoan().Chitiet(userName);
            if (taiKhoan != null)
            {
                User = taiKhoan;
                HttpContext.Current.Session["user"] = User;
            }


            //Nếu chưa có secction thì chuyển về trang đăng nhập
            if (User == null)
            {
                fillterContext.Result = new RedirectToRouteResult(new
                    RouteValueDictionary(new
                    {
                        Controller = "Login",
                        action = "DangNhap",
                        Areas = "Admin"
                    }));
                return;
            }
            if (string.IsNullOrEmpty(ChucNang))
            {
                return;
            }

            //kiểm tra quyền

            var phanQuyen = db.PhanQuyens.Count(n => n.UserName == User.UserName && n.MaChucNang == ChucNang);

            if (phanQuyen <= 0)
            {
                fillterContext.Result = new RedirectToRouteResult(new
                    RouteValueDictionary(new
                    {
                        Controller = "Erorr",
                        action = "PhanQuyen",
                        Areas = "Admin"
                    }));
                return;
            }
        }
    }
}