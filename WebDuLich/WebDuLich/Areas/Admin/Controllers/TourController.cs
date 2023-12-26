using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebDuLich.Areas.Admin.Models;
using WebDuLich.DB;
using WebDuLich.Models.ModelView.Tours;

namespace WebDuLich.Areas.Admin.Controllers
{
    public class TourController : Controller
    {
        TestDataEntities2 db = new TestDataEntities2();
        // GET: Tour
        public ActionResult Index()
        {
            var model = new MapTour();
            List<Tour> tours = model.DanhSach();
            List<DiaDiem> diadiems = model.DsDiaDiem();
            List<KhachSan> khachsans = model.DsKhachSan();

            return View("Index",new MapTourView
            {
                tours = tours,
                khachSans = khachsans,
                diaDiems = diadiems,

            });
        }

        public ActionResult Create()
        {
            var maptour = new MapTour();
            List<DiaDiem> diadiems = maptour.DsDiaDiem();
            List<KhachSan> khachsans = maptour.DsKhachSan();

            return View(new MapTourView
            {
                diaDiems = diadiems,
                khachSans = khachsans,
            });
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(Tour tour)
        {
            var maptour = new MapTour();
            if (new MapTour().ThemMoi(tour))
            {
                ViewBag.thongbap = maptour.thongbao;
                return Redirect("/Admin/Tour");

                
            }
            ViewBag.thongbap = maptour.thongbao;
            return Create(tour);

        }

        public ActionResult Update(int id)
        {
            MapTour mapTour = new MapTour();
            Tour tour = mapTour.Chitiet(id);
            List<KhachSan> khachSans = new MapKhachSan().DanhSach();
            List<DiaDiem> diaDiems = new MapDiaDiem().DanhSach();

            MapTourView mapTourView = new MapTourView()
            {
                tour = tour,
                diaDiems = diaDiems,
                khachSans = khachSans
            };

            if (tour != null)
            {
                return View(mapTourView);
            }
            return View(mapTourView);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Update(Tour model)
        {
            MapTour tour = new MapTour();
            if (tour.CapNhatTour(model))
            {
                return Redirect("/Admin/Tour");
            }
                return Update(model.ID);

        }

        public ActionResult Delete(int id)
        {
            new MapTour().Xoa(id);
            return Redirect("/Admin/Tour");
        }

    }
}