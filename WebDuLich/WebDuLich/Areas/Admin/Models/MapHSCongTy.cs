using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebDuLich.DB;

namespace WebDuLich.Areas.Admin.Models
{
    public class MapHSCongTy
    {
        TestDataEntities db = new TestDataEntities();
        public string thongbao = "";

        public List<HoSoCongTy> DanhSach(string searchName)
        {
            if (string.IsNullOrEmpty(searchName))
            {
                return db.HoSoCongTies.ToList();
            }

            var HoSoCongTies = from i in db.HoSoCongTies
                           where i.Ten.ToLower().Contains(searchName.ToLower())
                           select i;

            return HoSoCongTies.ToList();
        }

        public HoSoCongTy Chitiet(int id)
        {
            HoSoCongTy HoSoCongTy = db.HoSoCongTies.FirstOrDefault(n => n.ID == id);

            if (HoSoCongTy == null)
            {
                thongbao = "không tìm thấy công ty phù hợp";
                return null;
            }
            return HoSoCongTy;
        }

        public bool ThemMoi(HoSoCongTy HoSoCongTy)
        {
            if (HoSoCongTy != null)
            {
                if (string.IsNullOrEmpty(HoSoCongTy.Ten))
                {
                    return false;
                }
                if (string.IsNullOrEmpty(HoSoCongTy.Maso))
                {
                    return false;
                }
                db.HoSoCongTies.Add(HoSoCongTy);
                db.SaveChanges();
                thongbao = "Thêm mới công ty thành công";
                return true;
            }
            thongbao = "Thêm mới công ty thất bại";
            return false;
        }

        public bool CapNhat(HoSoCongTy model)
        {
            HoSoCongTy HoSoCongTy = db.HoSoCongTies.FirstOrDefault(n => n.ID == model.ID);

            if (HoSoCongTy != null && model != null)
            {
                if (string.IsNullOrEmpty(model.Ten))
                {
                    thongbao = "Tên công ty không được để trống";
                    return false;
                }

                if (string.IsNullOrEmpty(model.DiaChi))
                {
                    thongbao = "Tên công ty không được để trống";
                    return false;
                }

                HoSoCongTy.Ten = model.Ten;
                HoSoCongTy.DienThoai = model.DienThoai;
                HoSoCongTy.Email = model.Email;
                HoSoCongTy.GioiThieu = model.GioiThieu;
                HoSoCongTy.DiaChi = model.DiaChi;
                HoSoCongTy.Logo = model.Logo;
                HoSoCongTy.Maso = model.Maso;
                HoSoCongTy.NgayTao = model.NgayTao;
                HoSoCongTy.NguoiTao = model.NguoiTao;

                db.SaveChanges();
                thongbao = "Cập nhật công ty thành công";
                return true;
            }
            thongbao = "Cập nhật công ty thất bại";
            return false;
        }

        public bool Xoa(int id)
        {
            HoSoCongTy HoSoCongTy = db.HoSoCongTies.FirstOrDefault(n => n.ID == id);
            if (HoSoCongTy != null)
            {
                db.HoSoCongTies.Remove(HoSoCongTy);
                db.SaveChanges();
                return true;
            }
            return false;

        }
    }
}