using Microsoft.AspNetCore.Mvc;
using WebProjeAkademisyenProfil.Areas.Admin.Filters;

namespace WebProjeAkademisyenProfil.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminUsersController : Controller
    {
        [ServiceFilter(typeof(AdminUserSecurityAttribute))]
        public IActionResult AdminUsers()
        {
            ViewBag.AdminName = HttpContext.Session.GetString("Ad");
            ViewBag.AdminSurName = HttpContext.Session.GetString("Soyad");
            ViewBag.AdminImage = HttpContext.Session.GetString("Resim");
            ViewBag.AdminID =  HttpContext.Session.GetInt32("Kullanici_ID"); 
            return View();
        }
        
             
        public IActionResult AdminUsersEdit()
        {
            ViewBag.AdminName = HttpContext.Session.GetString("Ad");
            ViewBag.AdminSurName = HttpContext.Session.GetString("Soyad");
            ViewBag.AdminImage = HttpContext.Session.GetString("Resim");
            ViewBag.AdminID = HttpContext.Session.GetInt32("Kullanici_ID");
            return View();
        }
        
        public IActionResult AdminUsersAdd()
        {
            ViewBag.AdminName = HttpContext.Session.GetString("Ad");
            ViewBag.AdminSurName = HttpContext.Session.GetString("Soyad");
            ViewBag.AdminImage = HttpContext.Session.GetString("Resim");
            return View();
        }
    }
}
