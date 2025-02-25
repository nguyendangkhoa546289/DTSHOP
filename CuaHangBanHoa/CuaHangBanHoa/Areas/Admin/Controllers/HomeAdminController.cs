using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CuaHangBanHoa.Areas.Admin.Controllers
{
    public class HomeAdminController : Controller
    {
        //
        // GET: /Admin/HomeAdmin/
        public ActionResult Index()
        {
            var u = Session["use"] as CuaHangBanHoa.TaiKhoan;
            //Kiểm tra nếu tên quyền Administrator mới được truy cập trang admin
            if (u.PhanQuyen.TenQuyen == "Adminstrator")
            {
                return View();
            }

            return RedirectPermanent("~/Home/Index");
        }
	}
}