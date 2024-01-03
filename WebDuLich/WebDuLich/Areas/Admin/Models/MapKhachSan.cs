using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebDuLich.DB;

namespace WebDuLich.Areas.Admin.Models
{
    public class MapKhachSan
    {

        TestDataEntities2 db = new TestDataEntities2();

        public List<KhachSan> DanhSach()
        {
            return db.KhachSans.ToList();
        }

        public KhachSan ChiTiet(int id)
        {
            KhachSan khachSan = db.KhachSans.SingleOrDefault(n => n.ID == id);
            return khachSan;
        }
    }
}