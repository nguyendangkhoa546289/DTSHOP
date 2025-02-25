using System;
using CuaHangBanHoa.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.Net;


namespace CuaHangBanHoa.Controllers
{
    public class NguoiDungController : Controller
    {
        //
        // GET: /NguoiDung/
        CuaHangNoiThatEntities db = new CuaHangNoiThatEntities();
        public ActionResult DangKy()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult DangKy(TaiKhoan taiKhoan, FormCollection f)
        {
            // Lưu tài khoản vào session
            Session["userReg"] = taiKhoan;

            // Kiểm tra xem email có tồn tại trong cơ sở dữ liệu không
            bool checkEmail = db.TaiKhoans.Any(s => s.Email == taiKhoan.Email);

            if (!checkEmail)
            {
                // Cập nhật thông tin tài khoản
                taiKhoan.HoTen = f["HoTen"];
                taiKhoan.Email = f["Email"];
                taiKhoan.Matkhau = f["MatKhau"];
                taiKhoan.Dienthoai = f["DienThoai"];
                taiKhoan.Diachi = f["DiaChi"];

                // Thêm tài khoản mới vào cơ sở dữ liệu
                db.TaiKhoans.Add(taiKhoan);
                db.SaveChanges();

                // Thông báo đăng ký thành công
                ViewBag.RegOk = "Đăng kí thành công. Đăng nhập ngay";
                ViewBag.isReg = true;
            }
            else
            {
                // Email đã tồn tại, yêu cầu nhập lại email
                ViewBag.RegOk = "Email đã tồn tại! Vui lòng chọn 1 email khác";
                ViewBag.isReg = false;
            }

            return View("DangKy");
        }

        [AllowAnonymous]
        public ActionResult DangNhap()
        {
            return View();

        }

        [HttpPost]
        public ActionResult DangNhap(FormCollection userlog)
        {
            string userMail = userlog["Email"].ToString();
            string password = userlog["MatKhau"].ToString();
            var islogin = db.TaiKhoans.FirstOrDefault(x => x.Email.Equals(userMail) && x.Matkhau.Equals(password));

            if (islogin != null)
            {
                if (userMail == "Admin@gmail.com")
                {
                    Session["use"] = islogin;
                    return RedirectToAction("Index", "Admin/HomeAdmin");
                }
                else
                {
                    Session["use"] = islogin;
                    return RedirectToAction("Index", "Home");
                }
            }
            ViewBag.Fail = "Tài khoản hoặc mật khẩu không chính xác.";
            return View("DangNhap");
        }
        public ActionResult listTK()
        {
            List<TaiKhoan> lstKH = db.TaiKhoans.ToList();
            return View(lstKH);
        }
        public ActionResult DangXuat()
        {
            Session["use"] = null;
            return RedirectToAction("Index", "Home");

        }

        public ActionResult ThongTinNguoiDung(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TaiKhoan taiKhoan = db.TaiKhoans.Find(id);
            if (taiKhoan == null)
            {
                return HttpNotFound();
            }
            return View(taiKhoan);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TaiKhoan taiKhoan = db.TaiKhoans.Find(id);
            if (taiKhoan == null)
            {
                return HttpNotFound();
            }
            ViewBag.IDQuyen = new SelectList(db.PhanQuyens, "IDQuyen", "TenQuyen", taiKhoan.IDQuyen);
            return View(taiKhoan);
        }

        // POST: Admin/Nguoidungs/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaNguoiDung,HoTen,Email,Dienthoai,Matkhau,IDQuyen,Diachi")] TaiKhoan taiKhoan)
        {
            if (ModelState.IsValid)
            {
                db.Entry(taiKhoan).State = EntityState.Modified;
                db.SaveChanges();
                //@ViewBag.show = "Chỉnh sửa hồ sơ thành công";
                //return View(nguoidung);
                return RedirectToAction("Profile", new { id = taiKhoan.MaNguoiDung });

            }
            ViewBag.IDQuyen = new SelectList(db.PhanQuyens, "IDQuyen", "TenQuyen", taiKhoan.IDQuyen);
            return View(taiKhoan);
        }
        public static byte[] encryptData(string data)
        {
            System.Security.Cryptography.MD5CryptoServiceProvider md5Hasher = new System.Security.Cryptography.MD5CryptoServiceProvider();
            byte[] hashedBytes;
            System.Text.UTF8Encoding encoder = new System.Text.UTF8Encoding();
            hashedBytes = md5Hasher.ComputeHash(encoder.GetBytes(data));
            return hashedBytes;
        }
        public static string md5(string data)
        {
            return BitConverter.ToString(encryptData(data)).Replace("-", "").ToLower();
        }
        //public ActionResult QuenMatKhau(string email)
        //{
        //    string email1 = email;
        //    var s = db.TaiKhoans.FirstOrDefault(n=>n.Email == email1);
        //    if (s != null)
        //    {
        //        ViewBag.ThongBao = "Đã gửi mail thành công";
        //        var mail = new SmtpClient("smtp.gmail.com", 587)
        //        {
        //            Credentials = new NetworkCredential("huynhtam3000@gmail.com");
        //            EnableSsl = true;
        //        }
        //    }

        //}
	}
}