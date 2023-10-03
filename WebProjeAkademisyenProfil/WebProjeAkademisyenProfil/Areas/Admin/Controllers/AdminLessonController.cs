using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebProjeAkademisyenProfil.Areas.Admin.Filters;
using WebProjeAkademisyenProfil.Data;
using WebProjeAkademisyenProfil.Models;

namespace WebProjeAkademisyenProfil.Areas.Admin.Controllers
{
    [Area("Admin")]
    
    public class AdminLessonController : Controller
    {
        private APSContext db = new APSContext();

        [ServiceFilter(typeof(AdminUserSecurityAttribute))]
        public IActionResult AdminLesson()
        {
            List<SelectListItem> values = (from a in db.Academicians.ToList() select new SelectListItem { Text = a.AcademicianName +" "+ a.AcademicianSurName, Value = a.AcademicianId.ToString()}).ToList();
            ViewBag.selectedItem = values;
            ViewBag.AdminName = HttpContext.Session.GetString("Ad");
            ViewBag.AdminSurName = HttpContext.Session.GetString("Soyad");
            ViewBag.AdminImage = HttpContext.Session.GetString("Resim");
            return View(db.Academicians.ToList());
        }

        [ServiceFilter(typeof(AdminUserSecurityAttribute))]
        public IActionResult AdminLessonAdd()
        {
            ViewBag.AdminName = HttpContext.Session.GetString("Ad");
            ViewBag.AdminSurName = HttpContext.Session.GetString("Soyad");
            ViewBag.AdminImage = HttpContext.Session.GetString("Resim");
            return View(db.Academicians.ToList());
        }


    }
}
