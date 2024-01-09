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
    [kiemTraQuyen]
    public class KhachSanController : Controller
    {
        TestDataEntities db = new TestDataEntities();

        [kiemTraQuyen(ChucNang= "KS_XemDS")]
        // GET: Admin/NhaHang
        public ActionResult Index()
        {
            List<KhachSan> khachSans = db.KhachSans.ToList();
            return View(khachSans);
        }

        [kiemTraQuyen(ChucNang = "KS_ThemMoi")]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(KhachSan model)
        {
            new MapKhachSan().ThemMoi(model);
            ViewBag.thongbao = new MapKhachSan().thongbao;
            return Redirect("/Admin/KhachSan");

        }

        [kiemTraQuyen(ChucNang = "KS_ChinhSua")]
        public ActionResult Update(int id)
        {
            KhachSan khachSan = new MapKhachSan().ChiTiet(id);
            if (khachSan != null)
            {
                return View(khachSan);
            }
            return Redirect("/Admin/KhachSan");

        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Update(KhachSan model)
        {
            if (new MapKhachSan().CapNhatKhachSan(model))
            {
                return Redirect("/Admin/KhachSan");
            }
            return Redirect("/Admin/KhachSan");
            //return RedirectToAction("Update", "KhachSan", model);
        }

        [kiemTraQuyen(ChucNang = "KS_Xoa")]
        public ActionResult Delete(int id)
        {
            new MapKhachSan().Xoa(id);
            ViewBag.thongbao = new MapKhachSan().thongbao;
            return Redirect("/Admin/KhachSan");
        }
    }
}