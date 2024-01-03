using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebDuLich.App_Start;

namespace WebDuLich.Areas.Admin.Controllers
{
    public class HomeController : Controller
    {
        [kiemTraQuyen]
        // GET: Admin/Home
        public ActionResult Index()
        {
            return View();
        }
    }
}