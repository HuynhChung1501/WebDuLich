using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebDuLich.DB;

namespace WebDuLich.Models.ModelView.Tours
{
    public class MapTourView
    {
        public List<KhachSan> khachSans { get; set; }

        public List<DiaDiem> diaDiems { get; set; }

        public List<Tour> tours { get; set; }

        public Tour tour { get; set; }

        public DiaDiem diaDiem { get; set; }

        public KhachSan khachSan { get; set; }
    }
}