using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace CuaHangBanHoa.Areas.Admin.Controllers
{
    public class TinTucAdminController : Controller
    {
        CuaHangNoiThatEntities db = new CuaHangNoiThatEntities();
        // GET: Admin/TinTucAdmin
        public ActionResult Index()
        {
            var u = Session["use"] as CuaHangBanHoa.TaiKhoan;
            if (u.PhanQuyen.TenQuyen == "Adminstrator")
            {
                return View(db.TinTucs.ToList());
            }
            return RedirectPermanent("~/Home/Index");
        }
        public ActionResult CreateNews()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateNews([Bind(Include = "TieuDe,NoiDung")] TinTuc tintuc)
        {
            if (ModelState.IsValid)
            {
                db.TinTucs.Add(tintuc);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tintuc);
        }

        public ActionResult EditNews(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            TinTuc tintuc = db.TinTucs.Find(id);
            if (tintuc == null)
            {
                return HttpNotFound();
            }

            return View(tintuc);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditNews([Bind(Include = "MaTT,TieuDe,NoiDung")] TinTuc tintuc)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tintuc).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tintuc);
        }

        // GET: TinTucAdmin/DeleteNews/5
        public ActionResult DeleteNews(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            TinTuc tintuc = db.TinTucs.Find(id);
            if (tintuc == null)
            {
                return HttpNotFound();
            }

            return View(tintuc);
        }

        // POST: TinTucAdmin/DeleteNews/5
        [HttpPost, ActionName("DeleteNews")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteNews(int id)
        {
            TinTuc tintuc = db.TinTucs.Find(id);
            if (tintuc != null)
            {
                db.TinTucs.Remove(tintuc);
                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }

    }
}