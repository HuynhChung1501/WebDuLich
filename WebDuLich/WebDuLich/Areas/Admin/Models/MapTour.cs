using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebDuLich.DB;

namespace WebDuLich.Areas.Admin.Models
{
    public class MapTour
    {
        TestDataEntities2 db = new TestDataEntities2();
        public string thongbao = "";

        public List<Tour> DanhSach()
        {
            List<Tour> tours = db.Tours.ToList();
            return tours;
        }

        public Tour Chitiet(int id)
        {
            Tour tour = db.Tours.FirstOrDefault(n => n.ID == id);

            if (tour == null)
            {
                thongbao = "không tìm thấy Tour phù hợp";
                return null;
            }
            return tour;
        }

        public bool ThemMoi(Tour tour)
        {
            if (tour != null)
            {
                if (string.IsNullOrEmpty(tour.TenTour))
                {
                    thongbao = "Tên Tour không được để trống";
                    return false;
                }
                db.Tours.Add(tour);
                db.SaveChanges();
                thongbao = "Thêm mới tour thành công";
                return true;
            }
            thongbao = "Thêm mới tour thất bại";
            return false;
        }

        public bool CapNhatTour(Tour model)
        {

            Tour tour = db.Tours.FirstOrDefault(n => n.ID == model.ID);

            if (tour != null && model != null)
            {
                if (string.IsNullOrEmpty(model.TenTour))
                {
                    thongbao = "Tên Tour không được để trống";
                    return false;
                }

                tour.TenTour = model.TenTour;
                tour.GiaCu = model.GiaCu;
                tour.GiaMoi = model.GiaMoi;
                tour.BaiViet = model.BaiViet;
                tour.DiemDen = model.DiemDen;
                tour.IDDiaDiem = model.IDDiaDiem;
                tour.IDKhachSan = model.IDKhachSan;

                db.SaveChanges();
                thongbao = "Cập nhật tour thành công";
                return true;
            }
            thongbao = "Cập nhật tour thất bại";
            return false;
        }

        public bool Xoa(int id)
        {
            Tour tour = db.Tours.FirstOrDefault(n => n.ID == id);
            if (tour != null)
            {
                db.Tours.Remove(tour);
                db.SaveChanges();
                return true;
            }
            return false;

        }

        public List<DiaDiem> DsDiaDiem()
        {
            List<DiaDiem> diadiems = db.DiaDiems.ToList();
            return diadiems;

        }

        public List<KhachSan> DsKhachSan()
        {
            List<KhachSan> khachSans = db.KhachSans.ToList();
            return khachSans;

        }

    }
}