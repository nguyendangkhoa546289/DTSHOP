using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using System.Data.Entity;
using CuaHangBanHoa.Models;
using System.IO;

namespace CuaHangBanHoa.Areas.Admin.Controllers
{
    public class SanPhamsController : Controller
    {
        //
        CuaHangNoiThatEntities db = new CuaHangNoiThatEntities();
        // GET: /Admin/SanPhams/
        public ActionResult Index()
        {
            var sanPhams = db.SanPhams.Include(s => s.LoaiHang);
            var u = Session["use"] as CuaHangBanHoa.TaiKhoan;
            if (u.PhanQuyen.TenQuyen == "Adminstrator")
            {
                return View(sanPhams.OrderByDescending(s => s.MaSP).ToList());
            }
            return RedirectPermanent("~/Home/Index");
        }

        // GET: SanPhams/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SanPham sanPham = db.SanPhams.Find(id);
            if (sanPham == null)
            {
                return HttpNotFound();
            }
            return View(sanPham);
        }

        // GET: SanPhams/Create
        public ActionResult Create()
        {
            ViewBag.MaLoai = new SelectList(db.LoaiHangs, "MaLoai", "TenLoai");
            
            return View();
        }

        // POST: SanPhams/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaSP,TenSP,GiaBan,Soluong,MoTa,MaLoai,MaNCC,AnhSP")] SanPham sanPham)
        {
            if (ModelState.IsValid)
            {
                db.SanPhams.Add(sanPham);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaLoai = new SelectList(db.LoaiHangs, "MaLoai", "TenLoai", sanPham.MaLoai);
           
            return View(sanPham);
        }

        // GET: SanPhams/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SanPham sanPham = db.SanPhams.Find(id);
            if (sanPham == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaLoai = new SelectList(db.LoaiHangs, "MaLoai", "TenLoai", sanPham.MaLoai);
           
            return View(sanPham);
        }

        // POST: SanPhams/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaSP,TenSP,GiaBan,Soluong,MoTa,MaLoai,MaNCC")] SanPham sanPham, HttpPostedFileBase AnhSP)
        {
            if (ModelState.IsValid)
            {
                if (AnhSP != null && AnhSP.ContentLength > 0)
                {
                    string fileName = Path.GetFileName(AnhSP.FileName);
                    string path = Path.Combine(Server.MapPath("~/Content/Images/"), fileName);
                    AnhSP.SaveAs(path);
                    if (sanPham.MaLoai == 3)
                    {
                        sanPham.AnhSP = "~/Images/PhongAn" + fileName;
                    }
                    if (sanPham.MaLoai == 2)
                    {
                        sanPham.AnhSP = "~/Images/PhongNgu" + fileName;
                    }
                    if (sanPham.MaLoai == 1)
                    {
                        sanPham.AnhSP = "~/Images/PhongKhach" + fileName;
                    }
                }

                db.Entry(sanPham).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaLoai = new SelectList(db.LoaiHangs, "MaLoai", "TenLoai", sanPham.MaLoai);
            return View(sanPham);
        }
        // GET: SanPhams/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SanPham sanPham = db.SanPhams.Find(id);
            if (sanPham == null)
            {
                return HttpNotFound();
            }
            return View(sanPham);
        }

        // POST: SanPhams/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SanPham sanPham = db.SanPhams.Find(id);
            db.SanPhams.Remove(sanPham);
            db.SaveChanges();
            return RedirectToAction("Index");
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