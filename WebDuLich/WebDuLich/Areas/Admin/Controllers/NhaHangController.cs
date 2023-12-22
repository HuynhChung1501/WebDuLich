using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebDuLich.Areas.Admin.Models;
using WebDuLich.Areas.Admin.Models.ModelView.NhaHang;
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
            return View(model);
        }


        public ActionResult Update(int id)
        {
            NhaHang nhaHang = new MapNhaHang().Chitiet(id);
            if (nhaHang != null)
            {
                return RedirectToAction("Edit", "NhaHang", nhaHang);
            }
            return View("Index");
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Update(NhaHang model)
        {
            if (new MapNhaHang().ThemMoi(model))
            {
                return View("Index");
            }

            return RedirectToAction("Edit", "NhaHang", model);
        }

        public ActionResult Delete(int id)
        {
            new MapNhaHang().Xoa(id);
            ViewBag.thongbao = new MapNhaHang().thongbao;
            return View("index");
        }
    }
}