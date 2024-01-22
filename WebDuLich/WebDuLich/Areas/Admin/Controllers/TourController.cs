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
using WebDuLich.Models.ModelView.Tours;
using WebDuLich.Common;


namespace WebDuLich.Areas.Admin.Controllers
{
    [kiemTraQuyen]
    public class TourController : Controller
    {
        TestDataEntities db = new TestDataEntities();
        // GET: Tour
        public ActionResult Index()
        {
            var model = new MapTour();
            List<Tour> tours = model.DanhSach(0, 0);
            List<DiaDiem> diadiems = model.DsDiaDiem();
            List<KhachSan> khachsans = model.DsKhachSan();

            return View("Index", new MapTourView
            {
                tours = tours,
                khachSans = khachsans,
                diaDiems = diadiems,

            });
        }

        public ActionResult Create()
        {
            var maptour = new MapTour();
            List<DiaDiem> diadiems = maptour.DsDiaDiem();
            List<KhachSan> khachsans = maptour.DsKhachSan();

            return View(new MapTourView
            {
                diaDiems = diadiems,
                khachSans = khachsans,
            });
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(Tour tour)
        {
            var maptour = new MapTour();
            if (new MapTour().ThemMoi(tour))
            {
                ViewBag.thongbap = maptour.thongbao;
                return Redirect("/Admin/Tour");


            }
            ViewBag.thongbap = maptour.thongbao;
            return Create(tour);

        }

        public ActionResult Update(int id)
        {
            MapTour mapTour = new MapTour();
            Tour tour = mapTour.Chitiet(id);
            List<KhachSan> khachSans = new MapKhachSan().DanhSach();
            List<DiaDiem> diaDiems = new MapDiaDiem().DanhSach();

            MapTourView mapTourView = new MapTourView()
            {
                tour = tour,
                diaDiems = diaDiems,
                khachSans = khachSans
            };

            if (tour != null)
            {
                return View(mapTourView);
            }
            return View(mapTourView);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Update(Tour model)
        {
            MapTour tour = new MapTour();
            if (tour.CapNhat(model))
            {
                return Redirect("/Admin/Tour");
            }
            return Update(model.ID);

        }

        public ActionResult Delete(int id)
        {
            new MapTour().Xoa(id);
            return Redirect("/Admin/Tour");
        }

        [HttpPost]
        public ActionResult Timkiem(string table_search, int? IDDiadiem, int? IDKhachSan)
        {
            var model = new MapTour();
            var tours = new MapTour().DanhSach(IDDiadiem, IDKhachSan);
            List<DiaDiem> diadiems = model.DsDiaDiem();
            List<KhachSan> khachsans = model.DsKhachSan();

            return PartialView("_Detail", new MapTourView
            {
                tours = tours,
                khachSans = khachsans,
                diaDiems = diadiems,
            });
        }

        public ActionResult ExportExcel()
        {
            var wb = new XLWorkbook();

            var ws = wb.Worksheets.Add("Contacts");
            ws.Range("A2:i2").Style.Font.Bold = true;
            var rngHeader = ws.Range("A1:i1");
            rngHeader.Merge();
            rngHeader.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
            rngHeader.Style.Alignment.SetVertical(XLAlignmentVerticalValues.Center);
            rngHeader.Value = "Danh sách tour";
            rngHeader.Style.Fill.BackgroundColor = XLColor.Aqua;
            rngHeader.Style.Font.Bold = true;

            // Title
            ws.Cell(2, 1).Value = "STT";
            ws.Cell(2, 2).Value = "Tên tour";
            ws.Cell(2, 3).Value = "Giá cũ";
            ws.Cell(2, 4).Value = "Giá mới";
            ws.Cell(2, 5).Value = "Bài viết";
            ws.Cell(2, 6).Value = "Đia điểm";
            ws.Cell(2, 7).Value = "Khách sạn";
            ws.Cell(2, 8).Value = "Ngày tạo";
            ws.Cell(2, 9).Value = "Ngưới tạo";

            int row = 3;

            List<Tour> tours = new MapTour().DanhSach(0, 0);
            var mapTour = new MapTourView()
            {
                tours = tours,
                khachSans = db.KhachSans.ToList(),
                diaDiems = db.DiaDiems.ToList(),
            };

            for (int i = 0; i < mapTour.tours.Count; i++)
            {
                ws.Cell(row, 1).Value = i;
                ws.Cell(row, 2).Value = mapTour.tours[i].TenTour;
                ws.Cell(row, 3).Value = mapTour.tours[i].GiaCu;
                ws.Cell(row, 4).Value = mapTour.tours[i].GiaMoi;
                if (string.IsNullOrEmpty(mapTour.tours[i].BaiViet))
                {
                    ws.Cell(row, 5).Value = "";
                }
                else
                {
                    ws.Cell(row, 5).Value = Base.StripHTML(mapTour.tours[i].BaiViet);
                }
                for (int j = 0; j < mapTour.diaDiems.Count; j++)
                {
                    if (mapTour.tours[i].IDDiaDiem == mapTour.diaDiems[j].ID)
                    {
                        ws.Cell(row, 6).Value = mapTour.diaDiems[j].Ten;
                        break;
                    }
                    else
                    {
                        ws.Cell(row, 6).Value = "Không có Đia điểm";
                    }
                }
                for (int k = 0; k < mapTour.diaDiems.Count; k++)
                {
                    if (mapTour.tours[i].IDKhachSan == mapTour.khachSans[k].ID)
                    {
                        ws.Cell(row, 7).Value = mapTour.khachSans[k].Ten;
                        break;
                    }
                    else
                    {
                        ws.Cell(row, 7).Value = "Không có Đia điểm";
                    }
                }
                ws.Cell(row, 8).Value = mapTour.tours[i].NgayTao;
                ws.Cell(row, 9).Value = mapTour.tours[i].NguoiTao;
                row++;
            }

            string namefile = "Export_Tour_" + DateTime.Now.Ticks + ".xlsx";
            string pathFile = Server.MapPath("~/Data/ExportExcel/" + namefile);

            wb.SaveAs(pathFile);
            return Json(namefile, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ImportExcel()
        {
            List<Tour> listTour = new List<Tour>();
            try
            {
                // mở file excel

                string pathFile = Server.MapPath("~/Data/ImportExcel/ImportData-Tour.xlsx");

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
                        var giacu = ws1.Cell(i, 3).GetValue<int>();
                        var giamoi = ws1.Cell(i, 4).GetValue<int>();
                        string baiviet = ws1.Cell(i, 5).GetValue<string>();
                        var IDdiaDiem = ws1.Cell(i, 6).GetValue<int>();
                        var IDkhachSan = ws1.Cell(i, 7).GetValue<int>();
                        DateTime Ngaytao = ws1.Cell(i, 8).GetValue<DateTime>();
                        string NguoiTao = ws1.Cell(i, 9).GetValue<string>();

                        // tạo Tour từ dữ liệu đã lấy được
                        Tour Tour = new Tour()
                        {
                            TenTour = ten,
                            GiaCu = giacu,
                            GiaMoi = giamoi,
                            BaiViet = baiviet,
                            IDDiaDiem = IDdiaDiem,
                            IDKhachSan = IDkhachSan,
                            NgayTao = Ngaytao,
                            NguoiTao = NguoiTao,
                        };

                        // add Tour vào danh sách listTour
                        listTour.Add(Tour);
                    }

                    catch (Exception exe)
                    {

                    }
                }
                foreach (Tour item in listTour)
                {
                    db.Tours.Add(item);
                    db.SaveChanges();
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show("Error!");
            }
            return Redirect("/admin/Tour");
        }

    }
}