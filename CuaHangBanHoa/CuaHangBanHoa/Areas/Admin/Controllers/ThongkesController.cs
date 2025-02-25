using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using OfficeOpenXml; // Thêm thư viện EPPlus
using OfficeOpenXml.Style;
using CuaHangBanHoa.Models;

namespace CuaHangBanHoa.Areas.Admin.Controllers
{
    public class ThongkesController : Controller
    {
        CuaHangNoiThatEntities db = new CuaHangNoiThatEntities();

        public ActionResult Index()
        {
            var u = Session["use"] as CuaHangBanHoa.TaiKhoan;
            if (u != null && u.PhanQuyen.TenQuyen == "Adminstrator")
            {
                var dataThongke = db.DonHangs
                    .GroupBy(s => s.MaNguoiDung)
                    .Select(g => new ThongKes
                    {
                        Tennguoidung = g.FirstOrDefault().TaiKhoan.HoTen,
                        Tongtien = g.Sum(x => x.TongTien),
                        Dienthoai = g.FirstOrDefault().TaiKhoan.Dienthoai,
                        Soluong = g.Count()
                    })
                    .OrderByDescending(s => s.Tongtien)
                    .Take(5)
                    .ToList();

                return View(dataThongke);
            }
            return RedirectPermanent("~/HomeAdmin/Index");
        }

        // ✅ Xuất dữ liệu ra Excel
        public ActionResult ExportExcel()
        {
            var dataThongke = db.DonHangs
                .GroupBy(s => s.MaNguoiDung)
                .Select(g => new ThongKes
                {
                    Tennguoidung = g.FirstOrDefault().TaiKhoan.HoTen,
                    Tongtien = g.Sum(x => x.TongTien),
                    Dienthoai = g.FirstOrDefault().TaiKhoan.Dienthoai,
                    Soluong = g.Count()
                })
                .OrderByDescending(s => s.Tongtien)
                .Take(5)
                .ToList();
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (ExcelPackage package = new ExcelPackage())
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Top 5 Khách Hàng");

                // 🟢 Header
                worksheet.Cells["A1:D1"].Merge = true;
                worksheet.Cells["A1"].Value = "Top 5 Khách Hàng Mua Hàng Nhiều Nhất";
                worksheet.Cells["A1"].Style.Font.Size = 14;
                worksheet.Cells["A1"].Style.Font.Bold = true;
                worksheet.Cells["A1"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                // 🟢 Tiêu đề cột
                worksheet.Cells["A2"].Value = "Tên Khách Hàng";
                worksheet.Cells["B2"].Value = "Số Điện Thoại";
                worksheet.Cells["C2"].Value = "Tổng Tiền Đã Mua";
                worksheet.Cells["D2"].Value = "Số Lượng Đơn Hàng";

                using (var range = worksheet.Cells["A2:D2"])
                {
                    range.Style.Font.Bold = true;
                    range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    range.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightBlue);
                    range.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                }

                // 🟢 Dữ liệu
                int row = 3;
                foreach (var item in dataThongke)
                {
                    worksheet.Cells[row, 1].Value = item.Tennguoidung;
                    worksheet.Cells[row, 2].Value = item.Dienthoai;
                    worksheet.Cells[row, 3].Value = item.Tongtien;
                    worksheet.Cells[row, 3].Style.Numberformat.Format = "#,##0 VNĐ";
                    worksheet.Cells[row, 4].Value = item.Soluong;
                    row++;
                }

                // 🟢 AutoFit cột
                worksheet.Cells.AutoFitColumns();

                // 🟢 Xuất file
                var stream = new MemoryStream();
                package.SaveAs(stream);
                stream.Position = 0;

                string fileName = "ThongKeKhachHang.xlsx";
                string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                return File(stream, contentType, fileName);
            }
        }
    }
}
