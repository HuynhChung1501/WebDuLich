using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebDuLich.App_Start;
using WebDuLich.Areas.Admin.Models;
using WebDuLich.DB;
using WebDuLich.Models.ModelView.PhuongTiens;

namespace WebDuLich.Areas.Admin.Controllers
{
    [kiemTraQuyen]
    public class PhuongTienController : Controller
    {
        TestDataEntities db = new TestDataEntities();

        [kiemTraQuyen(ChucNang = "PT_XemDS")]
        // GET: Admin/PhuongTien
        public ActionResult Index()
        {
            List<PhuongTien> phuongTiens = db.PhuongTiens.ToList();
            return View(phuongTiens);
        }

        [kiemTraQuyen(ChucNang = "PT_ThemMoi")]
        public ActionResult Create()
        {

            return View(db.LoaiPhuongTiens.ToList());
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(PhuongTien model)
        {
            if (new MapPhuongTien().ThemMoi(model))
            {
                return Redirect("/Admin/PhuongTien/Create");
            }
            return Redirect("/Admin/PhuongTien/Create");
        }


        [kiemTraQuyen(ChucNang = "PT_ChinhSua")]
        public ActionResult Update(int id)
        {
            List<LoaiPhuongTien> loaiPhuongtiens = new MapPhuongTien().DanhSachLoaiPhuongTien();
            PhuongTien phuongTiens = new MapPhuongTien().Chitiet(id);
            if (phuongTiens != null)
            {
                var tenPhuongTien = "";
                foreach (var item in loaiPhuongtiens)
                {
                    if (item.IDPhuongTien == phuongTiens.ID)
                    {
                        tenPhuongTien = item.Ten;
                    }
                }

                return View("Update", new MapPhuongTienView()
                {
                    TenPhuongtien = tenPhuongTien,
                    PhuongTien = phuongTiens,
                    DanhSachLoaiPhuongTien = loaiPhuongtiens
                });
            }
            return Index();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Update(PhuongTien model)
        {
            if (new MapPhuongTien().CapNhat(model))
            {
                return Redirect("/Admin/PhuongTien");
            }

            return Redirect("/admin/phuongtien");
            //return RedirectToAction("Update", "PhuongTien", model);

        }

        [kiemTraQuyen(ChucNang = "PT_Xoa")]
        public ActionResult Delete(int id)
        {
            new MapPhuongTien().Xoa(id);
            ViewBag.thongbao = new MapPhuongTien().thongbao;
            return Redirect("/Admin/PhuongTien");
        }
    }
}