using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebDuLich.DB;

namespace WebDuLich.Areas.Admin.Models
{
    public class MapChucNang
    {
        TestDataEntities2 db = new TestDataEntities2();
        public string thongbao = "";

        public List<ChucNang> DanhSach()
        {
            return db.ChucNangs.ToList();
        }

        public ChucNang Chitiet(string maChucNang)
        {
            ChucNang chucNang = db.ChucNangs.FirstOrDefault(n => n.MaChucNang == maChucNang);

            if (chucNang == null)
            {
                thongbao = "không tìm thấy chức năng phù hợp";
                return null;
            }
            return chucNang;
        }
    }
}