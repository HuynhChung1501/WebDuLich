using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebDuLich.Areas.Admin.Models;
using WebDuLich.DB;

namespace WebDuLich.Areas.Admin.Controllers
{
    public class NhaHangController : Controller
    {
        TestDataEntities2 db = new TestDataEntities2();

        // GET: Admin/NhaHang
        public ActionResult Index()
        {
            List<NhaHang> nhaHangs = db.NhaHangs.ToList();
            return View(nhaHangs);
        }

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
            if (new MapNhaHang().CapNhatTour(model))
            {
                return Redirect("/Admin/NhaHang");
            }
            return Redirect("/Admin/Nhahang");
            //return RedirectToAction("Update", "NhaHang", model);
        }

        public ActionResult Delete(int id)
        {
            new MapNhaHang().Xoa(id);
            ViewBag.thongbao = new MapNhaHang().thongbao;
            return Redirect("/Admin/NhaHang");
        }
    }
}