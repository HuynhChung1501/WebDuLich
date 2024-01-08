﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebDuLich.DB;

namespace WebDuLich.Areas.Admin.Models
{
    public class MapKhachSan
    {

        TestDataEntities db = new TestDataEntities();
        public string thongbao = "";

        public List<KhachSan> DanhSach()
        {
            return db.KhachSans.ToList();
        }

        public KhachSan ChiTiet(int id)
        {
            KhachSan khachSan = db.KhachSans.SingleOrDefault(n => n.ID == id);
            return khachSan;
        }

        public bool ThemMoi(KhachSan khachSan)
        {
            if (khachSan != null)
            {
                if (string.IsNullOrEmpty(khachSan.Ten))
                {
                    thongbao = "Tên khách sạn không được để trống";
                    return false;
                }
                db.KhachSans.Add(khachSan);
                db.SaveChanges();
                thongbao = "Thêm mới khách sạn thành công";
                return true;
            }
            thongbao = "Thêm mới khách sạn thất bại";
            return false;
        }

        public bool CapNhatKhachSan(KhachSan model)
        {

            KhachSan khachSan = db.KhachSans.FirstOrDefault(n => n.ID == model.ID);

            if (khachSan != null && model != null)
            {
                if (string.IsNullOrEmpty(model.Ten))
                {
                    thongbao = "Tên khách sạn không được để trống";
                    return false;
                }

                if (string.IsNullOrEmpty(model.DiaChi))
                {
                    thongbao = "Tên khách sạn không được để trống";
                    return false;
                }

                khachSan.Ten = model.Ten;
                khachSan.DiaChi = model.DiaChi;
                khachSan.Noidung = model.Noidung;
                khachSan.Gia = model.Gia;
                khachSan.HinhAnh = model.HinhAnh;

                db.SaveChanges();
                thongbao = "Cập nhật khách sạn thành công";
                return true;
            }
            thongbao = "Cập nhật khách sạn thất bại";
            return false;
        }

        public bool Xoa(int id)
        {
            KhachSan khachSan = db.KhachSans.FirstOrDefault(n => n.ID == id);
            if (khachSan != null)
            {
                db.KhachSans.Remove(khachSan);
                db.SaveChanges();
                return true;
            }
            return false;

        }

    }
}