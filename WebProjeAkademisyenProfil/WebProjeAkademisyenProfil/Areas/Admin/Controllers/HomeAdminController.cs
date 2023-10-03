using Microsoft.AspNetCore.Mvc;
using WebProjeAkademisyenProfil.Areas.Admin.Filters;

namespace WebProjeAkademisyenProfil.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeAdminController : Controller
    {
        [ServiceFilter(typeof(AdminUserSecurityAttribute))]
        public IActionResult HomeAdmin()
        {
            ViewBag.AdminName = HttpContext.Session.GetString("Ad");
            ViewBag.AdminSurName = HttpContext.Session.GetString("Soyad");
            ViewBag.AdminImage = HttpContext.Session.GetString("Resim");
            ViewBag.AdminID = HttpContext.Session.GetInt32("Kullanici_ID");
            return View();
        }

        public IActionResult ErrorHandling()
        {
            return View();
        }
    }
}
