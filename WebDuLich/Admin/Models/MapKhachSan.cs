using Admin.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Admin.Models
{
    public class MapKhachSan
    {
        TestDataEntities db = new TestDataEntities();
        
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