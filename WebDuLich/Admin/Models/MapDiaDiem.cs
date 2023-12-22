using Admin.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Admin.Models
{
    public class MapDiaDiem
    {
        TestDataEntities db = new TestDataEntities();

        public List<DiaDiem> DanhSach()
        {
            return db.DiaDiems.ToList();
        }

        public DiaDiem ChiTiet(int id)
        {
            DiaDiem diaDiem = db.DiaDiems.SingleOrDefault(n => n.ID == id);
            return diaDiem;
        }
    }
}