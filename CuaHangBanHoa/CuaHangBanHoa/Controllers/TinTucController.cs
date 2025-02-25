using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CuaHangBanHoa.Controllers
{
    public class TinTucController : Controller
    {
        //
        // GET: /TinTuc/
        CuaHangNoiThatEntities db = new CuaHangNoiThatEntities();
        public ActionResult Index()
        {
            var sanPham = db.TinTucs;
            return View(sanPham.ToList());
        }
	}
}