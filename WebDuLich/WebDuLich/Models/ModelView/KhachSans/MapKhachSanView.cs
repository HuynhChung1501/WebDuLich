using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebDuLich.DB;

namespace WebDuLich.Models.ModelView.KhachSans
{
    public class MapKhachSanView
    {
        public List<KhachSan> DSKhachSans { get; set; }

        public KhachSan khachSan { get; set; }

        public List<MucGia> DSMucGia { get; set;}
    }
}