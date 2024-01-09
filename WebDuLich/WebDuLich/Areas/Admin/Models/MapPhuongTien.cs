using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebDuLich.DB;

namespace WebDuLich.Areas.Admin.Models
{
    public class MapPhuongTien
    {
        TestDataEntities db = new TestDataEntities();
        public string thongbao = "";

        public List<PhuongTien> DanhSach()
        {
            List<PhuongTien> PhuongTiens = db.PhuongTiens.ToList();
            return PhuongTiens;
        }

        public List<LoaiPhuongTien> DanhSachLoaiPhuongTien()
        {
            List<LoaiPhuongTien> LoaiPhuongTiens = db.LoaiPhuongTiens.ToList();
            return LoaiPhuongTiens;
        }

        public PhuongTien Chitiet(int id)
        {
            PhuongTien PhuongTien = db.PhuongTiens.FirstOrDefault(n => n.ID == id);

            if (PhuongTien == null)
            {
                thongbao = "không tìm thấy phương tiện phù hợp";
                return null;
            }
            return PhuongTien;
        }

        public bool ThemMoi(PhuongTien model)
        {
            if (model != null)
            {
                if (model.IDPhuongTien <= 0)
                {
                    thongbao = "Trường phương tiện không để để trống";
                    return false;
                }
                db.PhuongTiens.Add(model);
                db.SaveChanges();
                thongbao = "Thêm mới thông báo thành công";
                return true;
            }
            return false;
        }

        public bool CapNhat(PhuongTien model)
        {

            PhuongTien PhuongTien = db.PhuongTiens.FirstOrDefault(n => n.ID == model.ID);

            if (PhuongTien != null && model != null)
            {
                if (model.IDPhuongTien <= 0)
                {
                    thongbao = "Tên phương tiện không được để trống";
                    return false;
                }

                if (model.ChoNgoi > 0)
                {
                    thongbao = "Tên phương tiện không được để trống";
                    return false;
                }

                PhuongTien.IDPhuongTien = model.IDPhuongTien;
                PhuongTien.ChoNgoi = model.ChoNgoi;
                PhuongTien.Gia = model.Gia;
                PhuongTien.HinhAnh = model.HinhAnh;
                PhuongTien.NoiDung = model.NoiDung;

                db.SaveChanges();
                thongbao = "Cập nhật phương tiện thành công";
                return true;
            }
            thongbao = "Cập nhật phương tiện thất bại";
            return false;
        }

        public bool Xoa(int id)
        {
            PhuongTien PhuongTien = db.PhuongTiens.FirstOrDefault(n => n.ID == id);
            if (PhuongTien != null)
            {
                db.PhuongTiens.Remove(PhuongTien);
                db.SaveChanges();
                return true;
            }
            return false;

        }

    }
}