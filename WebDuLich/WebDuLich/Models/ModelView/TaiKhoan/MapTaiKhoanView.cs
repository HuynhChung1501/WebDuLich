using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebDuLich.DB;

namespace WebDuLich.Models.ModelView.TaiKhoan
{
    public class MapTaiKhoanView
    {
        public List<User> Users { get; set; }

        public User User { get; set; }

        public string UserName { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
        public string SoDienThoai { get; set; }
        public int IsActive { get; set; }
        public string Avatar { get; set; }

    }
}