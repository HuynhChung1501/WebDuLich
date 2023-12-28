using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebDuLich.DB;

namespace WebDuLich.Areas.Admin.Models
{
    public class MapTaiKhoan
    {
        TestDataEntities2 db = new TestDataEntities2();

        public string thongbao = "";

        public List<User> DanhSach()
        {
            List<User> users = db.Users.ToList();
            return users;
        }

        public User Chitiet(string userName, string pass = "")
        {
            if(!string.IsNullOrEmpty(pass))
            {
                User tk = db.Users.FirstOrDefault(n => n.UserName == userName && n.Password == pass);
                if (tk != null)
                {
                    return tk;
                }else
                {
                    return null;
                }
            }

            User user = db.Users.FirstOrDefault(n => n.UserName == userName);

            if (user != null)
            {
                thongbao = "không tìm thấy tài khoản phù hợp";
                return user;
            }
            return null;
        }

        public bool ThemMoi(User user)
        {
            if (user != null)
            {
                if (string.IsNullOrEmpty(user.UserName) )
                {
                    thongbao = "Tên tài khoản không được để trống";
                    return false;
                }
                 if (Chitiet(user.UserName) != null)
                {
                    thongbao = "Tên tài khoản đã tồn tại";
                    return false;
                }
                if (string.IsNullOrEmpty(user.Password))
                {
                    thongbao = "mật khẩu không được để trống";
                    return false;
                }
                db.Users.Add(user);
                db.SaveChanges();
                thongbao = "Thêm mới tài khoản thành công";
                return true;
            }
            thongbao = "Thêm mới tài khoản thất bại";
            return false;
        }

        public bool CapNhat(User model)
        {

            User user = db.Users.FirstOrDefault(n => n.UserName == model.UserName);

            if (user != null && model != null)
            {
                if (string.IsNullOrEmpty(model.UserName))
                {
                    thongbao = "Tên tài khoản không được để trống";
                    return false;
                }

                if (string.IsNullOrEmpty(model.Password))
                {
                    thongbao = "mật khẩu không được để trống";
                    return false;
                }

                user.Password = model.Password;
                user.Avatar = model.Avatar;
                user.FullName = model.FullName;
                user.SoDienThoai = model.SoDienThoai;

                db.SaveChanges();
                thongbao = "Cập nhật tài khoản thành công";
                return true;
            }
            thongbao = "Cập nhật tài khoản thất bại";
            return false;
        }

        public bool Xoa(string userName)
        {
            User user = db.Users.FirstOrDefault(n => n.UserName == userName);
            if (user != null)
            {
                db.Users.Remove(user); 
                db.SaveChanges();
                return true;
            }
            return false;

        }
    }
}