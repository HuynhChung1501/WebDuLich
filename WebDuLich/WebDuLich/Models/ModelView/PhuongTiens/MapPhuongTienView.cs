using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebDuLich.DB;

namespace WebDuLich.Models.ModelView.PhuongTiens
{
    public class MapPhuongTienView
    {
        public List<LoaiPhuongTien> DanhSachLoaiPhuongTien { get; set; }

        public PhuongTien PhuongTien { get; set; }

        public string TenPhuongtien {  get; set; }

        public List<PhuongTien> DanhSachPhuongTien { get; set; }
    }
}