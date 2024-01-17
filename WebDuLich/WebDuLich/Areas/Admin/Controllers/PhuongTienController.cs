using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebDuLich.App_Start;
using WebDuLich.Areas.Admin.Models;
using WebDuLich.DB;
using WebDuLich.Models.ModelView.KhachSans;
using WebDuLich.Models.ModelView.PhuongTiens;
using WebDuLich.Common;
using System.IO;
using OfficeOpenXml;
using DocumentFormat.OpenXml.Spreadsheet;
using System.Windows;
using Microsoft.Office.Interop.Excel;
using System.Text.RegularExpressions;

namespace WebDuLich.Areas.Admin.Controllers
{
    [kiemTraQuyen]
    public class PhuongTienController : Controller
    {
        TestDataEntities db = new TestDataEntities();

        [kiemTraQuyen(ChucNang = "PT_XemDS")]
        // GET: Admin/PhuongTien
        public ActionResult Index()
        {
            List<MapDBPhuongTien> phuongTiens = new MapPhuongTien().DanhSach(0, 0);
            return View(new MapPhuongTienView()
            {
                DanhSachPhuongTien = phuongTiens,
                DSMucGia = db.MucGias.ToList(),
                DanhSachLoaiPhuongTien = db.LoaiPhuongTiens.ToList(),
            });
        }

        [kiemTraQuyen(ChucNang = "PT_ThemMoi")]
        public ActionResult Create()
        {

            return View(db.LoaiPhuongTiens.ToList());
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(PhuongTien model)
        {
            if (new MapPhuongTien().ThemMoi(model))
            {
                return Redirect("/Admin/PhuongTien/Create");
            }
            return Redirect("/Admin/PhuongTien/Create");
        }


        [kiemTraQuyen(ChucNang = "PT_ChinhSua")]
        public ActionResult Update(int id)
        {
            List<LoaiPhuongTien> loaiPhuongtiens = new MapPhuongTien().DanhSachLoaiPhuongTien();
            PhuongTien phuongTiens = new MapPhuongTien().Chitiet(id);
            if (phuongTiens != null)
            {
                var tenPhuongTien = "";
                foreach (var item in loaiPhuongtiens)
                {
                    if (item.IDPhuongTien == phuongTiens.ID)
                    {
                        tenPhuongTien = item.Ten;
                    }
                }

                return View("Update", new MapPhuongTienView()
                {
                    TenPhuongtien = tenPhuongTien,
                    PhuongTien = phuongTiens,
                    DanhSachLoaiPhuongTien = loaiPhuongtiens
                });
            }
            return Index();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Update(PhuongTien model)
        {
            if (new MapPhuongTien().CapNhat(model))
            {
                return Redirect("/Admin/PhuongTien");
            }

            return Redirect("/admin/phuongtien");
            //return RedirectToAction("Update", "PhuongTien", model);

        }

        [kiemTraQuyen(ChucNang = "PT_Xoa")]
        public ActionResult Delete(int id)
        {
            new MapPhuongTien().Xoa(id);
            ViewBag.thongbao = new MapPhuongTien().thongbao;
            return Redirect("/Admin/PhuongTien");
        }

        public ActionResult TimKiem(int IDPhuongTien, int IDMucGia)
        {
            List<MapDBPhuongTien> phuongTiens = new MapPhuongTien().DanhSach(IDPhuongTien, IDMucGia);

            return PartialView("_Detail", new MapPhuongTienView()
            {
                DanhSachPhuongTien = phuongTiens,
                DSMucGia = db.MucGias.ToList(),
                DanhSachLoaiPhuongTien = db.LoaiPhuongTiens.ToList(),
            });
        }

        public ActionResult ExportExcel()
        {
            var wb = new XLWorkbook();

            var ws = wb.Worksheets.Add("Contacts");
            ws.Range("A2:G2").Style.Font.Bold = true;
            var rngHeader = ws.Range("A1:G1");
            rngHeader.Merge();
            rngHeader.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
            rngHeader.Style.Alignment.SetVertical(XLAlignmentVerticalValues.Center);
            rngHeader.Value = "Danh sách Phương tiện";
            rngHeader.Style.Fill.BackgroundColor = XLColor.Aqua;
            rngHeader.Style.Font.Bold = true;

            // Title
            ws.Cell(2, 1).Value = "STT";
            ws.Cell(2, 2).Value = "Tên phương tiện";
            ws.Cell(2, 3).Value = "Chỗ ngồi";
            ws.Cell(2, 4).Value = "Giá";
            ws.Cell(2, 5).Value = "Ngày tạo";
            ws.Cell(2, 6).Value = "Người tạo";
            ws.Cell(2, 7).Value = "Nội dung";

            int row = 3;

            List<MapDBPhuongTien> phuongTiens = new MapPhuongTien().DanhSach(0, 0);
            var mapPhuongTien = new MapPhuongTienView()
            {
                DanhSachPhuongTien = phuongTiens,
                DanhSachLoaiPhuongTien = db.LoaiPhuongTiens.ToList(),
            };

            for (int i = 0; i < mapPhuongTien.DanhSachPhuongTien.Count; i++)
            {
                ws.Cell(row, 1).Value = i;
                foreach (var item in mapPhuongTien.DanhSachLoaiPhuongTien)
                {
                    if (item.IDPhuongTien == mapPhuongTien.DanhSachPhuongTien[i].IDPhuongTien)
                    {
                        ws.Cell(row, 2).Value = item.Ten;
                        break;
                    }
                    else
                    {
                        ws.Cell(row, 2).Value = "không có phương tiện";
                    }
                }
                ws.Cell(row, 3).Value = mapPhuongTien.DanhSachPhuongTien[i].ChoNgoi;
                ws.Cell(row, 4).Value = mapPhuongTien.DanhSachPhuongTien[i].Gia;
                ws.Cell(row, 5).Value = mapPhuongTien.DanhSachPhuongTien[i].NgayTao;
                ws.Cell(row, 6).Value = mapPhuongTien.DanhSachPhuongTien[i].NguoiTao;
                ws.Cell(row, 7).Value = Base.StripHTML(mapPhuongTien.DanhSachPhuongTien[i].NoiDung);
                row++;
            }

            string namefile = "Export_" + DateTime.Now.Ticks + ".xlsx";
            string pathFile = Server.MapPath("~/Data/ExportExcel/" + namefile);

            wb.SaveAs(pathFile);
            return Json(namefile, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ImportExcel(object sender)
        {
            List<PhuongTien> listPT = new List<PhuongTien>();
            try
            {
                // mở file excel

                string pathFile = Server.MapPath("~/Data/ImportExcel/ImportData.xlsx");

                var wbook = new XLWorkbook(pathFile);

                var ws1 = wbook.Worksheet(1);
                var data = ws1.Cell("A1").GetValue<string>();

                var LastRowCount = ws1.LastRowUsed().RowNumber();

                // duyệt tuần tự từ dòng thứ 2 đến dòng cuối cùng của file. lưu ý file excel bắt đầu từ số 1 không phải số 0
                for (int i = 3; i <= LastRowCount; i++)
                {
                    try
                    {
                        // lấy ra cột họ tên tương ứng giá trị tại vị trí [i, 1]. i lần đầu là 2
                        // tăng j lên 1 đơn vị sau khi thực hiện xong câu lệnh
                        //var ID = ws1.Cell(i, 1).GetValue<int>();

                        // lấy ra cột ngày sinh tương ứng giá trị tại vị trí [i, 2]. i lần đầu là 2
                        // tăng j lên 1 đơn vị sau khi thực hiện xong câu lệnh
                        // lấy ra giá trị ngày tháng và ép kiểu thành DateTime                      
                        var ChoNgoi = ws1.Cell(i, 2).GetValue<int>();
                        var Gia = ws1.Cell(i, 3).GetValue<int>();
                        string NoiDung = ws1.Cell(i, 4).GetValue<string>();
                        string NguoiTao = ws1.Cell(i, 5).GetValue<string>();

                        // tạo UserInfo từ dữ liệu đã lấy được
                        PhuongTien Phuongtien = new PhuongTien()
                        {
                            ChoNgoi = ChoNgoi,
                            Gia = Gia,
                            NoiDung = NoiDung,
                            NguoiTao = NguoiTao,
                        };

                        // add Phuongtien vào danh sách listPT
                        listPT.Add(Phuongtien);

                    }

                    catch (Exception exe)
                    {

                    }
                }
                foreach (PhuongTien item in listPT)
                {
                    db.PhuongTiens.Add(item);
                    db.SaveChanges();
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show("Error!");
            }
            return Redirect("/admin/phuongtien");
        }
    }
}