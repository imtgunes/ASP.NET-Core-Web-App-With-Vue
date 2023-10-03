using Microsoft.AspNetCore.Mvc;
using WebProjeAkademisyenProfil.Areas.Admin.Filters;
using WebProjeAkademisyenProfil.Data;

namespace WebProjeAkademisyenProfil.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminExperienceController : Controller
    {
        private APSContext db = new APSContext();

        [ServiceFilter(typeof(AdminUserSecurityAttribute))]
        public IActionResult AdminExperience()
        {
            ViewBag.AdminName = HttpContext.Session.GetString("Ad");
            ViewBag.AdminSurName = HttpContext.Session.GetString("Soyad");
            ViewBag.AdminImage = HttpContext.Session.GetString("Resim");
            return View(db.Academicians.ToList());
        }

        [ServiceFilter(typeof(AdminUserSecurityAttribute))]
        public IActionResult AdminExperienceAdd()
        {
            ViewBag.AdminName = HttpContext.Session.GetString("Ad");
            ViewBag.AdminSurName = HttpContext.Session.GetString("Soyad");
            ViewBag.AdminImage = HttpContext.Session.GetString("Resim");
            return View(db.Academicians.ToList());
        }
    }
}
