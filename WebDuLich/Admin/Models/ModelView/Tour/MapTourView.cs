using Admin.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Admin.Models.ModelView.Tour
{
    public class MapTourView
    {
        public List<KhachSan> khachSans { get; set; }

        public List<DiaDiem> diaDiems { get; set; }

        public List<Admin.DB.Tour> tours { get; set; }

        public Admin.DB.Tour tour { get; set; }

        public DiaDiem diaDiem { get; set; }

        public KhachSan khachSan { get; set; }
    }
}