using Admin.DB;
using Admin.Models;
using Admin.Models.ModelView.Tour;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Admin.Controllers
{
    public class TourController : Controller
    {
        TestDataEntities db = new TestDataEntities();
        // GET: Tour
        public ActionResult Index()
        {
            var model = new MapTour();
            List<Tour> tours = model.DanhSach();
            List<DiaDiem> diadiems = model.DsDiaDiem();
            List<KhachSan> khachsans = model.DsKhachSan();

            return View(new MapTourView
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
                return Create();
            }
            ViewBag.thongbap = maptour.thongbao;
            return Create();

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
                return Update(model.ID);
            }
            return Update(model.ID);

        }

        public ActionResult Delete(int id)
        {
            
            new MapTour().Xoa(id);
            return RedirectToAction("Index", "Tour");
        }

    }
}