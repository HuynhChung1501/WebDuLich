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
    public class NhaHangController : Controller
    {
        TestDataEntities db = new TestDataEntities();

        [kiemTraQuyen(ChucNang= "NH_XemDS")]
        // GET: Admin/NhaHang
        public ActionResult Index()
        {
            List<NhaHang> nhaHangs = db.NhaHangs.ToList();
            return View(nhaHangs);
        }

        [kiemTraQuyen(ChucNang = "NH_ThemMoi")]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(NhaHang model)
        {
            new MapNhaHang().ThemMoi(model);
            ViewBag.thongbao = new MapNhaHang().thongbao;
            return Redirect("/Admin/NhaHang");

        }

        [kiemTraQuyen(ChucNang = "NH_ChinhSua")]
        public ActionResult Update(int id)
        {
            NhaHang nhaHang = new MapNhaHang().Chitiet(id);
            if (nhaHang != null)
            {
                return View(nhaHang);
            }
            return Redirect("/Admin/NhaHang");

        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Update(NhaHang model)
        {
            if (new MapNhaHang().CapNhat(model))
            {
                return Redirect("/Admin/NhaHang");
            }
            return Redirect("/Admin/Nhahang");
            //return RedirectToAction("Update", "NhaHang", model);
        }

        [kiemTraQuyen(ChucNang = "NH_Xoa")]
        public ActionResult Delete(int id)
        {
            new MapNhaHang().Xoa(id);
            ViewBag.thongbao = new MapNhaHang().thongbao;
            return Redirect("/Admin/NhaHang");
        }
    }
}