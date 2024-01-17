using Antlr.Runtime.Misc;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Linq;
using WebDuLich.DB;
using WebDuLich.Models.ModelView.PhuongTiens;

namespace WebDuLich.Areas.Admin.Models
{
    public class MapPhuongTien
    {
        TestDataEntities db = new TestDataEntities();
        public string thongbao = "";

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

                var checkCode = db.PhuongTiens.Where(n => n.Code.ToLower().Contains(model.Code.ToLower()));

                if (checkCode != null)
                {
                    thongbao = "Trường mã phương tiện đã tồn tại";
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
                PhuongTien.Ten = model.Ten;
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

        public List<MapDBPhuongTien> DanhSach(int? IDPhuongTien, int? IDMucGia)
        {

            var phuongTiens = from item in db.PhuongTiens
                              from LPhuongTien in db.LoaiPhuongTiens
                              where (LPhuongTien.IDPhuongTien == IDPhuongTien || IDPhuongTien == 0) && item.IDPhuongTien == LPhuongTien.IDPhuongTien
                              select new MapDBPhuongTien
                              {
                                  ID = item.ID,
                                  Code = item.Code,
                                  Ten= item.Ten,
                                  ChoNgoi = item.ChoNgoi,
                                  Gia = (int)item.Gia,
                                  HinhAnh = item.HinhAnh,
                                  NoiDung = item.NoiDung,
                                  IDPhuongTien = (int)LPhuongTien.IDPhuongTien,
                                  NgayTao = item.NgayTao,
                                  NguoiTao = item.NguoiTao,
                                  TenLoaiPhuongTien = LPhuongTien.Ten
                              };


            switch (IDMucGia)
            {
                case 0:
                    break;

                case 1:
                    phuongTiens = phuongTiens.Where(n => n.Gia < 1000000);
                    break;
                case 2:
                    phuongTiens = phuongTiens.Where(n => n.Gia >= 1000000 && n.Gia < 6000000);
                    break;
                case 3:
                    phuongTiens = phuongTiens.Where(n => n.Gia >= 6000000 && n.Gia < 11000000);
                    break;
                case 4:
                    phuongTiens = phuongTiens.Where(n => n.Gia >= 11000000);
                    break;
            }


            return phuongTiens.ToList();
        }

    }
}