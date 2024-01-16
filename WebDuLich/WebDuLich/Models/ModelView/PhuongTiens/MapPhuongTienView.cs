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

        public string TenPhuongtien { get; set; }

        public List<MapDBPhuongTien> DanhSachPhuongTien { get; set; }

        public List<MucGia> DSMucGia { get; set; }


    }

    public class MapDBPhuongTien
    {
        public int ID { get; set; }

        public string Code { get; set; }

        public string Ten { get; set; }

        public int? ChoNgoi { get; set; }

        public int? Gia { get; set; }

        public string HinhAnh { get; set; }

        public string NoiDung { get; set; }

        public int IDPhuongTien { get; set; }
        
        public string TenLoaiPhuongTien { get; set; }

        public DateTime? NgayTao { get; set; }

        public string NguoiTao { get; set; }
    }
}