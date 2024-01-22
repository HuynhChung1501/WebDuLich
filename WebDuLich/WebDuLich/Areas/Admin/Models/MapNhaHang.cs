using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebDuLich.DB;

namespace WebDuLich.Areas.Admin.Models
{
    public class MapNhaHang
    {
        TestDataEntities db = new TestDataEntities();
        public string thongbao = "";

        public List<NhaHang> DanhSach(string searchName)
        {
            if (string.IsNullOrEmpty(searchName))
            {
                return db.NhaHangs.ToList();
            }

            var nhaHangs = from nh in db.NhaHangs
                           where nh.Ten.ToLowerInvariant().Contains(searchName.ToLowerInvariant()) || nh.DiaChi.ToLowerInvariant().Contains(searchName.ToLowerInvariant())
                           select nh;
            return nhaHangs.ToList();
        }

        public NhaHang Chitiet(int id)
        {
            NhaHang nhaHang = db.NhaHangs.FirstOrDefault(n => n.ID == id);

            if (nhaHang == null)
            {
                thongbao = "không tìm thấy nhà hàng phù hợp";
                return null;
            }
            return nhaHang;
        }

        public bool ThemMoi(NhaHang nhaHang)
        {
            if (nhaHang != null)
            {
                if (string.IsNullOrEmpty(nhaHang.Ten))
                {
                    thongbao = "Tên nhà hàng không được để trống";
                    return false;
                }
                db.NhaHangs.Add(nhaHang);
                db.SaveChanges();
                thongbao = "Thêm mới nhà hàng thành công";
                return true;
            }
            thongbao = "Thêm mới nhà hàng thất bại";
            return false;
        }

        public bool CapNhat(NhaHang model)
        {

            NhaHang nhaHang = db.NhaHangs.FirstOrDefault(n => n.ID == model.ID);

            if (nhaHang != null && model != null)
            {
                if (string.IsNullOrEmpty(model.Ten))
                {
                    thongbao = "Tên nhà hàng không được để trống";
                    return false;
                }

                if (string.IsNullOrEmpty(model.DiaChi))
                {
                    thongbao = "Tên nhà hàng không được để trống";
                    return false;
                }

                nhaHang.Ten = model.Ten;
                nhaHang.Gia = model.Gia;
                nhaHang.DiaChi = model.DiaChi;
                nhaHang.NoiDung = model.NoiDung;

                db.SaveChanges();
                thongbao = "Cập nhật nhà hàng thành công";
                return true;
            }
            thongbao = "Cập nhật nhà hàng thất bại";
            return false;
        }

        public bool Xoa(int id)
        {
            NhaHang nhaHang = db.NhaHangs.FirstOrDefault(n => n.ID == id);
            if (nhaHang != null)
            {
                db.NhaHangs.Remove(nhaHang);
                db.SaveChanges();
                return true;
            }
            return false;

        }

    }
}