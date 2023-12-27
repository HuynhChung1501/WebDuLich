using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebDuLich.Areas.Admin.Models;
using WebDuLich.DB;
using WebDuLich.Models.ModelView.TaiKhoan;

namespace WebDuLich.Areas.Admin.Controllers
{
    public class TaiKhoanController : Controller
    {
        TestDataEntities2 db = new TestDataEntities2();

        // GET: Admin/taiKhoan
        public ActionResult Index()
        {
            List<User> users = db.Users.ToList();
            return View(new MapTaiKhoanView()
            {
                Users = users
            });
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(MapTaiKhoanView model)
        {
            var us = new User
            {
                UserName = model.UserName,
                FullName = model.FullName,
                IsActive = model.IsActive == 1 ? true : false,
                SoDienThoai = model.SoDienThoai,
                Avatar = model.Avatar,
                Password = model.Password,
            };
            new MapTaiKhoan().ThemMoi(us);
            ViewBag.thongbao = new MapTaiKhoan().thongbao;
            return Redirect("/Admin/TaiKhoan");

        }

        public ActionResult Update(string userName)
        {
            User user = new MapTaiKhoan().Chitiet(userName);
            if (user != null)
            {
                return View(new MapTaiKhoanView()
                {
                    User = user
                });
            }
            return Redirect("/Admin/TaiKhoan");

        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Update(User model)
        {
            if (new MapTaiKhoan().CapNhat(model))
            {
                return Redirect("/Admin/TaiKhoan");
            }
            return View(new MapTaiKhoanView()
            {
                User = model
            });
            //return RedirectToAction("Update", "NhaHang", model);
        }

        public ActionResult Detail(string userName)
        {
            User user = new MapTaiKhoan().Chitiet(userName);
            List<ChucNang> chucNang = new MapChucNang().DanhSach();
            List<PhanQuyen> phanQuyen = new MapPhanQuyen().DanhSach();

            if (user != null)
            {
                return View(new MapTaiKhoanView()
                {
                    User = user,
                    DanhSachPhanQuyen = phanQuyen,
                    DanhSachChucNang = chucNang,
                });
            }
            return Redirect("/Admin/TaiKhoan");
        }

        public ActionResult Delete(string userName)
        {
            new MapTaiKhoan().Xoa(userName);
            ViewBag.thongbao = new MapTaiKhoan().thongbao;
            return Redirect("/Admin/TaiKhoan");
        }

        public JsonResult PhanQuyenTaiKhoan (string userName, string maChucNang)
        {
            PhanQuyen phanQuyen = db.PhanQuyens.FirstOrDefault(n => n.UserName == userName && n.MaChucNang == maChucNang);
            if (phanQuyen != null)
            {
                db.PhanQuyens.Remove(phanQuyen);
                db.SaveChanges();
            }
            else
            {
                phanQuyen = new PhanQuyen();
                phanQuyen.UserName = userName;
                phanQuyen.MaChucNang= maChucNang;
                db.PhanQuyens.Add(phanQuyen);
                db.SaveChanges();
            }
            return Json(new
            {
                status = "ok"
            });
        }
    }
}