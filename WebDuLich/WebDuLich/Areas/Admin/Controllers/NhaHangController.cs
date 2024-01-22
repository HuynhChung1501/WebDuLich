using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Windows;
using WebDuLich.App_Start;
using WebDuLich.Areas.Admin.Models;
using WebDuLich.DB;
using WebDuLich.Models.ModelView.PhuongTiens;
using WebDuLich.Common;
using WebDuLich.Models.ModelView.NhaHang;

namespace WebDuLich.Areas.Admin.Controllers
{
    [kiemTraQuyen]
    public class NhaHangController : Controller
    {
        TestDataEntities db = new TestDataEntities();

        [kiemTraQuyen(ChucNang = "NH_XemDS")]
        // GET: Admin/NhaHang
        public ActionResult Index()
        {
            List<NhaHang> nhaHangs = db.NhaHangs.ToList();
            return View(nhaHangs);
        }

        [kiemTraQuyen(ChucNang = "NH_ThemMoi")]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(NhaHang model)
        {
            new MapNhaHang().ThemMoi(model);
            ViewBag.thongbao = new MapNhaHang().thongbao;
            return Redirect("/Admin/NhaHang");

        }

        [kiemTraQuyen(ChucNang = "NH_ChinhSua")]
        public ActionResult Update(int id)
        {
            NhaHang nhaHang = new MapNhaHang().Chitiet(id);
            if (nhaHang != null)
            {
                return View(nhaHang);
            }
            return Redirect("/Admin/NhaHang");

        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Update(NhaHang model)
        {
            if (new MapNhaHang().CapNhat(model))
            {
                return Redirect("/Admin/NhaHang");
            }
            return Redirect("/Admin/Nhahang");
            //return RedirectToAction("Update", "NhaHang", model);
        }

        [kiemTraQuyen(ChucNang = "NH_Xoa")]
        public ActionResult Delete(int id)
        {
            new MapNhaHang().Xoa(id);
            ViewBag.thongbao = new MapNhaHang().thongbao;
            return Redirect("/Admin/NhaHang");
        }

        public ActionResult TimKiem(string searchName)
        {
            List<NhaHang> nhaHangs = new MapNhaHang().DanhSach(searchName);
            return PartialView("_Detail", nhaHangs);
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
            rngHeader.Value = "Danh sách nhà hàng";
            rngHeader.Style.Fill.BackgroundColor = XLColor.Yellow;
            rngHeader.Style.Font.Bold = true;

            // Title
            ws.Cell(2, 1).Value = "STT";
            ws.Cell(2, 2).Value = "Tên nhà hàng";
            ws.Cell(2, 3).Value = "Gia";
            ws.Cell(2, 4).Value = "Địa chỉ";
            ws.Cell(2, 5).Value = "Nội dung";
            ws.Cell(2, 6).Value = "Ngày tạo";
            ws.Cell(2, 7).Value = "Người tạo";

            int row = 3;

            List<NhaHang> nhaHangs = new MapNhaHang().DanhSach("");

            for (int i = 0; i < nhaHangs.Count; i++)
            {
                ws.Cell(row, 1).Value = i;
                ws.Cell(row, 2).Value = nhaHangs[i].Ten;
                ws.Cell(row, 3).Value = nhaHangs[i].Gia;
                ws.Cell(row, 4).Value = nhaHangs[i].DiaChi;
                ws.Cell(row, 5).Value = Base.StripHTML(nhaHangs[i].NoiDung);
                ws.Cell(row, 6).Value = nhaHangs[i].NgayTao;
                ws.Cell(row, 7).Value = nhaHangs[i].NguoiTao;
                row++;
            }

            string namefile = "Export_NhaHang_" + DateTime.Now.Ticks + ".xlsx";
            string pathFile = Server.MapPath("~/Data/ExportExcel/" + namefile);

            wb.SaveAs(pathFile);
            return Json(namefile, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ImportExcel(object sender)
        {
            List<NhaHang> listNhaHang = new List<NhaHang>();
            try
            {
                // mở file excel

                string pathFile = Server.MapPath("~/Data/ImportExcel/ImportData-nhahang.xlsx");

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
                        string ten = ws1.Cell(i, 2).GetValue<string>();
                        var Gia = ws1.Cell(i, 3).GetValue<int>();
                        string DiaChi = ws1.Cell(i, 4).GetValue<string>();
                        string NoiDung = ws1.Cell(i, 5).GetValue<string>();
                        DateTime NgayTao = ws1.Cell(i, 6).GetValue<DateTime>();
                        string NguoiTao = ws1.Cell(i, 7).GetValue<string>();

                        // tạo UserInfo từ dữ liệu đã lấy được
                        NhaHang nhaHang = new NhaHang()
                        {
                            Ten = ten,
                            Gia = Gia,
                            DiaChi = DiaChi,
                            NoiDung = NoiDung,
                            NgayTao = NgayTao,
                            NguoiTao = NguoiTao,
                        };

                        // add Phuongtien vào danh sách listPT
                        listNhaHang.Add(nhaHang);

                    }

                    catch (Exception exe)
                    {

                    }
                }
                foreach (NhaHang item in listNhaHang)
                {
                    db.NhaHangs.Add(item);
                    db.SaveChanges();
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show("Error!");
            }
            return Redirect("/admin/NhaHang");
        }
    }
}