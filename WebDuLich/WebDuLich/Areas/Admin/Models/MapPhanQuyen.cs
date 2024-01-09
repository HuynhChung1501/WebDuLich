using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebDuLich.DB;

namespace WebDuLich.Areas.Admin.Models
{
    public class MapPhanQuyen
    {
        TestDataEntities db = new TestDataEntities();
        public string thongbao = "";

        public List<PhanQuyen> DanhSach()
        {
            return db.PhanQuyens.ToList();
        }

        public PhanQuyen Chitiet(string maChucNang)
        {
            PhanQuyen phanQuyen = db.PhanQuyens.FirstOrDefault(n => n.MaChucNang == maChucNang);

            if (phanQuyen == null)
            {
                thongbao = "không tìm thấy quyền phù hợp";
                return null;
            }
            return phanQuyen;
        }
    }
}