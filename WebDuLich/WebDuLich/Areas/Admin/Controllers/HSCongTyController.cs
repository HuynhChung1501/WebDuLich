using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebDuLich.Areas.Admin.Models;
using WebDuLich.DB;
using WebDuLich.Models.ModelView.HSCongTys;
using WebDuLich.Models.ModelView.KhachSans;

namespace WebDuLich.Areas.Admin.Controllers
{
    public class HSCongTyController : Controller
    {
        TestDataEntities db = new TestDataEntities();
        // GET: Admin/HSCongTy
        public ActionResult Index()
        {
            List<HoSoCongTy> hoSos = db.HoSoCongTies.ToList();
            if (TempData["result"] != null)
            {
                ViewBag.success = TempData["result"];
            }
            return View(new MapHSCongTyView()
            {
                DanhSachHoSoCongTy = hoSos
            });
        }

        public ActionResult Create()
        {

            if (TempData["result"] != null)
            {
                ViewBag.success = TempData["result"];
            }
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(HoSoCongTy model)
        {
            MapHSCongTy mapTaiKhoan = new MapHSCongTy();
            new MapHSCongTy().ThemMoi(model);
            TempData["result"] = "Thêm mới thành công!";
            return Redirect("/Admin/HSCongTy");
        }

        public JsonResult JsonCreate(HoSoCongTy model)
        {
            MapHSCongTy mapTaiKhoan = new MapHSCongTy();
            
            if (new MapHSCongTy().ThemMoi(model))
            {
                TempData["result"] = "Thêm mới thành công!";
            }
            return Json(new
            {
                data = "/Admin/HSCongTy/Index"
            });
        }

        public ActionResult Update(int id)
        {
            if (TempData["result"] != null)
            {
                ViewBag.success = TempData["result"];
            }
            HoSoCongTy HSCongTy = new MapHSCongTy().Chitiet(id);
            if (HSCongTy != null)
            {
                return View(HSCongTy);
            }
            return Redirect("/Admin/HSCongTy");
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Update(HoSoCongTy model)
        {
            if (new MapHSCongTy().CapNhat(model))
            {
                TempData["result"] = "Chỉnh sửa thành công!";
                return Redirect("/Admin/HSCongTy");
            }
            TempData["result"] = "Chỉnh sửa không thành công!";
            return Redirect("/Admin/HSCongTy");
        }

        public ActionResult Delete(int id)
        {
            if (new MapHSCongTy().Xoa(id))
            {
                TempData["result"] = "Xóa thành công!";
                return Redirect("/Admin/HSCongTy");
            }
            TempData["result"] = "Xóa không thành công!";
            return Redirect("/Admin/HSCongTy");
        }

        public ActionResult TimKiem(string searchName)
        {
            List<HoSoCongTy> hs = new MapHSCongTy().DanhSach(searchName);
            return PartialView("_Detail", new MapHSCongTyView()
            {
                DanhSachHoSoCongTy = hs
            }); ;
        }

        public JsonResult ActiveCongtyController(int? id)
        {
            HoSoCongTy congty = db.HoSoCongTies.FirstOrDefault(n => n.ID == id);
            List<HoSoCongTy> listCongTy = db.HoSoCongTies.ToList();
            foreach (var item in listCongTy)
            {
                item.IsActive = false;
                db.SaveChanges();
            }
            if (congty.IsActive == true)
            {
                congty.IsActive = false;
                db.SaveChanges();
            }
            else
            {
                congty.IsActive = true;
                db.SaveChanges();
            }
            return Json(new
            {
                status = "ok"
            });
        }


    }
}