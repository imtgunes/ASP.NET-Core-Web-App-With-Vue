using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebProjeAkademisyenProfil.Data;
using WebProjeAkademisyenProfil.Models;

namespace WebProjeAkademisyenProfil.Controllers
{
    public class HomeController : Controller
    {
        private APSContext db = new APSContext();
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            string userId = Request.Cookies["userID"];
            int acdId;
            if (!String.IsNullOrEmpty(userId))
            {
                acdId = int.Parse(userId);
            }
            else
            {
                acdId = 1;
            }
            
            ViewBag.Name = db.Academicians.Find(acdId).AcademicianName.ToString();
            ViewBag.SurName = db.Academicians.Find(acdId).AcademicianSurName.ToString();
            ViewBag.Image = db.Academicians.Find(acdId).AcademicianImage.ToString();
            ViewBag.InstituteInfo = db.Infos.Where(a => a.AcademicianId == acdId).Select(i => i.InfoInstitute).SingleOrDefault();
            ViewBag.ResearchInfo = db.Infos.Where(a => a.AcademicianId == acdId).Select(i => i.InfoResearch).SingleOrDefault();
            ViewBag.BioInfo = db.Infos.Where(a => a.AcademicianId == acdId).Select(i => i.InfoBio).SingleOrDefault();
            ViewBag.EmailInfo = db.Infos.Where(a => a.AcademicianId == acdId).Select(i => i.InfoEmail).SingleOrDefault();
            ViewBag.WebsiteInfo = db.Infos.Where(a => a.AcademicianId == acdId).Select(i => i.InfoWebsite).SingleOrDefault();
            ViewBag.AddressInfo = db.Infos.Where(a => a.AcademicianId == acdId).Select(i => i.InfoAddress).SingleOrDefault();
            
            return View(db.Categories.ToList());
        }


        [HttpGet("{academicianName}&{academicianSurName}")]
        public IActionResult GetAcademicianInfo(String academicianName, String academicianSurName)
        {
            var acd = db.Academicians.Where(a => a.AcademicianName == academicianName && a.AcademicianSurName == academicianSurName).SingleOrDefault();
            CookieOptions cookie = new CookieOptions();
            cookie.Expires = DateTime.Now.AddYears(10);
            Response.Cookies.Append("userID", acd.AcademicianId.ToString(), cookie);

            return RedirectToAction("Index", "Home");

        }

        [HttpGet("Home/Index/{academicianName}&{academicianSurName}")]
        public IActionResult GetAcademicianInfoHome(String academicianName, String academicianSurName)
        {
            var acd = db.Academicians.Where(a => a.AcademicianName == academicianName && a.AcademicianSurName == academicianSurName).SingleOrDefault();
            CookieOptions cookie = new CookieOptions();
            cookie.Expires = DateTime.Now.AddYears(10);
            Response.Cookies.Append("userID", acd.AcademicianId.ToString(), cookie);

            return RedirectToAction("Index", "Home");

        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}