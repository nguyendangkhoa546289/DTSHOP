using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace CuaHangBanHoa.Controllers
{
    public class SanPhamController : Controller
    {
        //
        // GET: /SanPham/
        CuaHangNoiThatEntities db = new CuaHangNoiThatEntities();
        // GET: sanpham
        public ActionResult Index()
        {
            var sanPham = db.SanPhams.Include(s => s.LoaiHang);
            return View(sanPham.ToList());
        }

        public ActionResult PhongKhachPartial(int? page)
        {
            var ip = db.SanPhams.Where(n => n.MaLoai == 1).Take(4).ToList();
            return PartialView(ip);

        }
        public ActionResult PhongNguPartial(int? page)
        {
            var ip = db.SanPhams.Where(n => n.MaLoai == 2).Take(4).ToList();
            return PartialView(ip);

        }
        public ActionResult PhongAnPartial(int? page)
        {
            var ip = db.SanPhams.Where(n => n.MaLoai == 3).Take(4).ToList();
            return PartialView(ip);

        }
       

        public ActionResult XemChiTietSP(int Masp = 0)
        {
            //var chitiet = db.SanPhams.SingleOrDefault(n => n.MaSP == Masp);
            var chitiet = db.SanPhams.Where(s => s.MaSP == Masp).FirstOrDefault();
            if (chitiet == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(chitiet);
        }

        public ActionResult XemChiTietDanhMuc(int MaLoai)
        {
            var ip = db.SanPhams.Where(n => n.MaLoai == MaLoai).ToList();
            return PartialView(ip);

        }

        public ActionResult TimKiem(string keyword)
        {
            // Kiểm tra nếu keyword rỗng thì quay về danh sách sản phẩm
            if (string.IsNullOrEmpty(keyword))
            {
                return RedirectToAction("Index");
            }

            // Tìm kiếm trong database theo tên sản phẩm
            var sanPhamTimKiem = db.SanPhams.Where(s => s.TenSP.Contains(keyword)).ToList();

            // Nếu không tìm thấy sản phẩm nào, có thể trả về thông báo
            if (sanPhamTimKiem.Count == 0)
            {
                ViewBag.ThongBao = "Không tìm thấy sản phẩm nào.";
            }

            return View(sanPhamTimKiem);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}