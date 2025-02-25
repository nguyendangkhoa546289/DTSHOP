using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CuaHangBanHoa.Controllers
{
    public class DanhMucController : Controller
    {
        //
        // GET: /DanhMuc/
        CuaHangNoiThatEntities db = new CuaHangNoiThatEntities();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult DanhMucPartial()
        {

            //var danhmuc = db.LoaiHangs.ToList();
            //return PartialView(danhmuc);

            var danhmuc = db.LoaiHangs.Where(n => n.TenLoai != "Thùng rác").Take(6).ToList();
            return PartialView(danhmuc);
        }
	}
}